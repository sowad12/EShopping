using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Core.Infrastructure.Options
{
    public class AppOptions
    {

        public string FrontendUrl { get; set; }

        public string BackendUrl { get; set; }
        public string PaymentProcessor { get; set; }
        public string ShopifyRedirectUrl { get; set; }
        public string ShopifyApiKey { get; set; }
        public string ShopifySecretKey { get; set; }


    }
}
