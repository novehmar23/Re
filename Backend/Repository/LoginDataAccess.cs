using Domain;
using Domain.Utils;
using DTO;
using Microsoft.EntityFrameworkCore;
using RepositoryInterfaces;
using System.Linq;

namespace Repository
{
    public class LoginDataAccess : ILoginDataAccess
    {
        private readonly BugManagerContext context;

        public LoginDataAccess(DbContext newContext)
        {
            context = (BugManagerContext)newContext;
        }

        public TokenIdDTO GetIdRoleFromToken(string token)
        {
            if (context.Sessions.FirstOrDefault(s => s.Token == token) == null) return null;
            string username = context.Sessions.FirstOrDefault(s => s.Token == token).Username;

            int Id = 0;
            string role = null;
            if (context.Admins.FirstOrDefault(u => u.Username == username) != null)
            {
                role = Roles.Admin;
                Id = context.Admins.FirstOrDefault(u => u.Username == username).Id;
            }
            else if (context.Developer.FirstOrDefault(u => u.Username == username) != null)
            {
                role = Roles.Dev;
                Id = context.Developer.FirstOrDefault(u => u.Username == username).Id;
            }
            else if (context.Tester.FirstOrDefault(u => u.Username == username) != null)
            {
                role = Roles.Tester;
                Id = context.Tester.FirstOrDefault(u => u.Username == username).Id;
            }
            return new TokenIdDTO() { Id = Id, Role = role };
        }

        public void SaveLogin(LoginToken loginToken)
        {
            context.Add(loginToken);
            context.SaveChanges();
        }

        public string VerifyUser(string username, string password)
        {
            string role = null;
            if (context.Admins.Any(u => u.Username == username && u.Password == password))
            {
                role = Roles.Admin;
            }
            else if (context.Developer.Any(u => u.Username == username && u.Password == password))
            {
                role = Roles.Dev;
            }
            else if (context.Tester.Any(u => u.Username == username && u.Password == password))
            {
                role = Roles.Tester;
            }
            return role;
        }


    }
}
