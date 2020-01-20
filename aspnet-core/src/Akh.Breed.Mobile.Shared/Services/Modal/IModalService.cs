using System.Threading.Tasks;
using Akh.Breed.Views;
using Xamarin.Forms;

namespace Akh.Breed.Services.Modal
{
    public interface IModalService
    {
        Task ShowModalAsync(Page page);

        Task ShowModalAsync<TView>(object navigationParameter) where TView : IXamarinView;

        Task<Page> CloseModalAsync();
    }
}

