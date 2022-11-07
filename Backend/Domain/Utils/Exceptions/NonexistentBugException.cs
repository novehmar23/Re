using System;

namespace Domain.Utils
{
    public class NonexistentBugException : Exception
    {

        public NonexistentBugException() : base("The bug does not exist") { }
    }
}