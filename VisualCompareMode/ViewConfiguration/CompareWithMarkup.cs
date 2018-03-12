using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Shell;

namespace VisualCompareMode.ViewConfiguration
{
    [ServiceConfiguration(typeof(EPiServer.Shell.ViewConfiguration))]
    public class CompareWithMarkup : ViewConfiguration<IContentData>
    {
        public CompareWithMarkup()
        {
            Key = "compareWithMarkup";
            ControllerType = "epi-cms/compare/views/CompareView";
            ViewType = "visualcomparemode/compareWithMarkupView";
            IconClass = "epi-iconCompare";
            HideFromViewMenu = true;
        }
    }
}