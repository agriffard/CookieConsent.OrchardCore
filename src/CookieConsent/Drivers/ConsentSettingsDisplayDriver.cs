using CookieConsent.OrchardCore.Settings;
using CookieConsent.OrchardCore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using OrchardCore.DisplayManagement.Entities;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Settings;
using System.Threading.Tasks;

namespace CookieConsent.OrchardCore.Drivers
{
    public class ConsentSettingsDisplayDriver : SiteDisplayDriver<ConsentSettings>
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ConsentSettingsDisplayDriver(
            IAuthorizationService authorizationService,
            IHttpContextAccessor httpContextAccessor)
        {
            _authorizationService = authorizationService;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override string SettingsGroupId
            => CookieConsentConstants.Features.CookieConsent;

        public override async Task<IDisplayResult> EditAsync(ISite site, ConsentSettings settings, BuildEditorContext context)
        {
            var user = _httpContextAccessor.HttpContext?.User;
            if (user == null || !await _authorizationService.AuthorizeAsync(user, Permissions.ManageConsent))
            {
                return null;
            }

            return Initialize<ConsentSettingsViewModel>("ConsentSettings_Edit", model =>
            {
                model.BarColor = settings.BarColor;
                model.BarTextColor = settings.BarTextColor;
                model.BarMainButtonColor = settings.BarMainButtonColor;
                model.BarMainButtonTextColor = settings.BarMainButtonTextColor;
                model.ModalMainButtonColor = settings.ModalMainButtonColor;
                model.ModalMainButtonTextColor = settings.ModalMainButtonTextColor;
                model.Language = settings.Language;
                model.Categories = settings.Categories;
                model.Services = settings.Services;
            }).Location("Content:5").OnGroup(CookieConsentConstants.Features.CookieConsent);
        }

        public override async Task<IDisplayResult> UpdateAsync(ISite site, ConsentSettings settings, UpdateEditorContext context)
        {
            if (context.GroupId == CookieConsentConstants.Features.CookieConsent)
            {
                var user = _httpContextAccessor.HttpContext?.User;
                if (user == null || !await _authorizationService.AuthorizeAsync(user, Permissions.ManageConsent))
                {
                    return null;
                }

                var model = new ConsentSettingsViewModel();
                await context.Updater.TryUpdateModelAsync(model, Prefix);

                if (context.Updater.ModelState.IsValid)
                {
                    settings.BarColor = model.BarColor;
                    settings.BarTextColor = model.BarTextColor;
                    settings.BarMainButtonColor = model.BarMainButtonColor;
                    settings.BarMainButtonTextColor = model.BarMainButtonTextColor;
                    settings.ModalMainButtonColor = model.ModalMainButtonColor;
                    settings.ModalMainButtonTextColor = model.ModalMainButtonTextColor;
                    settings.Language = model.Language;
                    settings.Categories = model.Categories;
                    settings.Services = model.Services;
                }
            }
            return await EditAsync(site, settings, context);
        }
    }
}