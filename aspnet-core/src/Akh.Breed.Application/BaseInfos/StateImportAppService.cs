using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Localization;
using Abp.Localization.Sources;
using Akh.Breed.Authorization.Users.Importing.Dto;
using Akh.Breed.BaseInfo;
using Akh.Breed.BaseInfos.Dto;
using Akh.Breed.DataExporting.Excel.EpPlus;
using OfficeOpenXml;

namespace Akh.Breed.BaseInfos
{
    public class StateImportAppService : EpPlusExcelImporterBase<StateImportDto>, IStateImportAppService
    {
        private readonly IRepository<StateInfo> _stateInfoRepository;
        private readonly IRepository<CityInfo> _cityInfoRepository;
        private readonly IRepository<VillageInfo> _villageInfoRepository;
        private readonly IRepository<RegionInfo> _regionInfoRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly ILocalizationSource _localizationSource;

        public StateImportAppService(IRepository<StateInfo> stateInfoRepository,
            IRepository<CityInfo> cityInfoRepository, IRepository<RegionInfo> regionInfoRepository
            , IRepository<VillageInfo> villageInfoRepository, ILocalizationManager localizationManager,
            IUnitOfWorkManager unitOfWorkManager)
        {
            _stateInfoRepository = stateInfoRepository;
            _cityInfoRepository = cityInfoRepository;
            _villageInfoRepository = villageInfoRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _regionInfoRepository = regionInfoRepository;
            _localizationSource = localizationManager.GetSource(BreedConsts.LocalizationSourceName);
        }

        [UnitOfWork(IsDisabled = true)]
        public void InitialData()
        {
            var defaultStateInfo = _stateInfoRepository.FirstOrDefault(e => e.Name == "مرکزي");
            var defaultCityInfo = _cityInfoRepository.FirstOrDefault(e => e.Name == "اراک");
            var defaultRegionInfo = _regionInfoRepository.FirstOrDefault(e => e.Name == "مرکزي");
            if (defaultStateInfo == null)
            {
                var path = Directory.GetCurrentDirectory() + "\\StateCity.xlsx";
                var fileBytes = System.IO.File.ReadAllBytes(path);
                List<StateImportDto> stateCities = ProcessExcelFile(fileBytes, ProcessExcelRow);
                foreach (var stateCity in stateCities)
                {
                    if (stateCity.StateName != defaultStateInfo?.Name)
                    {
                        defaultStateInfo = _stateInfoRepository.FirstOrDefault(e => e.Name == stateCity.StateName) ??
                                           _stateInfoRepository.Insert(new StateInfo()
                                               {Code = stateCity.StateCode, Name = stateCity.StateName});
                    }


                    if (stateCity.CityName != defaultCityInfo?.Name)
                    {
                        defaultCityInfo = _cityInfoRepository.FirstOrDefault(e => e.Name == stateCity.CityName) ??
                                          _cityInfoRepository.Insert(new CityInfo() {StateInfo = defaultStateInfo, Code = stateCity.CityCode, Name = stateCity.CityName});
                    }


                    if (stateCity.RegionName != defaultRegionInfo?.Name)
                    {
                        defaultRegionInfo =
                            _regionInfoRepository.FirstOrDefault(e => e.Name == stateCity.CityName) ??
                            _regionInfoRepository.Insert(new RegionInfo()
                                {CityInfo = defaultCityInfo, Code = stateCity.RegionCode, Name = stateCity.RegionName});
                    }


                    _villageInfoRepository.Insert(new VillageInfo()
                        {RegionInfo = defaultRegionInfo, Code = stateCity.VillageCode, Name = stateCity.VillageName});
                }
            }


        }


        private StateImportDto ProcessExcelRow(ExcelWorksheet worksheet, int row)
        {
            if (IsRowEmpty(worksheet, row))
            {
                return null;
            }

            var exceptionMessage = new StringBuilder();
            var stateImport = new StateImportDto();

            try
            {
                stateImport.StateCode = GetRequiredValueFromRowOrNull(worksheet, row, 1, nameof(stateImport.StateCode),
                    exceptionMessage);
                stateImport.StateName = GetRequiredValueFromRowOrNull(worksheet, row, 2, nameof(stateImport.StateName),
                    exceptionMessage);
                stateImport.CityCode =
                    GetRequiredValueFromRowOrNull(worksheet, row, 3, nameof(stateImport.CityCode), exceptionMessage);
                stateImport.CityName =
                    GetRequiredValueFromRowOrNull(worksheet, row, 4, nameof(stateImport.CityName), exceptionMessage);
                stateImport.RegionCode = GetRequiredValueFromRowOrNull(worksheet, row, 5,
                    nameof(stateImport.RegionCode), exceptionMessage);
                stateImport.RegionName = GetRequiredValueFromRowOrNull(worksheet, row, 6,
                    nameof(stateImport.RegionName), exceptionMessage);
                stateImport.VillageCode = GetRequiredValueFromRowOrNull(worksheet, row, 7,
                    nameof(stateImport.VillageCode), exceptionMessage);
                stateImport.VillageName = GetRequiredValueFromRowOrNull(worksheet, row, 8,
                    nameof(stateImport.VillageName), exceptionMessage);

            }
            catch (System.Exception exception)
            {
                stateImport.Exception = exception.Message;
            }

            return stateImport;
        }

        private bool IsRowEmpty(ExcelWorksheet worksheet, int row)
        {
            return worksheet.Cells[row, 1].Value == null ||
                   string.IsNullOrWhiteSpace(worksheet.Cells[row, 1].Value.ToString());
        }

        private string GetRequiredValueFromRowOrNull(ExcelWorksheet worksheet, int row, int column, string columnName,
            StringBuilder exceptionMessage)
        {
            var cellValue = worksheet.Cells[row, column].Value;

            if (cellValue != null && !string.IsNullOrWhiteSpace(cellValue.ToString()))
            {
                return cellValue.ToString();
            }

            exceptionMessage.Append(GetLocalizedExceptionMessagePart(columnName));
            return null;
        }

        private string GetLocalizedExceptionMessagePart(string parameter)
        {
            return _localizationSource.GetString("{0}IsInvalid", _localizationSource.GetString(parameter)) + "; ";
        }
    }
}