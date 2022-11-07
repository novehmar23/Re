using Domain;
using Microsoft.EntityFrameworkCore;
using RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class TesterDataAccess : UserDataAccess<Tester>, ITesterDataAccess
    {
        private readonly DbSet<Tester> testers;
        private DbSet<Developer> devs;
        private DbSet<Admin> admins;

        public TesterDataAccess(DbContext newContext) : base(newContext)
        {
            testers = context.Set<Tester>();
            users = testers;
            devs = context.Set<Developer>();
            admins = context.Set<Admin>();
        }

        public List<Tester> GetAllTesters()
        {
            return testers.ToList();
        }

        public List<string> GetAllUsernames()
        {
            List<User> listaTester = testers.Cast<User>().ToList();
            List<User> listaAdmins = users.Cast<User>().ToList();
            List<User> listaDeveloper = devs.Cast<User>().ToList();
            List<User> listUser = new List<User>();

            ConcatListUsers(listUser, listaAdmins, listaDeveloper, listaTester);
            List<string> listUsernames = GetUsernames(listUser);

            return listUsernames;
        }

        private void ConcatListUsers(List<User> listUser, List<User> listaAdmins, List<User> listaDeveloper, List<User> listaTester)
        {
            listUser.AddRange(listaAdmins);
            listUser.AddRange(listaDeveloper);
            listUser.AddRange(listaTester);
        }

        private List<string> GetUsernames(List<User> users)
        {
            List<string> usernames = new List<string>();
            foreach (User u in users)
            {
                usernames.Add(u.Username);
            }
            return usernames;
        }

        public List<Bug> GetBugsByName(int idTester, string filter)
        {
            Tester tester = testers.FirstOrDefault(t => t.Id == idTester);
            List<Bug> filteredBugs = new List<Bug>();
            foreach (Project project in tester.Projects)
            {
                foreach (Bug bug in project.Bugs)
                {
                    if (bug.Name == filter)
                    {
                        filteredBugs.Add(bug);
                    }
                }
            }

            return filteredBugs;
        }

        public List<Bug> GetBugsByProject(int idTester, int filter)
        {
            Tester tester = testers.Include("Projects").FirstOrDefault(t => t.Id == idTester);
            List<Bug> filteredBugs = new List<Bug>();
            foreach (Project project in tester.Projects)
            {
                foreach (Bug bug in project.Bugs)
                {
                    if (bug.ProjectId == filter)
                    {
                        filteredBugs.Add(bug);
                    }
                }
            }
            return filteredBugs;
        }

        public List<Bug> GetBugsByStatus(int idTester, bool filter)
        {
            Tester tester = testers.FirstOrDefault(t => t.Id == idTester);
            List<Bug> filteredBugs = new List<Bug>();
            foreach (Project project in tester.Projects)
            {
                foreach (Bug bug in project.Bugs)
                {
                    if (bug.IsActive == filter)
                    {
                        filteredBugs.Add(bug);
                    }
                }
            }
            return filteredBugs;
        }
    }
}
