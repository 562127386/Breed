using System.Threading.Tasks;

namespace Akh.Breed.Net.Sms
{
    public interface ISmsSender
    {
        Task SendAsync(string number, string message);
    }
}
