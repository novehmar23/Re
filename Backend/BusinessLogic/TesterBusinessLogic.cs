using BusinessLogicInterfaces;
using Domain;
using DTO;
using RepositoryInterfaces;
using System.Collections.Generic;

namespace BusinessLogic
{
    public class TesterBusinessLogic : ITesterBusinessLogic
    {
        public ITesterDataAccess testerDataAccess { get; set; }

        public TesterBusinessLogic(ITesterDataAccess newTesterDataAccess)
        {
            testerDataAccess = newTesterDataAccess;
        }


        public TesterDTO Add(TesterDTO newTester)
        {
            Tester tester = newTester.ConvertToDomain();
            tester.Validate();
            List<string> allUsers = testerDataAccess.GetAllUsernames();
            foreach (string u in allUsers)
            {
                if (u.Equals(tester.Username))
                    throw new ValidationException();
            }
            testerDataAccess.Create(tester);
            return newTester;
        }

        public List<BugDTO> GetBugsByStatus(int idTester, bool filter)
        {
            List<Bug> bugs = testerDataAccess.GetBugsByStatus(idTester, filter);
            return bugs.ConvertAll(b => new BugDTO(b));
        }

        public List<BugDTO> GetBugsByName(int idTester, string filter)
        {
            List<Bug> bugs = testerDataAccess.GetBugsByName(idTester, filter);
            return bugs.ConvertAll(b => new BugDTO(b));
        }

        public List<BugDTO> GetBugsByProject(int idTester, int filter)
        {
            List<Bug> bugs = testerDataAccess.GetBugsByProject(idTester, filter);
            return bugs.ConvertAll(b => new BugDTO(b));
        }
        public bool VerifyRole(string token)
        {
            return testerDataAccess.VerifyRole(token);
        }

        public List<TesterDTO> GetAllTesters()
        {
            return testerDataAccess.GetAllTesters().ConvertAll(t => new TesterDTO(t));
        }
    }


}