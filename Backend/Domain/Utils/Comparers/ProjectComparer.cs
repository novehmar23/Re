

using System.Collections;
using System.Linq;

namespace Domain.Utils
{
    public class ProjectComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            Project projectExpected = x as Project;
            Project projectReturned = y as Project;

            bool equals = projectExpected.Id == projectReturned.Id &&
                projectExpected.Name == projectReturned.Name &&
                !projectExpected.Testers.Where(i => !projectReturned.Testers.Contains(i)).Any() &&
                !projectReturned.Testers.Where(i => !projectExpected.Testers.Contains(i)).Any() &&
                !projectReturned.Developers.Where(i => !projectExpected.Developers.Contains(i)).Any() &&
                !projectReturned.Developers.Where(i => !projectExpected.Developers.Contains(i)).Any() &&
                !projectReturned.Bugs.Where(i => !projectExpected.Bugs.Contains(i)).Any() &&
                !projectReturned.Bugs.Where(i => !projectExpected.Bugs.Contains(i)).Any() &&
                !projectReturned.Works.Where(i => !projectExpected.Works.Contains(i)).Any() &&
                !projectReturned.Works.Where(i => !projectExpected.Works.Contains(i)).Any();

            return equals ? 0 : -1;
        }
    }
}

