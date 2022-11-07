using BusinessLogicInterfaces;
using Domain;
using DTO;
using RepositoryInterfaces;
using System.Collections.Generic;

namespace BusinessLogic
{
    public class DeveloperBusinessLogic : IDeveloperBusinessLogic
    {
        public IDeveloperDataAccess devDataAccess { get; set; }

        public DeveloperBusinessLogic(IDeveloperDataAccess newDevDataAccess)
        {
            devDataAccess = newDevDataAccess;
        }


        public DeveloperDTO Add(DeveloperDTO newDev)
        {
            Developer dev = newDev.ConvertToDomain();
            dev.Validate();
            List<string> allUsers = devDataAccess.GetAllUsernames();
            foreach (string u in allUsers)
            {
                if (u.Equals(newDev.Username))
                    throw new ValidationException();
            }
            devDataAccess.Create(dev);
            return newDev;
        }

        public int GetQuantityBugsResolved(int idDev)
        {
            return devDataAccess.GetQuantityBugsResolved(idDev);
        }

        public bool VerifyRole(string token)
        {
            return devDataAccess.VerifyRole(token);
        }

        public List<DeveloperDTO> GetAllDevs()
        {
            List<DeveloperDTO> devs = devDataAccess.GetAllDevs().ConvertAll(d => new DeveloperDTO(d));
            devs.ForEach(d => d.BugsResolved = GetQuantityBugsResolved(d.Id));
            return devs;
        }
    }


}