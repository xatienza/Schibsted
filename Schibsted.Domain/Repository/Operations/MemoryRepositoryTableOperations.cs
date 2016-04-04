using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schibsted.Domain.Repository.Operations
{
    public class MemoryRepositoryTableOperations: IRepositoryTableOperations
    {
        #region attributes
        private List<Model.Base.IRepositoryItem> avaliableRows = null;
        #endregion

        #region Ctor
        public MemoryRepositoryTableOperations(List<Model.Base.IRepositoryItem> rows)
        {
            setRows(rows);
        }

        #endregion

        #region private Methods
        private void setRows(List<Model.Base.IRepositoryItem> rows)
        {
            avaliableRows = rows; 
        }

        private void checkRepositoryIntegrity(Model.Base.IRepositoryItem item)
        {
            //check method integrity
            if (item == null)
                throw new NullReferenceException("Repository item cannot be null");

            if (avaliableRows == null)
                throw new NullReferenceException("Rows cannot be null");
        }

        private int getNexRowId()
        {
            if (avaliableRows == null)
                throw new NullReferenceException("Repository item cannot be null");

            var nextId = avaliableRows.Count() + 1;

            return nextId;
        }

        #endregion

        #region public Methods
        public bool Create(Model.Base.IRepositoryItem item)
        {
            bool result = true;

            checkRepositoryIntegrity(item);

            item.Id = getNexRowId();

            //add item to collection
            avaliableRows.Add(item);

            return result;
        }

        public Model.Base.IRepositoryItem Read(long Id)
        {
            if (avaliableRows == null)
                throw new NullReferenceException("Rows cannot be null");

            var item = avaliableRows.Where(i => i.Id == Id).FirstOrDefault();

            return item;

        }

        public bool Update(Model.Base.IRepositoryItem item)
        {
             bool result = true;

            checkRepositoryIntegrity(item);

            var existsItem = Find(item);

            if (existsItem == null)
                throw new Exception("Item does no exists");
            else
            {
                result = Delete(item.Id);

                if (result)
                    result = Create(item);
            }

            return result;
        }

        public bool Delete(long Id)
        {
            bool result = true;
            
            var item = avaliableRows.Where(i => i.Id == Id).FirstOrDefault();
            
            checkRepositoryIntegrity(item);

            result = avaliableRows.Remove(item);

            return result;
        }

        public Model.Base.IRepositoryItem Find(Model.Base.IRepositoryItem item)
        {
            checkRepositoryIntegrity(item);

            var matchItem = avaliableRows.Where(i => i.Id == item.Id).FirstOrDefault();

            return matchItem;
        }

        public List<Model.Base.IRepositoryItem> GetAll()
        {
            if (avaliableRows == null)
                throw new NullReferenceException("Rows cannot be null");

            return avaliableRows;
        }

        #endregion
    }
}
