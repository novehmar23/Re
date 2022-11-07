using Domain;
using Microsoft.EntityFrameworkCore;
using RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class AdminDataAccess : UserDataAccess<Admin>, IAdminDataAccess
    {
        private DbSet<Developer> devs;
        private DbSet<Tester> testers;
        public AdminDataAccess(DbContext newContext) : base(newContext)
        {
            users = context.Set<Admin>();
            devs = context.Set<Developer>();
            testers = context.Set<Tester>();
        }

        public List<User> GetAll()
        {
            List<User> allUsers = users.ToList<User>();
            return allUsers;
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

        private List<string> GetUsernames(List<User> users)
        {
            List<string> usernames = new List<string>();
            foreach (User u in users)
            {
                usernames.Add(u.Username);
            }
            return usernames;
        }

        private void ConcatListUsers(List<User> listUser, List<User> listaAdmins, List<User> listaDeveloper, List<User> listaTester)
        {
            listUser.AddRange(listaAdmins);
            listUser.AddRange(listaDeveloper);
            listUser.AddRange(listaTester);
        }
    }
}
