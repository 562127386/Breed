using System.Threading.Tasks;
using Akh.Breed.Sessions.Dto;

namespace Akh.Breed.Web.Session
{
    public interface IPerRequestSessionCache
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformationsAsync();
    }
}

