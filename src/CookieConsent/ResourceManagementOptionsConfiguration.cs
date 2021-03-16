using Microsoft.Extensions.Options;
using OrchardCore.ResourceManagement;

namespace CookieConsent.OrchardCore
{
    public class ResourceManagementOptionsConfiguration : IConfigureOptions<ResourceManagementOptions>
    {
        private static ResourceManifest _manifest;

        static ResourceManagementOptionsConfiguration()
        {
            _manifest = new ResourceManifest();

            _manifest
                .DefineScript("CookieConsent")
                .SetUrl("~/CookieConsent.OrchardCore/Scripts/cookieconsent.min.js", "~/CookieConsent.OrchardCore/Scripts/cookieconsent.js")
                .SetVersion("1.0.0");
        }

        public void Configure(ResourceManagementOptions options)
        {
            options.ResourceManifests.Add(_manifest);
        }
    }
}
