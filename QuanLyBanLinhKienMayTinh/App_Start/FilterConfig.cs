using System.Web;
using System.Web.Mvc;

namespace QuanLyBanLinhKienMayTinh
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
