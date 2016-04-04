using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schibsted.Domain.Repository.MemoryRepository
{
    public class MemoryRepositoryDataBase: IMemoryRepositoryDataBase
    {
        #region Attributes
        private string name;
        private List<IMemoryRepositoryTable> avliableTables;
        #endregion

        #region Properties
        public string Name
        {
            get { return Name; }
        }

        public IEnumerable<IMemoryRepositoryTable> Tables
        {
            get { return avliableTables; }
        }
        #endregion

        #region Ctor

        public MemoryRepositoryDataBase(string DataBaseName)
        {
            name = DataBaseName;

            createTableRepository();
        }


        #endregion

        #region Private Methods

        private void createTableRepository()
        {
            avliableTables = new List<IMemoryRepositoryTable>();

        }

        private void checkTableRepository()
        {
            if (avliableTables == null)
                throw new NullReferenceException("Tables respository cannot be null");
        }

        #endregion

        public bool AddTable(IMemoryRepositoryTable NewTable)
        {
            var result = true;

            checkTableRepository();

            if (NewTable == null)
                throw new NullReferenceException("New table cannot be null");

            if (FindTable(NewTable.TableName) != null)
                result = false;
            else
                avliableTables.Add(item: NewTable);

            return result;
        }

        public bool DestroyTable(string TableName)
        {
            throw new NotImplementedException();
        }

        public bool ClearTable(string TableName)
        {
            throw new NotImplementedException();
        }

        public IMemoryRepositoryTable FindTable(string TableName)
        {

            checkTableRepository();

            var matchTable = avliableTables.Where(t => t.TableName == TableName).FirstOrDefault();

            return matchTable;
        }
    }
}
