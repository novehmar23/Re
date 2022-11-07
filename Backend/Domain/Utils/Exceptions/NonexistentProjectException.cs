using System;

namespace Domain.Utils
{
    [Serializable]
    public class NonexistentProjectException : Exception
    {
        public NonexistentProjectException() : base("The project does not exist or could not be found") { }
    }
}