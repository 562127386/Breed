using System.Collections.Generic;
using Akh.Breed.Authorization.Users.Dto;
using Akh.Breed.Dto;

namespace Akh.Breed.Authorization.Users.Exporting
{
    public interface IUserListExcelExporter
    {
        FileDto ExportToFile(List<UserListDto> userListDtos);
    }
}
