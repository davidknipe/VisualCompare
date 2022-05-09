using EPiServer.Configuration;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;

#if NET
using Microsoft.AspNetCore.Mvc;
#else
using System.Web.Mvc;
using System.Web.Routing;
#endif

namespace VisualCompareMode.Init
{
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class RegisterVisualCompareRoute : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
#if NET
#else
            var uiUrl = Settings.Instance.UIUrl.OriginalString.TrimStart("~/".ToCharArray()).TrimEnd("/".ToCharArray());

            //Register the route to hang off the Episerver UI Url to inherit any current security configs
            RouteTable.Routes.MapRoute(
                name: "VisualCompareRoute",
                url: uiUrl + "/VisualCompare/{action}",
                defaults: new { controller = "VisualCompare", action = "Index" }
            );
#endif
        }

        public void Uninitialize(InitializationEngine context) { }
    }
}