using Domain;
using System.Collections.Generic;

namespace RepositoryInterfaces
{
    public interface IAdminDataAccess : IUserDataAccess<Admin>
    {
        List<User> GetAll();
        List<string> GetAllUsernames();
    }
}
