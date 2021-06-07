using System;
using System.Text;
using System.Web;
using System.Web.Mvc;
using EPiServer.Globalization;
using EPiServer.Logging;
using VisualCompareMode.Interfaces;
using VisualCompareMode.Models;

namespace VisualCompareMode.Controllers
{
    public class VisualCompareController : Controller
    {
        private readonly IEpiserverUiUrlHelper _episerverUiUrlHelper;
        private static readonly ILogger Logger = LogManager.GetLogger();

        public VisualCompareController(IEpiserverUiUrlHelper episerverUiUrlHelper)
        {
            _episerverUiUrlHelper = episerverUiUrlHelper;
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
            return new GetDiffBootstrapperModel()
            {
                EpiserverUiUrl  = _episerverUiUrlHelper.GetCmsSegments(),
                FallbackLanguage = ContentLanguage.Instance?.FinalFallbackCulture.TwoLetterISOLanguageName
            };
        }

        [HttpPost]
        [ValidateInput(false)]
        public string Index(string version1, string version2, string originalcontenttype)
        {
            version1 = HttpUtility.UrlDecode(Encoding.UTF8.GetString(Convert.FromBase64String(version1)));
            version2 = HttpUtility.UrlDecode(Encoding.UTF8.GetString(Convert.FromBase64String(version2)));

            HtmlDiff.HtmlDiff diffHelper = new HtmlDiff.HtmlDiff(version1, version2);
            var diffResult = diffHelper.Build();
            string cssLink =
                $"<link href=\"/{_episerverUiUrlHelper.GetEpiserverSegment()}/VisualCompareMode/ClientResources/Styles/DiffStyles.css\" media=\"all\" rel=\"stylesheet\" />";
            diffResult = diffResult.Replace("</head>", cssLink + "</head>");
            diffResult = diffResult.Replace("<del class='diffmod'><img",
                "<del class='diffmod diff-image-deleted'><img");
            diffResult = diffResult.Replace("<ins class='diffmod'><img",
                "<ins class='diffmod diff-image-inserted'><img");
            return diffResult;
        }
    }
}