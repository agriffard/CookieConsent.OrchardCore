using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrchardCore.Security.Permissions;

namespace CookieConsent.OrchardCore
{
    public class Permissions
    {
        public static readonly Permission ManageCookies
            = new Permission(nameof(ManageCookies), "Manage Cookies settings");


        public class Cookies : IPermissionProvider
        {

            public Task<IEnumerable<Permission>> GetPermissionsAsync()
            {
                return Task.FromResult(new []
                {
                    ManageCookies
                }
                .AsEnumerable());
            }

            public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
            {
                yield return new PermissionStereotype
                {
                    Name = "Administrator",
                    Permissions = new []
                    {
                        ManageCookies
                    }
                };
            }
        }
    }
}
