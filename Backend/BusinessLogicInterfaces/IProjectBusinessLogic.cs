using DTO;
using System.Collections.Generic;

namespace BusinessLogicInterfaces
{
    public interface IProjectBusinessLogic
    {
        IEnumerable<ProjectDTO> GetAll(string token);
        ProjectDTO Add(ProjectDTO projectDTO);
        ProjectDTO GetById(int Id);
        ProjectDTO Update(int Id, ProjectDTO projectDTO);
        ResponseMessage Delete(int Id);

        ProjectDTO GetByName(string name);

        ProjectDTO UpdateByName(string name, ProjectDTO projectUpdated);

        ResponseMessage DeleteByName(string name);
        List<BugDTO> GetBugs(int id);
        BugsQuantity GetBugsQuantity(int idProject);
        List<DeveloperDTO> GetDevelopers(int id);
        List<TesterDTO> GetTesters(int id);
        ResponseMessage RemoveDeveloperFromProject(int idproject, int idDev);
        ResponseMessage RemoveTesterFromProject(int idproject, int idTester);
        DeveloperDTO AddDeveloperToProject(int idproject, int idDev);
        TesterDTO AddTesterToProject(int idproject, int idTester);
        ProjectCost GetProjectCost(int id);
        ProjectDuration GetProjectDuration(int id);
    }
}

