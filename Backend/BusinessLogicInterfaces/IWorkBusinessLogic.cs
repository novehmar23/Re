using Domain;
using DTO;
using System.Collections.Generic;

namespace BusinessLogicInterfaces
{
    public interface IWorkBusinessLogic
    {
        IEnumerable<WorkDTO> GetAll(string token);
        WorkDTO Add(WorkDTO t);
        Work GetById(int Id);
    }
}