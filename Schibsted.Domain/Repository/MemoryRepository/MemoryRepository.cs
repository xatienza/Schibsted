using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schibsted.Domain.Repository.MemoryRepository
{
    public class MemoryRepository: IMemoryRepository
    {
        #region Attributes
        private IMemoryRepositoryDataBase currentDataBase;
        #endregion

        #region Properties

        public bool HasDataBase
        {
            get { return (currentDataBase == null) ? false : true; }
        }

        public IMemoryRepositoryDataBase DataBase
        {
            get { return currentDataBase; }
        }

        #endregion

        public bool CreateDataBase(string DataBaseName)
        {
            var result = true;

            if (HasDataBase)
                return false;

            currentDataBase = new MemoryRepositoryDataBase(DataBaseName);

            return result;
        }

        public bool DestroyDataBase()
        {
            var result = true;

            if (!HasDataBase)
                return false;

            currentDataBase = null;

            return result;
        }

        public bool ClearDataBase()
        {
            throw new NotImplementedException();
        }


    }
}
