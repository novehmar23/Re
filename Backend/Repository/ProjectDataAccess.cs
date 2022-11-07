using BusinessLogicInterfaces;
using Domain;
using Domain.Utils;
using DTO;
using Microsoft.EntityFrameworkCore;
using RepositoryInterfaces;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class ProjectDataAccess : IProjectDataAccess
    {
        private readonly DbSet<Project> projects;
        private readonly BugManagerContext context;

        public ProjectDataAccess(DbContext newContext)
        {
            context = (BugManagerContext)newContext;
            projects = context.Set<Project>();
        }

        public Project Create(Project project)
        {
            if (project is null)
            {
                throw new NonexistentProjectException();
            }

            context.Add(project);
            context.SaveChanges();

            return project;
        }

        public ResponseMessage Delete(int id)
        {
            Project projectDelete = GetById(id);
            projects.Remove(projectDelete);
            context.SaveChanges();
            return new ResponseMessage("Deleted successfully");
        }

        public ResponseMessage DeleteByName(string name)
        {
            Project projectDelete = GetByName(name);
            projects.Remove(projectDelete);
            context.SaveChanges();
            return new ResponseMessage("Deleted successfully");
        }

        public IEnumerable<Project> GetAll(string token)
        {
            LoginDataAccess loginDataAccess = new LoginDataAccess(context);
            TokenIdDTO idRole = loginDataAccess.GetIdRoleFromToken(token);
            List<Project> projects = context.Projects.Include(p => p.Testers).Include(p => p.Developers).ToList();
            if (idRole.Role == Roles.Dev) return projects.FindAll(p => p.Developers.Exists(d => d.Id == idRole.Id));
            if (idRole.Role == Roles.Tester) return projects.FindAll(p => p.Testers.Exists(t => t.Id == idRole.Id));
            return projects;
        }


        public Project GetById(int id)
        {
            Project project = projects.FirstOrDefault(proj => proj.Id == id);
            if (project == null) throw new NonexistentProjectException();
            project = projects.Include("Developers").Include("Bugs").Include("Testers").Include("Works").First(proj => proj.Id == id);
            return project;
        }

        public Project GetByName(string name)
        {
            Project project = projects.First(proj => proj.Name == name);
            if (project == null) throw new NonexistentProjectException();
            return project;
        }

        public Project Update(int id, Project projectUpdated)
        {
            if (projects is null)
                throw new NonexistentProjectException();
            Project projectToUpdate = GetById(id);
            projectToUpdate.Name = projectUpdated.Name;
            context.SaveChanges();
            return projectToUpdate;
        }

        public Project UpdateByName(string name, Project projectUpdated)
        {
            if (projects is null)
                throw new NonexistentProjectException();
            Project projectToUpdate = GetByName(name);
            projectToUpdate.Name = projectUpdated.Name;
            projectToUpdate.Testers = projectUpdated.Testers;
            projectToUpdate.Developers = projectUpdated.Developers;
            projectToUpdate.Bugs = projectUpdated.Bugs;
            context.SaveChanges();
            return projectToUpdate;
        }


        public List<Bug> GetBugs(int id)
        {
            return GetById(id).Bugs;
        }

        public BugsQuantity GetBugsQuantity(int idProject)
        {
            var quantity = GetBugs(idProject).Count();
            return new BugsQuantity(quantity);
        }

        public List<Developer> GetDevelopers(int id)
        {
            return GetById(id).Developers;
        }

        public List<Tester> GetTesters(int id)
        {
            return GetById(id).Testers;
        }

        public ResponseMessage RemoveTesterFromProject(int idProject, int idTester)
        {
            Project project = GetById(idProject);
            Tester tester = context.Tester.FirstOrDefault(t => t.Id == idTester);
            if (tester == null) throw new NonexistentUserException();
            project.Testers.Remove(tester);
            context.SaveChanges();
            return new ResponseMessage("Removed tester from project");
        }

        public ResponseMessage RemoveDeveloperFromProject(int idProject, int idDev)
        {
            Project project = GetById(idProject);
            Developer dev = context.Developer.FirstOrDefault(t => t.Id == idDev);
            if (dev == null) throw new NonexistentUserException();
            project.Developers.Remove(dev);
            context.SaveChanges();
            return new ResponseMessage("Removed developer from project");
        }


        public Tester AddTesterToProject(int idProject, int idTester)
        {
            Project project = GetById(idProject);
            Tester tester = context.Tester.FirstOrDefault(t => t.Id == idTester);
            if (tester == null) throw new NonexistentUserException();
            project.Testers.Add(tester);
            context.SaveChanges();
            return tester;

        }

        public Developer AddDeveloperToProject(int idProject, int idDev)
        {
            Project project = GetById(idProject);
            Developer dev = context.Developer.FirstOrDefault(t => t.Id == idDev);
            if (dev == null) throw new NonexistentUserException();
            project.Developers.Add(dev);
            context.SaveChanges();
            return dev;

        }

        public ProjectCost GetProjectCost(int id)
        {
            Project project = GetById(id);
            int workCost = project.Works.Sum(w => w.Time * w.Cost);
            int bugsDuration = project.Bugs.Sum(b => b.Time * (b.IsActive ? 0 : 1));
            int bugsCostTesters = project.Testers.Sum(t => t.Cost * bugsDuration);
            int bugsCostDevs = project.Developers.Sum(t => t.Cost * bugsDuration);
            int totalCost = workCost + bugsCostTesters + bugsCostDevs;
            return new ProjectCost(totalCost);
        }

        public ProjectDuration GetProjectDuration(int id)
        {
            Project project = GetById(id);
            int bugsDuration = project.Bugs.Sum(b => b.Time * (b.IsActive ? 0 : 1));
            int workDuration = project.Works.Sum(w => w.Time);
            int totalDuration = bugsDuration + workDuration;
            return new ProjectDuration(totalDuration);
        }
    }
}