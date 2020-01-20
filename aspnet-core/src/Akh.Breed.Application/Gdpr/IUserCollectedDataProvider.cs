using System.Collections.Generic;
using System.Threading.Tasks;
using Abp;
using Akh.Breed.Dto;

namespace Akh.Breed.Gdpr
{
    public interface IUserCollectedDataProvider
    {
        Task<List<FileDto>> GetFiles(UserIdentifier user);
    }
}

