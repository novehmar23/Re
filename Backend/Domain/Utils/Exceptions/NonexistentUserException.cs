using System;

namespace Domain.Utils
{
    public class NonexistentUserException : Exception
    {
        public NonexistentUserException() : base("The user does not exist or could not be found") { }
    }
}