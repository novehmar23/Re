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
    public class WorkDataAccess : IWorkDataAccess
    {
        private readonly DbSet<Work> works;
        private BugManagerContext context;

        public WorkDataAccess(DbContext newContext)
        {
            context = (BugManagerContext)newContext;
            works = context.Set<Work>();
        }

        public Work Create(Work work)
        {
            if (work is null)
            {
                throw new NonexistentWorkException();
            }

            context.Add(work);
            context.SaveChanges();

            return work;
        }

        public Work GetById(int id)
        {
            Work work;
            try
            {
                work = works.First(work => work.Id == id);
            }
            catch (InvalidOperationException e)
            {
                throw new NonexistentWorkException();
            }
            return work;
        }

        public List<Work> GetAll(string token)
        {
            LoginDataAccess loginDataAccess = new LoginDataAccess(context);
            TokenIdDTO idRole = loginDataAccess.GetIdRoleFromToken(token);
            List<Work> works = context.Works.Include(w=>w.Project).ThenInclude(p => p.Testers)
                .Include(w => w.Project).ThenInclude(p => p.Developers).ToList();
            works.ForEach(w => w.ProjectName = w.Project.Name);
            if (idRole.Role == Roles.Dev) return works.FindAll(b => b.Project.Developers.Exists(d => d.Id == idRole.Id));
            if (idRole.Role == Roles.Tester) return works.FindAll(b => b.Project.Testers.Exists(t => t.Id == idRole.Id));
            return works;
        }
    }
}