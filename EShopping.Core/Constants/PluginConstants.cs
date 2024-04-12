

namespace EShopping.Core.Constants
{
    public class PluginConstants
    {
        public static string Success { get; set; } = "success";
        public static string Error { get; set; } = "error";
        public static string StaticDocumentPath { get; set; } = "Static";
        public static string SystemDefaultListName { get; set; } = "System Default";

        public static string ClientId { get; set; } = "Dg4ODg4ODg4COTgHBTg3ZjYCOzcKOQIJDQwOAg4FBQsCDQcJCTsHCjsKCjs5Ag4FCQwCBgoLDgg=";
        public static string ClientSecret { get; set; } = "EhISEhISEhIGPjk+Ogw+ZTQGCz8JOQYNDwkQBjoMDw4GDQkPEQoOPQo/Dg0/BhIJDRAGPA0KDBI";
        public static string RedirectUrl { get; set; } = "http://localhost:7020/autobill/auth";
        public static string BaseUrl { get; set; } = "https://plugin-bp.clubeez.com";
        //public static string BaseUrl { get; set; } = "https://localhost:7020";
        //public static OrderDetailsViewModel OrderDetails = new OrderDetailsViewModel();
        public static string PaymentProcessor { get; set; } = "Bitmascot Secure Pay";
        public static string AuthorizedSites { get; set; } = "";
        public static Int32 PageSize
        {

            get { return 20; }
        }

        public static Int32 PageSize10
        {

            get { return 10; }
        }

        public static Int32 PageSize12
        {

            get { return 12; }
        }

        public static Int32 PageSize15
        {

            get { return 15; }
        }

        public static Int32 PageSize20
        {

            get { return 20; }
        }
        public static Int32 PageSize100
        {

            get { return 100; }
        }
        public static Int32 PageSize200
        {

            get { return 200; }
        }
        public static Int32 PageSize500
        {

            get { return 500; }
        }
        public static Int32 PageSize1000
        {

            get { return 1000; }
        }
    }
}
