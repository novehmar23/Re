using System;

namespace Domain.Utils
{
    public class NonexistentWorkException : Exception
    {
        public NonexistentWorkException() : base("The work does not exist or could not be found") { }
    }
}