using System.ComponentModel.DataAnnotations;

namespace CookieConsent.OrchardCore.ViewModels
{
    public class CookiesSettingsViewModel
    {
        public string BarColor { get; set; }
        public string BarTextColor { get; set; }
        public string BarMainButtonColor { get; set; }
        public string BarMainButtonTextColor { get; set; }
        public string ModalMainButtonColor { get; set; }
        public string ModalMainButtonTextColor { get; set; }
        public string Language { get; set; }
        public string Categories { get; set; }
        public string Services { get; set; }
    }
}
