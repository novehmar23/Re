using System.Collections;

namespace Domain.Utils
{
    public class WorkComparer : IComparer
    {
        public WorkComparer()
        {
        }

        public int Compare(object x, object y)
        {
            Work workExpected = x as Work;
            Work workReturned = y as Work;

            bool equals = workExpected.Name == workReturned.Name &&
                        workExpected.Cost == workReturned.Cost &&
                        workExpected.Time == workReturned.Time;

            return equals ? 0 : -1;
        }
    }
}