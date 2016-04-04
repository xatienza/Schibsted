using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schibsted.Domain.Repository
{
    public class Repository : Schibsted.Domain.Repository.IRepository
    {
        #region Attributes
        private Model.Enums.RepositoryKind currentRepositoryKind;
        private IRepositoryEngine currentRepositoryEngine;
        #endregion

        #region Properties

        public Model.Enums.RepositoryKind Kind
        {
            get { return currentRepositoryKind; }
        }

        public IRepositoryEngine Engine
        {
            get { return currentRepositoryEngine; }
        }

        #endregion

        #region Public Methods

        public bool SetEngine(Model.Enums.RepositoryKind kind, IRepositoryEngine value)
        {
            var result = true;

            currentRepositoryKind = kind;
            currentRepositoryEngine = value;

            return result;
        }

        #endregion
    }
}
