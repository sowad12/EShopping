using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Library.Manager.Interface
{
    public interface ISystemManager
    {
        public Task<int> Migrate();
        public Task<bool> SeedDefault();
    }
}
