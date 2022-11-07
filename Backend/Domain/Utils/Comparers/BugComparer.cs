using System.Collections;

namespace Domain.Utils
{
    public class BugComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            Bug bugExpected = x as Bug;
            Bug bugReturned = y as Bug;

            bool equals = bugExpected.Id == bugReturned.Id &&
                        bugExpected.Description == bugReturned.Description &&
                        bugExpected.Name == bugReturned.Name &&
                        bugExpected.IsActive == bugReturned.IsActive &&
                        bugExpected.Version == bugReturned.Version &&
                        bugExpected.ProjectName == bugReturned.ProjectName &&
                        ((bugExpected.CompletedBy == null && bugReturned.CompletedBy == null) ||
                        (bugExpected.CompletedBy.Equals(bugReturned.CompletedBy)));

            return equals ? 0 : -1;
        }
    }
}
