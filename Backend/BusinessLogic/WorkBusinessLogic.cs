using BusinessLogicInterfaces;
using Domain;
using DTO;
using RepositoryInterfaces;
using System.Collections.Generic;

namespace BusinessLogic
{
    public class WorkBusinessLogic : IWorkBusinessLogic
    {
        public IWorkDataAccess WorkDataAccess { get; set; }

        public WorkBusinessLogic(IWorkDataAccess newWorkDataAccess)
        {
            WorkDataAccess = newWorkDataAccess;
        }

        public WorkDTO Add(WorkDTO workDTO)
        {
            Work work = workDTO.ConvertToDomain();
            work.Validate();
            WorkDataAccess.Create(work);
            return workDTO;
        }

        public Work GetById(int id)
        {
            Work work = WorkDataAccess.GetById(id);
            return work;
        }

        public IEnumerable<WorkDTO> GetAll(string token)
        {
            return WorkDataAccess.GetAll(token).ConvertAll(w => new WorkDTO(w));
        }
    }
}