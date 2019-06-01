using System;

namespace Aplication.Exceptions
{
  public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string message) : base($"{message} not found.")
        {

        }
    }
}
