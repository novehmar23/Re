using Domain;
using System.Collections.Generic;

namespace RepositoryInterfaces
{
    public interface IDeveloperDataAccess : IUserDataAccess<Developer>
    {
        int GetQuantityBugsResolved(int idDev);
        List<Developer> GetAllDevs();

        List<string> GetAllUsernames();
    }
}
