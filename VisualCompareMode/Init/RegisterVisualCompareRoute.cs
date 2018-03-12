using System.Web.Mvc;
using System.Web.Routing;
using EPiServer.Configuration;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;

namespace VisualCompareMode.Init
{
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class RegisterVisualCompareRoute : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            var uiUrl = Settings.Instance.UIUrl.OriginalString.TrimStart("~/".ToCharArray()).TrimEnd("/".ToCharArray());

            //Register the route to hang off the Episerver UI Url to inherit any current security configs
            RouteTable.Routes.MapRoute(
                name: "VisualCompareRoute",
                url: uiUrl + "/VisualCompare/{action}",
                defaults: new { controller = "VisualCompare", action = "Index" }
            );
        }

        public void Uninitialize(InitializationEngine context) { }
    }
}