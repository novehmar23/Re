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
    public class BugTypeDataAccess : IBugTypeDataAccess
    {

        private readonly DbSet<BugType> bugsType;
        private readonly BugManagerContext context;

        public BugTypeDataAccess(DbContext newContext)
        {
            context = (BugManagerContext)newContext;
            bugsType = context.Set<BugType>();
        }

        public void Create(BugType bug)
        {
            if (bug is null)
            {
                throw new NonexistentBugException();
            }
            context.Add(bug);
            context.SaveChanges();
        
        }
    }
}
