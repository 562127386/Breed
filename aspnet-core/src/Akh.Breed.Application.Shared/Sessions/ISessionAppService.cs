using System.Threading.Tasks;
using Abp.Application.Services;
using Akh.Breed.Sessions.Dto;

namespace Akh.Breed.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();

        Task<UpdateUserSignInTokenOutput> UpdateUserSignInToken();
    }
}

