using Domain;
using System.Collections.Generic;

namespace RepositoryInterfaces
{
    public interface ITesterDataAccess : IUserDataAccess<Tester>
    {
        List<Bug> GetBugsByStatus(int idTester, bool filter);
        List<Bug> GetBugsByName(int idTester, string filter);
        List<Bug> GetBugsByProject(int idTester, int filter);
        List<Tester> GetAllTesters();

        List<string> GetAllUsernames();
    }
}
