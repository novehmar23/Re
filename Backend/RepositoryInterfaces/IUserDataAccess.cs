using Domain;

namespace RepositoryInterfaces
{
    public interface IUserDataAccess<T> where T : User
    {
        public T Create(T newUser);
        public bool VerifyRole(string token);
    }
}
