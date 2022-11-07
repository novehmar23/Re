using BusinessLogicInterfaces;
using Domain;
using RepositoryInterfaces;
using System.Collections.Generic;

namespace BusinessLogic
{
    public class AdminBusinessLogic : IAdminBusinessLogic
    {
        public IAdminDataAccess adminDataAccess { get; set; }

        public AdminBusinessLogic(IAdminDataAccess newAdminDataAccess)
        {
            adminDataAccess = newAdminDataAccess;
        }

        public Admin Add(Admin newAdmin)
        {
            newAdmin.Validate();
            List<string> allUsers = adminDataAccess.GetAllUsernames();
            foreach (string u in allUsers)
            {
                if (u.Equals(newAdmin.Username))
                    throw new ValidationException();
            }
            adminDataAccess.Create(newAdmin);
            return newAdmin;
        }

        public bool VerifyRole(string token)
        {
            return adminDataAccess.VerifyRole(token);
        }
    }


}