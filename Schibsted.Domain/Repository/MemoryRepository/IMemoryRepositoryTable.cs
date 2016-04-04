using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schibsted.Domain.Repository.MemoryRepository
{
    public interface IMemoryRepositoryTable
    {
        #region Properties
        string TableName { get; }
        Operations.IRepositoryTableOperations Operations { get; }
        #endregion
    }
}
