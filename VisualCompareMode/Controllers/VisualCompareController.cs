using System;
using System.Text;
using System.Web;
using EPiServer.Logging;
using VisualCompareMode.Models;
using EPiServer.Shell;
using EPiServer.Web.Routing;
using EPiServer.Core;
using EPiServer.Web;

#if NET
using Microsoft.AspNetCore.Mvc;
#else
using System.Web.Mvc;
#endif

namespace VisualCompareMode.Controllers
{
    public class VisualCompareController : Controller
    {
        private readonly UrlResolver _urlResolver;
        private static readonly ILogger Logger = LogManager.GetLogger();

        public VisualCompareController(UrlResolver urlResolver)
        {
            _urlResolver = urlResolver;
        }

        public ActionResult Index(ContentReference first, string firstlang, ContentReference second, string secondlang)
        {
            try
            {
                var virtualPathArguments = new VirtualPathArguments { ContextMode = ContextMode.Preview };
                var model = new GetDiffBootstrapperModel()
                {
                    FirstUrl = _urlResolver.GetUrl(first, firstlang, virtualPathArguments),
                    SecondUrl = _urlResolver.GetUrl(second, secondlang, virtualPathArguments),
                    CompareUrl = Url.Action("Index", "VisualCompare", RouteData),
                };
                return PartialView("~/modules/_protected/VisualCompareMode/Views/GetDiffBootstrapper.cshtml", model);
            }
            catch (Exception ex)
            {
                Logger.Error("Exception in VisualCompareController", ex);
                return PartialView(null);
            }
        }

        [HttpPost]
#if NETFRAMEWORK
        [ValidateInput(false)]
#endif
        public string Index(string version1, string version2, string originalcontenttype)
        {
            version1 = HttpUtility.UrlDecode(Encoding.UTF8.GetString(Convert.FromBase64String(version1)));
            version2 = HttpUtility.UrlDecode(Encoding.UTF8.GetString(Convert.FromBase64String(version2)));

            HtmlDiff.HtmlDiff diffHelper = new HtmlDiff.HtmlDiff(version1, version2);
            var diffResult = diffHelper.Build();
            string cssPath = Paths.ToClientResource("visualcomparemode", "Styles/DiffStyles.css");
            string cssLink =
                $"<link href=\"{cssPath}\" media=\"all\" rel=\"stylesheet\" />";
            diffResult = diffResult.Replace("</head>", cssLink + "</head>");
            diffResult = diffResult.Replace("<del class='diffmod'><img",
                "<del class='diffmod diff-image-deleted'><img");
            diffResult = diffResult.Replace("<ins class='diffmod'><img",
                "<ins class='diffmod diff-image-inserted'><img");
            return diffResult;
        }
    }
}