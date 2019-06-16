using System;
using System.Collections.Generic;
using System.Text;

namespace PasswordHashing
{
    public interface IPasswordHasher : IDisposable
    {
        string HashPassword(string password, byte[] salt = null);
        string HashPassword(byte[] password, byte[] salt = null);
        bool ValidatePassword(string password, string hash);
        byte[] GenerateSalt();
    }
}
