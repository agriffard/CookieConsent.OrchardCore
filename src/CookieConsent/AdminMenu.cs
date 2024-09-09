using CookieConsent.OrchardCore.Drivers;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Localization;
using OrchardCore.Navigation;
using System.Threading.Tasks;

namespace CookieConsent.OrchardCore
{
    public class AdminMenu : AdminNavigationProvider
    {
        private static readonly RouteValueDictionary _routeValues = new()
        {
            { "area", "OrchardCore.Settings" },
            { "groupId", CookieConsentConstants.Features.CookieConsent },
        };

        internal readonly IStringLocalizer S;

        public AdminMenu(IStringLocalizer<AdminMenu> localizer)
        {
            S = localizer;
        }

        protected override ValueTask BuildAsync(NavigationBuilder builder)
        {
            builder
                .Add(S["Configuration"], configuration => configuration
                    .Add(S["Settings"], settings => settings
                        .Add(S["Consent"], S["Consent"].PrefixPosition(), consent => consent
                            .AddClass("consent").Id("consent")
                            .Id("admin")
                            .Action("Index", "Admin", _routeValues)
                            .Permission(Permissions.ManageConsent)
                            .LocalNav())
                ));

            return ValueTask.CompletedTask;
        }
    }
}
