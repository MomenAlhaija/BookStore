using System.Web;
using System.Web.Mvc;

namespace Pio_Tech_School_Managment
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
