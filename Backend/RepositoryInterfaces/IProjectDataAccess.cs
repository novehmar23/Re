using BusinessLogicInterfaces;
using Domain;
using DTO;
using System.Collections.Generic;

namespace RepositoryInterfaces
{
    public interface IProjectDataAccess
    {
        public Project Create(Project project);
        public Project GetById(int id);
        public Project GetByName(string name);

        public IEnumerable<Project> GetAll(string token);

        public Project Update(int id, Project projectUpdated);

        public Project UpdateByName(string name, Project projectUpdated);

        public ResponseMessage Delete(int id);

        public ResponseMessage DeleteByName(string name);
        List<Bug> GetBugs(int id);
        public BugsQuantity GetBugsQuantity(int idProject);
        List<Developer> GetDevelopers(int id);
        List<Tester> GetTesters(int id);
        ResponseMessage RemoveTesterFromProject(int idProject, int idTester);
        ResponseMessage RemoveDeveloperFromProject(int idProject, int idDev);
        Tester AddTesterToProject(int idProject, int idTester);
        Developer AddDeveloperToProject(int idProject, int idDev);
        ProjectCost GetProjectCost(int id);
        ProjectDuration GetProjectDuration(int id);
    }
}