using System.Threading.Tasks;

namespace Akh.Breed.Net.Sms
{
    public interface ISms98Sender
    {
        Task SendAsync(string number, string message);
    }
}
