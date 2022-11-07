using Domain.Utils;

namespace BugParser
{
    public interface IParserFactory
    {
        public IBugParser GetBugParser(ImportCompany format);
    }
}
