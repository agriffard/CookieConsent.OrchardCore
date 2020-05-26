using Microsoft.AspNetCore.Http;

namespace CookieConsent.OrchardCore.Settings
{
    public class CookiesSettings
    {
        public string BarColor { get; set; } = "#2C7CBF";
        public string BarTextColor { get; set; } = "#FFFFFF";
        public string BarMainButtonColor { get; set; } = "#FFFFFF";
        public string BarMainButtonTextColor { get; set; } = "#2C7CBF";
        public string ModalMainButtonColor { get; set; } = "#4285F4";
        public string ModalMainButtonTextColor { get; set; } = "#FFFFFF";
        public string Language { get; set; }
        public string Categories { get; set; }
        public string Services { get; set; }
    }
}
