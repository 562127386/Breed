using System;
using Akh.Breed.Core;
using Akh.Breed.Core.Dependency;
using Akh.Breed.Services.Permission;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Akh.Breed.Extensions.MarkupExtensions
{
    [ContentProperty("Text")]
    public class HasPermissionExtension : IMarkupExtension
    {
        public string Text { get; set; }
        
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (ApplicationBootstrapper.AbpBootstrapper == null || Text == null)
            {
                return false;
            }

            var permissionService = DependencyResolver.Resolve<IPermissionService>();
            return permissionService.HasPermission(Text);
        }
    }
}
