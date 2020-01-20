using System.Collections.Generic;
using MvvmHelpers;
using Akh.Breed.Models.NavigationMenu;

namespace Akh.Breed.Services.Navigation
{
    public interface IMenuProvider
    {
        ObservableRangeCollection<NavigationMenuItem> GetAuthorizedMenuItems(Dictionary<string, string> grantedPermissions);
    }
}
