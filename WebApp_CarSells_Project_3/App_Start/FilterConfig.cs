using System.Web;
using System.Web.Mvc;

namespace WebApp_CarSells_Project_3
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
