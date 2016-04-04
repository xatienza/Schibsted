using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schibsted.Domain.Model.Security.Users
{
    public class User : Domain.Model.Base.IRepositoryItem
    {
        #region Attributes
        private string password;
        #endregion

        #region Properties

        public long Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public List<Domain.Model.Base.IRepositoryItem> Roles { get; set; }

        #endregion
    }
}
