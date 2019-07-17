using System.Web;
using System.Web.Mvc;

namespace Practice_CRUD_without_Entity
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
