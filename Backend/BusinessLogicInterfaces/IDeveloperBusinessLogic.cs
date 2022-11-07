using Domain;
using DTO;
using System.Collections.Generic;

namespace BusinessLogicInterfaces
{
    public interface IDeveloperBusinessLogic : IUserBusinessLogic<Developer>
    {
        DeveloperDTO Add(DeveloperDTO newTester);

        int GetQuantityBugsResolved(int idDev);
        List<DeveloperDTO> GetAllDevs();
    }
}

