using BusinessLogicInterfaces;
using Domain;
using DTO;
using RepositoryInterfaces;
using System.Collections.Generic;

namespace BusinessLogic
{
    public class ProjectBusinessLogic : IProjectBusinessLogic
    {
        public IProjectDataAccess projectDataAccess { get; set; }

        public ProjectBusinessLogic(IProjectDataAccess newProjectDataAccess)
        {
            projectDataAccess = newProjectDataAccess;
        }

        public ProjectDTO Add(ProjectDTO projectDTO)
        {
            Project project = projectDTO.ConvertToDomain();
            project.Validate();
            return new ProjectDTO(projectDataAccess.Create(project));
        }

        public ResponseMessage Delete(int Id)
        {
            return projectDataAccess.Delete(Id);
        }

        public IEnumerable<ProjectDTO> GetAll(string token)
        {
            List<Project> projects = (List<Project>)projectDataAccess.GetAll(token);
            List<ProjectDTO> projectsDTO = projects.ConvertAll(p => new ProjectDTO(p));
            projectsDTO.ForEach(p => p.TotalCost = GetProjectCost(p.Id).Cost);
            projectsDTO.ForEach(p => p.TotalDuration = GetProjectDuration(p.Id).Duration);
            projectsDTO.ForEach(p => p.BugsQuantity = GetBugsQuantity(p.Id).Quantity);
            return projectsDTO;
        }

        public ProjectDTO GetById(int Id)
        {
            ProjectDTO project = new ProjectDTO(projectDataAccess.GetById(Id));
            project.BugsQuantity = GetBugsQuantity(Id).Quantity;
            project.TotalCost = GetProjectCost(Id).Cost;
            project.TotalDuration = GetProjectDuration(Id).Duration;
            return project;
        }

        public ProjectDTO Update(int Id, ProjectDTO projectModified)
        {
            Project project = projectModified.ConvertToDomain();
            project.Validate();
            return new ProjectDTO(projectDataAccess.Update(Id, project));
        }

        public ResponseMessage DeleteByName(string nameProject)
        {
            return projectDataAccess.DeleteByName(nameProject);
        }

        public ProjectDTO GetByName(string nameProject)
        {
            return new ProjectDTO(projectDataAccess.GetByName(nameProject));
        }

        public ProjectDTO UpdateByName(string nameProjectToUpdate, ProjectDTO projectModified)
        {
            Project project = projectModified.ConvertToDomain();
            project.Validate();
            return new ProjectDTO(projectDataAccess.UpdateByName(nameProjectToUpdate, project));
        }

        public List<BugDTO> GetBugs(int id)
        {
            return projectDataAccess.GetBugs(id).ConvertAll(b => new BugDTO(b));
        }

        public BugsQuantity GetBugsQuantity(int idProject)
        {
            return projectDataAccess.GetBugsQuantity(idProject);
        }

        public List<DeveloperDTO> GetDevelopers(int id)
        {
            return projectDataAccess.GetDevelopers(id).ConvertAll(d => new DeveloperDTO(d));
        }

        public List<TesterDTO> GetTesters(int id)
        {
            return projectDataAccess.GetTesters(id).ConvertAll(t => new TesterDTO(t));
        }

        public ResponseMessage RemoveDeveloperFromProject(int idproject, int idDev)
        {
            return projectDataAccess.RemoveDeveloperFromProject(idproject, idDev);

        }

        public ResponseMessage RemoveTesterFromProject(int idproject, int idTester)
        {
            return projectDataAccess.RemoveTesterFromProject(idproject, idTester);

        }

        public DeveloperDTO AddDeveloperToProject(int idproject, int idDev)
        {
            return new DeveloperDTO(projectDataAccess.AddDeveloperToProject(idproject, idDev));

        }

        public TesterDTO AddTesterToProject(int idproject, int idTester)
        {
            return new TesterDTO(projectDataAccess.AddTesterToProject(idproject, idTester));

        }

        public ProjectCost GetProjectCost(int id)
        {
            return projectDataAccess.GetProjectCost(id);
        }

        public ProjectDuration GetProjectDuration(int id)
        {
            return projectDataAccess.GetProjectDuration(id);
        }
    }
}