using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schibsted.Domain.Repository.MemoryRepository
{
    public class MemoryRepositoryTable: IMemoryRepositoryTable
    {
        #region Attributes
        private string tableName;
        private Operations.IRepositoryTableOperations avaliableOperations = null;
        #endregion

        #region Properties

        public string TableName
        {
            get { return this.tableName; }
        }

        public Operations.IRepositoryTableOperations Operations
        {
            get { return this.avaliableOperations; }
        }

        #endregion


        private void setMemoryRepositoryOperations(Operations.IRepositoryTableOperations operations)
        {
            if (operations == null)
                throw new NullReferenceException("Repository operations are neeeded");

            avaliableOperations = operations;
        }

        #region Ctor

        public MemoryRepositoryTable(string TableName, Operations.IRepositoryTableOperations operations)
        {
            tableName = TableName;

            //Set the repository avaliable operations
            setMemoryRepositoryOperations(operations);

        }

        #endregion

    }
}
