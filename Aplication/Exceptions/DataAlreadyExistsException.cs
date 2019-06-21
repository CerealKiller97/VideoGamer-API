using System;

namespace Aplication.Exceptions
{
	public class DataAlreadyExistsException : Exception
	{
		public DataAlreadyExistsException()
		{
		}

		public DataAlreadyExistsException(string message) : base(message)
		{
		}
	}
}
