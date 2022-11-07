using Domain;
using System.Collections.Generic;

namespace BugParser
{
    public interface IBugParser
    {
        List<Bug> GetBugs(string fullPath);
    }
}