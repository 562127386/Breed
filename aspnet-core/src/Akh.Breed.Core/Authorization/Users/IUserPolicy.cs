using System.Threading.Tasks;
using Abp.Domain.Policies;

namespace Akh.Breed.Authorization.Users
{
    public interface IUserPolicy : IPolicy
    {
        Task CheckMaxUserCountAsync(int tenantId);
    }
}

