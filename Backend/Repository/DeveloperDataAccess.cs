using Domain;
using Microsoft.EntityFrameworkCore;
using RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class DeveloperDataAccess : UserDataAccess<Developer>, IDeveloperDataAccess
    {
        private readonly DbSet<Developer> devs;
        private DbSet<Tester> testers;
        private DbSet<Admin> admins;

        public DeveloperDataAccess(DbContext newContext) : base(newContext)
        {
            devs = context.Set<Developer>();
            users = devs;
            testers = context.Set<Tester>();
            admins = context.Set<Admin>();
        }

        public List<Developer> GetAllDevs()
        {
            return devs.Include(d => d.Projects).ThenInclude(p => p.Testers).ToList();
        }

        public List<string> GetAllUsernames()
        {
            List<User> listaTester = testers.Cast<User>().ToList();
            List<User> listaAdmins = admins.Cast<User>().ToList();
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

        public int GetQuantityBugsResolved(int idDev)
        {
            return context.Bugs.Count(b => b.CompletedById == idDev);
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

    }
}
