using Domain;
using DTO;
using System.Collections.Generic;

namespace BusinessLogicInterfaces
{
    public interface ITesterBusinessLogic : IUserBusinessLogic<Tester>
    {
        List<BugDTO> GetBugsByStatus(int idTester, bool filter);
        List<BugDTO> GetBugsByName(int idTester, string filter);
        List<BugDTO> GetBugsByProject(int idTester, int filter);
        TesterDTO Add(TesterDTO newTester);
        List<TesterDTO> GetAllTesters();
    }
}

