using BusinessLogicInterfaces;
using Domain;
using Domain.Utils;
using DTO;
using Microsoft.EntityFrameworkCore;
using RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class BugDataAccess : IBugDataAccess
    {
        private readonly DbSet<Bug> bugs;
        private readonly BugManagerContext context;

        public BugDataAccess(DbContext newContext)
        {
            context = (BugManagerContext)newContext;
            bugs = context.Set<Bug>();
        }

        public Bug Create(Bug bug)
        {

            if (bug is null)
            {
                throw new NonexistentBugException();
            }
            AddProjectIdToBug(bug);
            Console.WriteLine(bug);
            context.Add(bug);
            context.SaveChanges();

            return bug;
        }

        

        private void AddProjectIdToBug(Bug bug)
        {
            if ((bug.ProjectId == 0) && (bug.ProjectName != null))
            {
                Project bugsProject = context.Projects.First(p => p.Name == bug.ProjectName);
                if (bugsProject == null) throw new NonexistentProjectException();
                bug.ProjectId = bugsProject.Id;
            }
        }
        public Bug GetById(int id)
        {
            Bug bug;
            try
            {
                bug = bugs.Include("Project").First(bug => bug.Id == id);
            }
            catch (InvalidOperationException e)
            {
                throw new NonexistentBugException();
            }
            return bug;
        }

        public IEnumerable<Bug> GetAll(string token)
        {
            LoginDataAccess loginDataAccess = new LoginDataAccess(context);
            TokenIdDTO idRole = loginDataAccess.GetIdRoleFromToken(token);
            List<Bug> bugs = context.Bugs.Include("CompletedBy").Include(b => b.Project).ThenInclude(p => p.Testers)
                                                            .Include(b => b.Project).ThenInclude(p => p.Developers).ToList();
            if (idRole.Role == Roles.Dev) return bugs.FindAll(b => b.Project.Developers.Exists(d => d.Id == idRole.Id));
            if (idRole.Role == Roles.Tester) return bugs.FindAll(b => b.Project.Testers.Exists(t => t.Id == idRole.Id));
            return bugs;
        }

        public Bug Update(int Id, Bug bugUpdated)
        {
            if (bugUpdated is null)
                throw new NonexistentBugException();
            Bug bugToUpdate = GetById(Id);
            bugToUpdate.Name = bugUpdated.Name;
            bugToUpdate.Version = bugUpdated.Version;
            bugToUpdate.IsActive = bugUpdated.IsActive;
            bugToUpdate.ProjectName = bugUpdated.ProjectName;
            bugToUpdate.ProjectId = bugUpdated.ProjectId;
            bugToUpdate.CompletedBy = bugUpdated.CompletedBy;
            bugToUpdate.CompletedById = bugUpdated.CompletedById;
            bugToUpdate.Description = bugUpdated.Description;
            context.SaveChanges();
            return GetById(Id);
        }

        public ResponseMessage Delete(int id)
        {
            Bug bugDelete = GetById(id);
            bugs.Remove(bugDelete);
            context.SaveChanges();
            return new ResponseMessage("Deleted successfully");
        }

        public Bug ResolveBug(int id, string token)
        {
            Bug bug = GetById(id);
            LoginDataAccess login = new LoginDataAccess(context);
            TokenIdDTO idRole = login.GetIdRoleFromToken(token);
            bug.IsActive = false;
            bug.CompletedById = idRole.Id;
            context.SaveChanges();
            return bug;
        }

        public Bug UnresolveBug(int id, string token)
        {
            Bug bug = GetById(id);
            LoginDataAccess login = new LoginDataAccess(context);
            TokenIdDTO idRole = login.GetIdRoleFromToken(token);
            bug.IsActive = true;
            bug.CompletedById = null;
            context.SaveChanges();
            return bug;
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
