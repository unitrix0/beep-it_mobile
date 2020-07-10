using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Navigation;

namespace Mobile.Helpers
{
    static class NavigationExtensions
    {
        public static Task<INavigationResult> NavigateAbsolutAsync(this INavigationService navService, params string[] pathElements)
        {
            var path = string.Join("/", pathElements);
            return navService.NavigateAsync($"/{path}");
        }

        public static Task<INavigationResult> NavigateAsync(this INavigationService navService, params string[] pathElements)
        {
            var path = string.Join("/", pathElements);
            return navService.NavigateAsync($"{path}");
        }
        
        
    }
}
