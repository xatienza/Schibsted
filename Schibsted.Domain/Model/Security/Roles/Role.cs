using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schibsted.Domain.Model.Security.Roles
{
    public class Role: Base.IRepositoryItem
    {
        public long Id { get; set; }

        public string Name { get; set; }
    }
}
