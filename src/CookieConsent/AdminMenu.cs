using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using OrchardCore.Environment.Shell.Descriptor.Models;
using OrchardCore.Navigation;

namespace CookieConsent.OrchardCore
{
    public class AdminMenu : INavigationProvider
    {
        private readonly ShellDescriptor _shellDescriptor;
        private readonly IStringLocalizer S;

        public AdminMenu(
            IStringLocalizer<AdminMenu> localizer,
            ShellDescriptor shellDescriptor)
        {
            S = localizer;
            _shellDescriptor = shellDescriptor;
        }

        public Task BuildNavigationAsync(string name, NavigationBuilder builder)
        {
            if (String.Equals(name, "admin", StringComparison.OrdinalIgnoreCase))
            {
                builder.Add(S["Configuration"], configuration => configuration
                        .Add(S["Settings"], settings => settings
                        .Add(S["Consent"], consent => consent
                            .AddClass("consent").Id("consent")
                            .Action("Index", "Admin", new { area = "OrchardCore.Settings", groupId = ConsentConstants.Features.Consent })
                            .LocalNav())
                    ));
            }
            return Task.CompletedTask;
        }
    }
}
