using System;

namespace Domain
{
    public class ValidationException : Exception
    {
        private string message;

        public override string Message
        {
            get { return message; }
        }

        public ValidationException()
        {
            this.message = "Invalid object";
        }
    }
}