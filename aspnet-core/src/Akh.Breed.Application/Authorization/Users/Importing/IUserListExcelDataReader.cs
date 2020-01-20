using System.Collections.Generic;
using Akh.Breed.Authorization.Users.Importing.Dto;
using Abp.Dependency;

namespace Akh.Breed.Authorization.Users.Importing
{
    public interface IUserListExcelDataReader: ITransientDependency
    {
        List<ImportUserDto> GetUsersFromExcel(byte[] fileBytes);
    }
}

