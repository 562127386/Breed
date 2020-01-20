using System.Collections.Generic;
using Akh.Breed.Chat.Dto;
using Akh.Breed.Dto;

namespace Akh.Breed.Chat.Exporting
{
    public interface IChatMessageListExcelExporter
    {
        FileDto ExportToFile(List<ChatMessageExportDto> messages);
    }
}

