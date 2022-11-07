using System.Collections;

namespace Domain.Utils
{
    public class UserComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            User userExpected = x as User;
            User userReturned = y as User;

            bool equals = userExpected.Name == userReturned.Name &&
                        userExpected.Username == userReturned.Username &&
                        userExpected.Lastname == userReturned.Lastname &&
                        userExpected.Password == userReturned.Password &&
                        userExpected.Email == userReturned.Email;

            return equals ? 0 : -1;
        }
    }
}
