using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Core.Infrastructure.Options
{
    public  class DatabaseOptions
    {
        public string ConnectionString { get; set; }
        public string SecretKey { get; set; }
    }
}
