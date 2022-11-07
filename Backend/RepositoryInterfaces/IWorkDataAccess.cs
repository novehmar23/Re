using Domain;
using System.Collections.Generic;

namespace RepositoryInterfaces
{
    public interface IWorkDataAccess
    {
        Work Create(Work work);
        Work GetById(int id);
        List<Work> GetAll(string token);
    }
}