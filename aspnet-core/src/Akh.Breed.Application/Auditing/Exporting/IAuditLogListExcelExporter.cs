using System.Collections.Generic;
using Akh.Breed.Auditing.Dto;
using Akh.Breed.Dto;

namespace Akh.Breed.Auditing.Exporting
{
    public interface IAuditLogListExcelExporter
    {
        FileDto ExportToFile(List<AuditLogListDto> auditLogListDtos);

        FileDto ExportToFile(List<EntityChangeListDto> entityChangeListDtos);
    }
}

