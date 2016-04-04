using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schibsted.Domain.Repository.MemoryRepository
{
    public interface IMemoryRepositoryDataBase
    {
        #region Properties
        string Name { get; }
        IEnumerable<IMemoryRepositoryTable> Tables { get; }
        #endregion

        #region Public Methods
        bool AddTable(IMemoryRepositoryTable NewTable);
        bool DestroyTable(string TableName);
        bool ClearTable(string TableName);
        IMemoryRepositoryTable FindTable(string TableName);
        #endregion
    }
}
