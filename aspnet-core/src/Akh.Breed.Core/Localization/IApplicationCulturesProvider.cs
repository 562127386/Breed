using System.Globalization;

namespace Akh.Breed.Localization
{
    public interface IApplicationCulturesProvider
    {
        CultureInfo[] GetAllCultures();
    }
}
