using System.Threading.Tasks;
using Abp.Application.Services;

namespace Akh.Breed.BaseInfos
{
    public interface IStateImportAppService : IApplicationService
    {
        void InitialData();
    }
}