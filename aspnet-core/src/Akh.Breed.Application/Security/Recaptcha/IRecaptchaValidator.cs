using System.Threading.Tasks;

namespace Akh.Breed.Security.Recaptcha
{
    public interface IRecaptchaValidator
    {
        Task ValidateAsync(string captchaResponse);
    }
}
