using Domain;
using System;
using System.Collections.Generic;

namespace RepositoryDataAccess
{
    public interface IBugDataAccess
    {
        public Bug Create(Bug bug);
        public Bug GetById(int id);
        public IEnumerable<Bug> GetAll();
        public void Update(Bug bugUpdated);
        public void Delete(Bug bugToDelete);
    }
}
