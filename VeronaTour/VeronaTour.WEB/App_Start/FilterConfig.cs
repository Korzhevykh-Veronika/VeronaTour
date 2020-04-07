using System.Web.Mvc;
using VeronaTour.WEB.Filters;

namespace VeronaTour.WEB
{
    /// <summary>
    ///     Configures application filteres
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        ///     Adds global filters, into an existing collection
        /// </summary>
        /// <param name="filters">global filters collection</param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new GlobalExceptionFilter());
        }
    }
}
