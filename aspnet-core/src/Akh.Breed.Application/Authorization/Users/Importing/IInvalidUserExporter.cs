using System.Collections.Generic;
using Akh.Breed.Authorization.Users.Importing.Dto;
using Akh.Breed.Dto;

namespace Akh.Breed.Authorization.Users.Importing
{
    public interface IInvalidUserExporter
    {
        FileDto ExportToFile(List<ImportUserDto> userListDtos);
    }
}

