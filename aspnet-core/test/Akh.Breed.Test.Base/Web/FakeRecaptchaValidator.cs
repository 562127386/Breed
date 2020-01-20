using System.Threading.Tasks;
using Akh.Breed.Security.Recaptcha;

namespace Akh.Breed.Test.Base.Web
{
    public class FakeRecaptchaValidator : IRecaptchaValidator
    {
        public Task ValidateAsync(string captchaResponse)
        {
            return Task.CompletedTask;
        }
    }
}

