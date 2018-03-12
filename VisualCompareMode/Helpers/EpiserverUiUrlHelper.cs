using EPiServer.Configuration;
using EPiServer.ServiceLocation;
using VisualCompareMode.Interfaces;

namespace VisualCompareMode.Helpers
{
    [ServiceConfiguration(typeof(IEpiserverUiUrlHelper))]
    public class EpiserverUiUrlHelper : IEpiserverUiUrlHelper
    {
        public string GetCmsSegments()
        {
            return Settings.Instance.UIUrl.OriginalString.TrimStart("~/".ToCharArray()).TrimEnd("/".ToCharArray());
        }

        public string GetEpiserverSegment()
        {
            return Settings.Instance.UIUrl.OriginalString.TrimStart("~/".ToCharArray()).TrimEnd("/".ToCharArray())
                .Split("/".ToCharArray())[0];
        }
    }
}
