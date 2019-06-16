using System;
using System.Security.Cryptography;
using System.Text;

namespace PasswordHashing
{
    /// <summary>
    /// Password Hasher
    /// </summary>
    public class PasswordHasher : IPasswordHasher
    {
        /// <summary>
        /// PBKDF2 Hasher
        /// </summary>
        private readonly RNGCryptoServiceProvider _randomService;

        /// <summary>
        /// Size of buffer for salt
        /// </summary>
        private int _saltSize = 20;

        /// <summary>
        /// Number of iterations for hashing
        /// More you have, better the hash
        /// </summary>
        private int _iterations = 5000;

        /// <summary>
        /// Size of the hash in bytes
        /// </summary>
        private int _hashSize = 64;

        public int SaltSize
        {
            get => _saltSize;
            set
            {
                if (value > 20 && value < 100)
                {
                    _saltSize = value;
                }
                else
                {
                    throw new ArgumentException("Value of SaltSize must be bigger than 20 and lesser than 100");
                }

            }
        }

        public int Iterations
        {
            get => _iterations;
            set
            {
                if (value < 1000) throw new ArgumentException("Iterations must be bigger that 1000");
                _iterations = value;
            }
        }

        public int HashSize
        {
            get => _hashSize;
            set
            {
                if (value < 20) throw new ArgumentException();
                _hashSize = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="randomService"></param>
        public PasswordHasher(RNGCryptoServiceProvider randomService)
        {
            this._randomService = randomService;
        }

        public byte[] GenerateSalt()
        {
            byte[] saltBytes = new byte[_saltSize];
            this._randomService.GetBytes(saltBytes);
            return saltBytes;
        }
        public string HashPassword(string password, byte[] salt = null)
        {
            return HashPassword(Encoding.UTF8.GetBytes(password), salt);
        }

        public string HashPassword(byte[] password, byte[] salt = null)
        {
            if (salt == null)
            {
                salt = GenerateSalt();
            }
            using (var rfc = new Rfc2898DeriveBytes(password, salt, _iterations))
            {
                byte[] hash = rfc.GetBytes(64);

                byte[] hashAndSalt = new byte[HashSize + SaltSize];

                Array.Copy(salt, 0, hashAndSalt, 0, SaltSize);
                Array.Copy(hash, 0, hashAndSalt, SaltSize, HashSize);
                return Convert.ToBase64String(hashAndSalt);
            }
        }
        public bool ValidatePassword(string password, string hash)
        {
            if (password == null || hash == null)
            {
                throw new ArgumentNullException("All parameters must be passed and cannot be null");
            }
            byte[] hashBytes = Convert.FromBase64String(hash);
            byte[] salt = new byte[SaltSize];
            if (hashBytes.Length != (SaltSize + HashSize))
            {
                throw new ArgumentException($"Hash is not {SaltSize + HashSize} bytes");
            }
            Array.ConstrainedCopy(hashBytes, 0, salt, 0, SaltSize);
            return HashPassword(password, salt) == hash;
        }

        public void Dispose()
        {
            _randomService?.Dispose();
        }
    }
}
