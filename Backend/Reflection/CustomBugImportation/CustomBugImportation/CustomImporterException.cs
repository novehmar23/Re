using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomBugImportation
{
    public class CustomImporterException : Exception
    {
        public CustomImporterException(string? message) : base(message) { }
    }
}
