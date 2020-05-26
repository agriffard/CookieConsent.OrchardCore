using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.Modules;
using OrchardCore.Navigation;
using OrchardCore.ResourceManagement;
using OrchardCore.Security.Permissions;
using OrchardCore.Settings;
using CookieConsent.OrchardCore.Drivers;

namespace CookieConsent.OrchardCore
{
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IPermissionProvider, Permissions.Cookies>();
            services.AddScoped<IResourceManifestProvider, ResourceManifest>();
            services.AddScoped<IDisplayDriver<ISite>, CookiesSettingsDisplayDriver>();
            services.AddScoped<INavigationProvider, AdminMenu>();
            services.Configure<MvcOptions>((options) =>
            {
                options.Filters.Add(typeof(CookiesFilter));
            });
        }
    }
}