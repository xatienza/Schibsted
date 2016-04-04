using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schibsted.Domain.Repository.Operations
{
    public interface IRepositoryTableOperations
    {
        bool Create (Model.Base.IRepositoryItem item);
        Model.Base.IRepositoryItem Read(long Id);
        bool Update(Model.Base.IRepositoryItem item);
        bool Delete(long Id);
        Model.Base.IRepositoryItem Find(Model.Base.IRepositoryItem item);
        List<Model.Base.IRepositoryItem> GetAll();
    }
}
