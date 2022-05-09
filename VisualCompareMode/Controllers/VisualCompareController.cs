using System;
using System.Text;
using System.Web;
using EPiServer.Globalization;
using EPiServer.Logging;
using VisualCompareMode.Models;
using EPiServer.Shell;
using EPiServer;

#if NET
using Microsoft.AspNetCore.Mvc;
#else
using System.Web.Mvc;
#endif

namespace VisualCompareMode.Controllers
{
    public class VisualCompareController : Controller
    {
        private static readonly ILogger Logger = LogManager.GetLogger();

        public VisualCompareController()
        {
        }

        public ActionResult Index()
        {
            try
            {
                return PartialView("~/modules/_protected/VisualCompareMode/Views/GetDiffBootstrapper.cshtml",
                    GetDiffModel());
            }
            catch (Exception ex)
            {
                Logger.Error("Exception in VisualCompareController", ex);
                return PartialView(null);
            }
        }

        private GetDiffBootstrapperModel GetDiffModel()
        {
#if NET
            var fallbackCulture = ContentLanguage.PreferredCulture;
#else
            var fallbackCulture = ContentLanguage.Instance?.FinalFallbackCulture;
#endif
            return new GetDiffBootstrapperModel()
            {
                EpiserverUiUrl  = UriSupport.UIUrl.OriginalString,
                FallbackLanguage = fallbackCulture?.TwoLetterISOLanguageName
            };
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