using Schibsted.Domain.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schibsted.Domain.Repository.MemoryRepository
{
    public interface IMemoryRepository : IRepositoryEngine
    {
        bool HasDataBase {get;}

        IMemoryRepositoryDataBase DataBase { get; }

    }
}
