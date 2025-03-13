using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Core.Infrastructure.Options
{
    public class CorsOptions
    {
        public string[] Origins { get; set; }
        public string PolicyName { get; set; }
    }
}
