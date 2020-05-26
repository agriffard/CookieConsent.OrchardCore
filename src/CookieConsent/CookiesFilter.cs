using System.Threading.Tasks;
using CookieConsent.OrchardCore.Settings;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrchardCore.Admin;
using OrchardCore.Entities;
using OrchardCore.ResourceManagement;
using OrchardCore.Settings;

namespace CookieConsent.OrchardCore
{
    public class CookiesFilter : IAsyncResultFilter
    {
        private readonly IResourceManager _resourceManager;
        private readonly ISiteService _siteService;

        private HtmlString _scriptsCache;

        public CookiesFilter(
            IResourceManager resourceManager,
            ISiteService siteService)
        {
            _resourceManager = resourceManager;
            _siteService = siteService;
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            // Should only run on the front-end for a full view
            if ((context.Result is ViewResult || context.Result is PageResult)) // && !AdminAttribute.IsApplied(context.HttpContext))
            {
                if (_scriptsCache == null)
                {
                    var settings = (await _siteService.GetSiteSettingsAsync()).As<CookiesSettings>();

                    _scriptsCache = new HtmlString($"<script src=\"/CookieConsent.OrchardCore/Scripts/cookieconsent.js\"></script>\n<script>window.CookieConsent.init({{modalMainTextMoreLink:null,barTimeout:1000,theme:{{barColor:'{settings.BarColor}',barTextColor:'{settings.BarTextColor}',barMainButtonColor: '{settings.BarMainButtonColor}',barMainButtonTextColor:'{settings.BarMainButtonTextColor}',modalMainButtonColor: '{settings.ModalMainButtonColor}',modalMainButtonTextColor:'{settings.ModalMainButtonTextColor}',}}" +
                        $"{settings.Language}" +
                        $"{settings.Categories}" +
                        $"{settings.Services} " +
                        $"}});</script>");
                }

                if (_scriptsCache != null)
                {
                    _resourceManager.RegisterHeadScript(_scriptsCache);
                }
            }

            await next.Invoke();
        }
    }
}
