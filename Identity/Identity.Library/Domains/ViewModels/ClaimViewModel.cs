using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Library.Domains.ViewModels
{
    public class ClaimViewModel
    {
        public string Type { get; set; }
        public string Value { get; set; }

        public ClaimViewModel()
        {

        }
        public ClaimViewModel(string type, string value)
        {
            Type = type;
            Value = value;
        }
    }
}
