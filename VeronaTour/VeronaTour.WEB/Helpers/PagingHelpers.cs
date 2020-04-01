using System.Text;
using System.Web.Mvc;
using VeronaTour.WEB.Models;

namespace VeronaTour.WEB.Helpers
{
    /// <summary>
    ///     Helper for the Razor that allows to create pagination component
    /// </summary>
    public static class PagingHelpers
    {
        /// <summary>
        ///     Extension method that creates paginator for navigating between pages
        /// </summary>
        /// <param name="html">html helper</param>
        /// <param name="pageInfo">Nessesary information for creation of pagination component</param>
        /// <returns></returns>
        public static MvcHtmlString PageLinks(this HtmlHelper html,
            PageInfo pageInfo)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= pageInfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("onClick", "ChangePage(" + i + ")");
                tag.InnerHtml = i.ToString();
                if (i == pageInfo.PageNumber)
                {
                    tag.AddCssClass("selected");
                }
                tag.AddCssClass("btn page-link");
                result.Append(tag.ToString());
            }
            return MvcHtmlString.Create(result.ToString());
        }
    }
}