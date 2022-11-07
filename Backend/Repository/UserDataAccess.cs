using Domain;
using Domain.Utils;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public abstract class UserDataAccess<T> where T : User
    {
        protected DbSet<T> users;
        protected readonly BugManagerContext context;

        protected UserDataAccess(DbContext newContext)
        {
            context = (BugManagerContext)newContext;
        }

        public T Create(T user)
        {

            if (user is null)
            {
                throw new NonexistentUserException();
            }
            context.Add(user);
            context.SaveChanges();

            return user;
        }

        public List<T> GetAll()
        {
            return users.ToList();
        }

        public bool VerifyRole(string token)
        {
            if (context.Sessions.FirstOrDefault(s => s.Token == token) == null) return false;
            string username = context.Sessions.FirstOrDefault(s => s.Token == token).Username;
            bool isRole = users.Any(u => u.Username == username);
            return isRole;
        }
    }
}
