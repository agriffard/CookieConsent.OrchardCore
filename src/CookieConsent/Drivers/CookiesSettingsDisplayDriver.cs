using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using OrchardCore.DisplayManagement.Entities;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Environment.Shell;
using CookieConsent.OrchardCore.Settings;
using CookieConsent.OrchardCore.ViewModels;
using OrchardCore.Settings;

namespace CookieConsent.OrchardCore.Drivers
{
    public class CookiesSettingsDisplayDriver : SectionDisplayDriver<ISite, CookiesSettings>
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IShellHost _shellHost;
        private readonly ShellSettings _shellSettings;

        public CookiesSettingsDisplayDriver(
            IAuthorizationService authorizationService,
            IHttpContextAccessor httpContextAccessor,
            IShellHost shellHost,
            ShellSettings shellSettings)
        {
            _authorizationService = authorizationService;
            _httpContextAccessor = httpContextAccessor;
            _shellHost = shellHost;
            _shellSettings = shellSettings;
        }

        public override async Task<IDisplayResult> EditAsync(CookiesSettings settings, BuildEditorContext context)
        {
            var user = _httpContextAccessor.HttpContext?.User;
            if (user == null || !await _authorizationService.AuthorizeAsync(user, Permissions.ManageCookies))
            {
                return null;
            }

            return Initialize<CookiesSettingsViewModel>("CookiesSettings_Edit", model =>
            {
                model.BarColor = settings.BarColor;
                model.BarTextColor = settings.BarTextColor;
                model.BarMainButtonColor = settings.BarMainButtonColor;
                model.ModalMainButtonColor = settings.ModalMainButtonColor;
                model.ModalMainButtonTextColor = settings.ModalMainButtonTextColor;
                model.Language = settings.Language;
                model.Categories = settings.Categories;
                model.Services = settings.Services;
            }).Location("Content:5").OnGroup(CookiesConstants.Features.Cookies);
        }

        public override async Task<IDisplayResult> UpdateAsync(CookiesSettings settings, BuildEditorContext context)
        {
            if (context.GroupId == CookiesConstants.Features.Cookies)
            {
                var user = _httpContextAccessor.HttpContext?.User;
                if (user == null || !await _authorizationService.AuthorizeAsync(user, Permissions.ManageCookies))
                {
                    return null;
                }

                var model = new CookiesSettingsViewModel();
                await context.Updater.TryUpdateModelAsync(model, Prefix);

                if (context.Updater.ModelState.IsValid)
                {
                    settings.BarColor = model.BarColor;
                    settings.BarTextColor = model.BarTextColor;
                    settings.BarMainButtonColor = model.BarMainButtonColor;
                    settings.ModalMainButtonColor = model.ModalMainButtonColor;
                    settings.ModalMainButtonTextColor = model.ModalMainButtonTextColor;
                    settings.Language = model.Language;
                    settings.Categories = model.Categories;
                    settings.Services = model.Services;
                }
            }
            return await EditAsync(settings, context);
        }
    }
}