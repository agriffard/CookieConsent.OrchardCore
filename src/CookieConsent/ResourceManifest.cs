using OrchardCore.ResourceManagement;

namespace CookieConsent.OrchardCore
{
    public class ResourceManifest : IResourceManifestProvider
    {
        public void BuildManifests(IResourceManifestBuilder builder)
        {
            var manifest = builder.Add();

            manifest
                .DefineScript("CookieConsent")
                .SetUrl("~/CookieConsent.OrchardCore/Scripts/cookieconsent.min.js", "~/CookieConsent.OrchardCore/Scripts/cookieconsent.js")
                .SetVersion("1.0.0");
        }
    }
}
