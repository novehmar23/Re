using Domain.Utils;
using System;

namespace BugParser
{
    public class ParserFactory : IParserFactory
    {
        public IBugParser GetBugParser(ImportCompany format)
        {
            if (format == ImportCompany.XML)
                return new BugParserXML();
            else
                throw new NotImplementedException("Bug parsers for this company not available");
        }

    }
}
