using System;
using System.Linq;
using Abp.Application.Editions;
using Abp.Application.Features;
using Abp.Auditing;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.EntityHistory;
using Abp.Localization;
using Abp.Notifications;
using Abp.Organizations;
using Abp.UI.Inputs;
using AutoMapper;
using Akh.Breed.Auditing.Dto;
using Akh.Breed.Authorization.Accounts.Dto;
using Akh.Breed.Authorization.Permissions.Dto;
using Akh.Breed.Authorization.Roles;
using Akh.Breed.Authorization.Roles.Dto;
using Akh.Breed.Authorization.Users;
using Akh.Breed.Authorization.Users.Dto;
using Akh.Breed.Authorization.Users.Importing.Dto;
using Akh.Breed.Authorization.Users.Profile.Dto;
using Akh.Breed.BaseInfo;
using Akh.Breed.BaseInfos.Dto;
using Akh.Breed.Chat;
using Akh.Breed.Chat.Dto;
using Akh.Breed.Contractors;
using Akh.Breed.Contractors.Dto;
using Akh.Breed.Herds;
using Akh.Breed.Herds.Dto;
using Akh.Breed.Editions;
using Akh.Breed.Editions.Dto;
using Akh.Breed.Friendships;
using Akh.Breed.Friendships.Cache;
using Akh.Breed.Friendships.Dto;
using Akh.Breed.Livestocks;
using Akh.Breed.Livestocks.Dto;
using Akh.Breed.Localization.Dto;
using Akh.Breed.MultiTenancy;
using Akh.Breed.MultiTenancy.Dto;
using Akh.Breed.MultiTenancy.HostDashboard.Dto;
using Akh.Breed.MultiTenancy.Payments;
using Akh.Breed.MultiTenancy.Payments.Dto;
using Akh.Breed.Notices;
using Akh.Breed.Notices.Dto;
using Akh.Breed.Notifications.Dto;
using Akh.Breed.Officers;
using Akh.Breed.Officers.Dto;
using Akh.Breed.Organizations.Dto;
using Akh.Breed.Plaques;
using Akh.Breed.Plaques.Dto;
using Akh.Breed.Sessions.Dto;
using Akh.Breed.Unions;
using Akh.Breed.Unions.Dto;
using OfficeOpenXml.FormulaParsing.Utilities;

namespace Akh.Breed
{
    internal static class CustomDtoMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            //Inputs
            configuration.CreateMap<CheckboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<SingleLineStringInputType, FeatureInputTypeDto>();
            configuration.CreateMap<ComboboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<IInputType, FeatureInputTypeDto>()
                .Include<CheckboxInputType, FeatureInputTypeDto>()
                .Include<SingleLineStringInputType, FeatureInputTypeDto>()
                .Include<ComboboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<StaticLocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>();
            configuration.CreateMap<ILocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>()
                .Include<StaticLocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>();
            configuration.CreateMap<LocalizableComboboxItem, LocalizableComboboxItemDto>();
            configuration.CreateMap<ILocalizableComboboxItem, LocalizableComboboxItemDto>()
                .Include<LocalizableComboboxItem, LocalizableComboboxItemDto>();

            //Chat
            configuration.CreateMap<ChatMessage, ChatMessageDto>();
            configuration.CreateMap<ChatMessage, ChatMessageExportDto>();

            //Feature
            configuration.CreateMap<FlatFeatureSelectDto, Feature>().ReverseMap();
            configuration.CreateMap<Feature, FlatFeatureDto>();

            //Role
            configuration.CreateMap<RoleEditDto, Role>().ReverseMap();
            configuration.CreateMap<Role, RoleListDto>();
            configuration.CreateMap<UserRole, UserListRoleDto>();

            //Edition
            configuration.CreateMap<EditionEditDto, SubscribableEdition>().ReverseMap();
            configuration.CreateMap<EditionCreateDto, SubscribableEdition>();
            configuration.CreateMap<EditionSelectDto, SubscribableEdition>().ReverseMap();
            configuration.CreateMap<SubscribableEdition, EditionInfoDto>();

            configuration.CreateMap<Edition, EditionInfoDto>().Include<SubscribableEdition, EditionInfoDto>();

            configuration.CreateMap<SubscribableEdition, EditionListDto>();
            configuration.CreateMap<Edition, EditionEditDto>();
            configuration.CreateMap<Edition, SubscribableEdition>();
            configuration.CreateMap<Edition, EditionSelectDto>();


            //Payment
            configuration.CreateMap<SubscriptionPaymentDto, SubscriptionPayment>().ReverseMap();
            configuration.CreateMap<SubscriptionPaymentListDto, SubscriptionPayment>().ReverseMap();
            configuration.CreateMap<SubscriptionPayment, SubscriptionPaymentInfoDto>();

            //Permission
            configuration.CreateMap<Permission, FlatPermissionDto>();
            configuration.CreateMap<Permission, FlatPermissionWithLevelDto>();

            //Language
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageEditDto>();
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageListDto>();
            configuration.CreateMap<NotificationDefinition, NotificationSubscriptionWithDisplayNameDto>();
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageEditDto>()
                .ForMember(ldto => ldto.IsEnabled, options => options.MapFrom(l => !l.IsDisabled));

            //Tenant
            configuration.CreateMap<Tenant, RecentTenant>();
            configuration.CreateMap<Tenant, TenantLoginInfoDto>();
            configuration.CreateMap<Tenant, TenantListDto>();
            configuration.CreateMap<TenantEditDto, Tenant>().ReverseMap();
            configuration.CreateMap<CurrentTenantInfoDto, Tenant>().ReverseMap();

            //User
            configuration.CreateMap<User, UserEditDto>()
                .ForMember(dto => dto.Password, options => options.Ignore())
                .ReverseMap()
                .ForMember(user => user.Password, options => options.Ignore());
            configuration.CreateMap<User, UserLoginInfoDto>();
            configuration.CreateMap<User, UserListDto>();
            configuration.CreateMap<User, ChatUserDto>();
            configuration.CreateMap<User, OrganizationUnitUserListDto>();
            configuration.CreateMap<Role, OrganizationUnitRoleListDto>();
            configuration.CreateMap<CurrentUserProfileEditDto, User>().ReverseMap();
            configuration.CreateMap<UserLoginAttemptDto, UserLoginAttempt>().ReverseMap();
            configuration.CreateMap<ImportUserDto, User>();

            //AuditLog
            configuration.CreateMap<AuditLog, AuditLogListDto>();
            configuration.CreateMap<EntityChange, EntityChangeListDto>();
            configuration.CreateMap<EntityPropertyChange, EntityPropertyChangeDto>();

            //Friendship
            configuration.CreateMap<Friendship, FriendDto>();
            configuration.CreateMap<FriendCacheItem, FriendDto>();

            //OrganizationUnit
            configuration.CreateMap<OrganizationUnit, OrganizationUnitDto>();

            /* ADD YOUR OWN CUSTOM AUTOMAPPER MAPPINGS HERE */

            //Contractor
            configuration.CreateMap<Contractor, ContractorListDto>()
                .ForMember(d => d.FirmTypeName, options => options.MapFrom(l => l.FirmType.Name))
                .ForMember(d => d.SubInstitution, options => options.MapFrom(l => l.UnionInfo.UnionName))
                .ForMember(d => d.Institution, options => options.MapFrom(l => l.Institution + " - " + l.StateInfo.Name + " - " + l.CityInfo.Name + (l.RegionInfo != null ? (" - " + l.RegionInfo.Name) : "") + (l.VillageInfo != null ? (" - " + l.VillageInfo.Name) : "")  ));
            configuration.CreateMap<ContractorCreateOrUpdateInput, Contractor>()
                .ForMember(d => d.BirthDate, options => options.MapFrom(l => l.BirthDate.GetMiladi()));
            configuration.CreateMap<Contractor, ContractorCreateOrUpdateInput>()
                .ForMember(d => d.UserName, options => options.MapFrom(l => l.User.UserName))
                .ForMember(d => d.BirthDate, options => options.MapFrom(l => l.BirthDate.GetShamsi()));

            configuration.CreateMap<Herd, HerdListDto>()
                .ForMember(d => d.ContractorName, options => options.MapFrom(l => l.Contractor.FirmName + "(" + l.Contractor.Name + "," + l.Contractor.Family + ")"))
                .ForMember(d => d.ActivityInfoName, options => options.MapFrom(l => l.ActivityInfo.Name))
                .ForMember(d => d.LiveStockOwner, options => options.MapFrom(l => l.Iranian ? (l.Reality ? " (حقیقی)" + l.Name + " " + l.Family : " (حقوقی)" + l.FirmName) :  " (شرکت خارجی)" + l.Phone))
                .ForMember(d => d.OfficerName, options => options.MapFrom(l => l.Officer.Name + " " + l.Officer.Family))
                .ForMember(d => d.Institution, options => options.MapFrom(l => l.Institution + " - " + l.StateInfo.Name + " - " + l.CityInfo.Name + (l.RegionInfo != null ? (" - " + l.RegionInfo.Name) : "") + (l.VillageInfo != null ? (" - " + l.VillageInfo.Name) : "")  ));
            configuration.CreateMap<HerdCreateOrUpdateInput, Herd>()
                .ForMember(d => d.BirthDate, options => options.MapFrom(l => l.BirthDate.GetMiladi()))
                .ForMember(d => d.IssueDate, options => options.MapFrom(l => l.IssueDate.GetMiladi()))
                .ForMember(d => d.ValidityDate, options => options.MapFrom(l => l.ValidityDate.GetMiladi()));
            configuration.CreateMap<Herd, HerdCreateOrUpdateInput>()
                .ForMember(d => d.BirthDate, options => options.MapFrom(l => l.BirthDate.GetShamsi()))
                .ForMember(d => d.IssueDate, options => options.MapFrom(l => l.IssueDate.GetShamsi()))
                .ForMember(d => d.ValidityDate, options => options.MapFrom(l => l.ValidityDate.GetShamsi()));

            
            configuration.CreateMap<Livestock, MonitoringListDto>()
                .ForMember(d => d.CreationTime, options => options.MapFrom(l => l.CreationTime.GetShamsi()))
                .ForMember(d => d.SpeciesInfoName, options => options.MapFrom(l => l.SpeciesInfo.Name))
                .ForMember(d => d.HerdName, options => options.MapFrom(l => l.Herd.HerdName))
                .ForMember(d => d.HerdOwner, options => options.MapFrom(l => l.Herd.Name+" "+l.Herd.Family))
                .ForMember(d => d.HerdCode, options => options.MapFrom(l => l.Herd.Code ))
                .ForMember(d => d.ContractorName, options => options.MapFrom(l => l.Herd.Contractor.Name + " " + l.Herd.Contractor.Family ))
                .ForMember(d => d.ContractorCode, options => options.MapFrom(l => l.Herd.Contractor.Code ))
                .ForMember(d => d.OfficerName, options => options.MapFrom(l => l.Officer.Name + " " + l.Officer.Family));
            configuration.CreateMap<Livestock, LivestockListDto>()
                .ForMember(d => d.CreationTime, options => options.MapFrom(l => l.CreationTime.GetShamsi()))
                .ForMember(d => d.SpeciesInfoName, options => options.MapFrom(l => l.SpeciesInfo.Name))
                .ForMember(d => d.SexInfoName, options => options.MapFrom(l => l.SexInfo.Name))
                .ForMember(d => d.HerdName, options => options.MapFrom(l => l.Herd.Code + " - " + l.Herd.HerdName + "(" +l.Herd.Name+","+l.Herd.Family+")"))
                .ForMember(d => d.ActivityInfoName, options => options.MapFrom(l => l.ActivityInfo.Name))
                .ForMember(d => d.OfficerName, options => options.MapFrom(l => l.Officer.Name + " " + l.Officer.Family));
            configuration.CreateMap<LivestockCreateOrUpdateInput, Livestock>()
                .ForMember(d => d.BirthDate, options => options.MapFrom(l => l.BirthDate.GetMiladi()))
                .ForMember(d => d.CreationTime, options => options.MapFrom(l => l.CreationTime.GetMiladi()));
            configuration.CreateMap<Livestock, LivestockCreateOrUpdateInput>()
                .ForMember(d => d.CreationTime, options => options.MapFrom(l => l.CreationTime.GetShamsi()))
                .ForMember(d => d.OfficerName, options => options.MapFrom(l => l.Officer.Name + " " + l.Officer.Family))
                .ForMember(d => d.BirthDate, options => options.MapFrom(l => l.BirthDate.GetShamsi()));

            configuration.CreateMap<PlaqueToHerd, PlaqueToHerdListDto>()
                .ForMember(d => d.CreationTime, options => options.MapFrom(l => l.CreationTime.GetShamsi()))
                .ForMember(d => d.HerdName, options => options.MapFrom(l => l.Herd.Code + " - " + l.Herd.HerdName + "(" +l.Herd.Name+","+l.Herd.Family+")"))
                .ForMember(d => d.OfficerName, options => options.MapFrom(l => l.Officer.Name + " " + l.Officer.Family));
            configuration.CreateMap<PlaqueToHerdCreateOrUpdateInput, PlaqueToHerd>()
                .ForMember(d => d.CreationTime, options => options.MapFrom(l => l.CreationTime.GetMiladi()));
            configuration.CreateMap<PlaqueToHerd, PlaqueToHerdCreateOrUpdateInput>()
                .ForMember(d => d.CreationTime, options => options.MapFrom(l => l.CreationTime.GetShamsi()))
                .ForMember(d => d.OfficerName, options => options.MapFrom(l => l.Officer.Name + " " + l.Officer.Family));

            //Officer
            configuration.CreateMap<Officer, OfficerListDto>()
                .ForMember(d => d.ContractorName, options => options.MapFrom(l => l.Contractor.FirmName + "(" + l.Contractor.Family + "," + l.Contractor.Name + ")"))
                .ForMember(d => d.StateInfoName, options => options.MapFrom(l => l.StateInfo.Name  ));
            configuration.CreateMap<OfficerCreateOrUpdateInput, Officer>()
                .ForMember(d => d.BirthDate, options => options.MapFrom(l => l.BirthDate.GetMiladi()));
            configuration.CreateMap<Officer, OfficerCreateOrUpdateInput>()
                .ForMember(d => d.UserName, options => options.MapFrom(l => l.User.UserName))
                .ForMember(d => d.BirthDate, options => options.MapFrom(l => l.BirthDate.GetShamsi()));
            configuration.CreateMap<Officer, OfficerEditDto>()
                .ForMember(d => d.BirthDate, options => options.MapFrom(l => l.BirthDate.GetShamsi()));

            //BaseInfo
            configuration.CreateMap<ProviderInfo, ProviderInfoListDto>();
            configuration.CreateMap<ProviderInfoCreateOrUpdateInput, ProviderInfo>();
            configuration.CreateMap<ProviderInfo, ProviderInfoCreateOrUpdateInput>();

            configuration.CreateMap<UnionInfo, UnionInfoListDto>()
                .ForMember(d => d.StateInfoName, options => options.MapFrom(l => l.StateInfo.Name));
            configuration.CreateMap<UnionInfoCreateOrUpdateInput, UnionInfo>();
            configuration.CreateMap<UnionInfo, UnionInfoCreateOrUpdateInput>()
                .ForMember(d => d.UserName, options => options.MapFrom(l => l.User.UserName));

            configuration.CreateMap<UnionEmployee, UnionEmployeeListDto>();
            configuration.CreateMap<UnionEmployeeCreateOrUpdateInput, UnionEmployee>();
            configuration.CreateMap<UnionEmployee, UnionEmployeeCreateOrUpdateInput>()
                .ForMember(d => d.UserName, options => options.MapFrom(l => l.User.UserName));
            
            configuration.CreateMap<ActivityInfo, ActivityInfoListDto>();
            configuration.CreateMap<ActivityInfoCreateOrUpdateInput, ActivityInfo>();
            configuration.CreateMap<ActivityInfo, ActivityInfoCreateOrUpdateInput>();
            
            configuration.CreateMap<AcademicDegree, AcademicDegreeListDto>();
            configuration.CreateMap<AcademicDegreeCreateOrUpdateInput, AcademicDegree>();
            configuration.CreateMap<AcademicDegree, AcademicDegreeCreateOrUpdateInput>();
            
            configuration.CreateMap<Manufacturer, ManufacturerListDto>();
            configuration.CreateMap<ManufacturerCreateOrUpdateInput, Manufacturer>();
            configuration.CreateMap<Manufacturer, ManufacturerCreateOrUpdateInput>();

            configuration.CreateMap<Notice, NoticeListDto>()
                .ForMember(d => d.UserName, options => options.MapFrom(l => l.User.Name + " " + l.User.Surname));
            configuration.CreateMap<NoticeCreateOrUpdateInput, Notice>();
            configuration.CreateMap<Notice, NoticeCreateOrUpdateInput>();
            
            configuration.CreateMap<EpidemiologicInfo, EpidemiologicInfoListDto>();
            configuration.CreateMap<EpidemiologicInfoCreateOrUpdateInput, EpidemiologicInfo>();
            configuration.CreateMap<EpidemiologicInfo, EpidemiologicInfoCreateOrUpdateInput>();

            configuration.CreateMap<FirmType, FirmTypeListDto>();
            configuration.CreateMap<FirmTypeCreateOrUpdateInput, FirmType>();
            configuration.CreateMap<FirmType, FirmTypeCreateOrUpdateInput>();

            configuration.CreateMap<PlaqueState, PlaqueStateListDto>();
            configuration.CreateMap<PlaqueStateCreateOrUpdateInput, PlaqueState>();
            configuration.CreateMap<PlaqueState, PlaqueStateCreateOrUpdateInput>();

            configuration.CreateMap<SexInfo, SexInfoListDto>();
            configuration.CreateMap<SexInfoCreateOrUpdateInput, SexInfo>();
            configuration.CreateMap<SexInfo, SexInfoCreateOrUpdateInput>();

            configuration.CreateMap<SpeciesInfo, SpeciesInfoListDto>();
            configuration.CreateMap<SpeciesInfoCreateOrUpdateInput, SpeciesInfo>();
            configuration.CreateMap<SpeciesInfo, SpeciesInfoCreateOrUpdateInput>();

            configuration.CreateMap<StateInfo, StateInfoListDto>();
            configuration.CreateMap<StateInfoCreateOrUpdateInput, StateInfo>();
            configuration.CreateMap<StateInfo, StateInfoCreateOrUpdateInput>();

            configuration.CreateMap<CityInfo, CityInfoListDto>()
                .ForMember(cityD => cityD.StateInfoName, options => options.MapFrom(l => l.StateInfo.Name));
            configuration.CreateMap<CityInfoCreateOrUpdateInput, CityInfo>();
            configuration.CreateMap<CityInfo, CityInfoCreateOrUpdateInput>();
            
            configuration.CreateMap<RegionInfo, RegionInfoListDto>()
                .ForMember(d => d.StateInfoName, options => options.MapFrom(l => l.CityInfo.StateInfo.Name))
                .ForMember(d => d.CityInfoName, options => options.MapFrom(l => l.CityInfo.Name));
            configuration.CreateMap<RegionInfoCreateOrUpdateInput, RegionInfo>();
            configuration.CreateMap<RegionInfo, RegionInfoCreateOrUpdateInput>()
                .ForMember(d => d.StateInfoId, options => options.MapFrom(l => l.CityInfo.StateInfoId));
            
            configuration.CreateMap<VillageInfo, VillageInfoListDto>()
                .ForMember(d => d.StateInfoName, options => options.MapFrom(l => l.RegionInfo.CityInfo.StateInfo.Name))
                .ForMember(d => d.CityInfoName, options => options.MapFrom(l => l.RegionInfo.CityInfo.Name))
                .ForMember(d => d.RegionInfoName, options => options.MapFrom(l => l.RegionInfo.Name));
            configuration.CreateMap<VillageInfoCreateOrUpdateInput, VillageInfo>();
            configuration.CreateMap<VillageInfo, VillageInfoCreateOrUpdateInput>()
                .ForMember(d => d.StateInfoId, options => options.MapFrom(l => l.RegionInfo.CityInfo.StateInfoId))
                .ForMember(d => d.CityInfoId, options => options.MapFrom(l => l.RegionInfo.CityInfoId));

            configuration.CreateMap<PlaqueStore, PlaqueStoreListDto>()
                .ForMember(d => d.SetTime, options => options.MapFrom(l => l.SetTime.GetShamsi()))
                .ForMember(d => d.PlaqueCount, options => options.MapFrom(l => l.ToCode - l.FromCode + 1))
                .ForMember(d => d.PlaqueAllocated, options => options.MapFrom(l => l.LastCode == 0 ? 0 : Convert.ToUInt64(l.LastCode - l.FromCode + 1)))
                .ForMember(d => d.SpeciesName, options => options.MapFrom(l => l.Species.Name))
                .ForMember(d => d.ManufacturerName, options => options.MapFrom(l => l.Manufacturer.Name));
            configuration.CreateMap<PlaqueStoreCreateOrUpdateInput, PlaqueStore>()
                .ForMember(d => d.SetTime, options => options.MapFrom(l => l.SetTime.GetMiladi()));
            configuration.CreateMap<PlaqueStore, PlaqueStoreCreateOrUpdateInput>()
                .ForMember(d => d.SetTime, options => options.MapFrom(l => l.SetTime.GetShamsi()));

            configuration.CreateMap<PlaqueChange, PlaqueChangeListDto>()
                .ForMember(d => d.CreationTime, options => options.MapFrom(l => l.CreationTime.GetShamsi()))
                .ForMember(d => d.PlaqueCode, options => options.MapFrom(l => l.Plaque.Code))
                .ForMember(d => d.OfficerName, options => options.MapFrom(l => l.Officer.Name + " " + l.Officer.Family))
                .ForMember(d => d.PlaqueHerdName, options => options.MapFrom(l =>  l.Plaque.Livestock.Herd.Code + " - " + l.Plaque.Livestock.Herd.HerdName + "(" +l.Plaque.Livestock.Herd.Name+","+l.Plaque.Livestock.Herd.Family+")"))
                .ForMember(d => d.PreStateName, options => options.MapFrom(l => l.PreState.Name));
            configuration.CreateMap<PlaqueChangeCreateOrUpdateInput, PlaqueChange>();
            configuration.CreateMap<PlaqueChange, PlaqueChangeCreateOrUpdateInput>();
            
            configuration.CreateMap<HerdGeoLog, HerdGeoLogListDto>()
                .ForMember(d => d.CreationTime, options => options.MapFrom(l => l.CreationTime.GetShamsi()))
                .ForMember(d => d.HerdName, options => options.MapFrom(l => l.Herd.Code + " - " + l.Herd.HerdName + "(" +l.Herd.Name+","+l.Herd.Family+")"))
                .ForMember(d => d.OfficerName, options => options.MapFrom(l => l.Officer.Name + " " + l.Officer.Family));
            configuration.CreateMap<HerdGeoLogCreateOrUpdateInput, HerdGeoLog>();
            configuration.CreateMap<HerdGeoLog, HerdGeoLogCreateOrUpdateInput>();
            
            configuration.CreateMap<PlaqueToState, PlaqueToStateListDto>()
                .ForMember(d => d.SetTime, options => options.MapFrom(l => l.SetTime.GetShamsi()))
                .ForMember(d => d.PlaqueCount, options => options.MapFrom(l =>  l.ToCode - l.FromCode + 1 ))
                .ForMember(d => d.PlaqueAllocated, options => options.MapFrom(l => l.LastCode == 0 ? 0 : Convert.ToUInt64(l.LastCode - l.FromCode + 1)))
                .ForMember(d => d.StateName, options => options.MapFrom(l => l.StateInfo.Name))
                .ForMember(d => d.SpeciesName, options => options.MapFrom(l => l.PlaqueStore.Species.Name));
            configuration.CreateMap<PlaqueToStateCreateOrUpdateInput, PlaqueToState>()
                .ForMember(d => d.SetTime, options => options.MapFrom(l => l.SetTime.GetMiladi()));
            configuration.CreateMap<PlaqueToState, PlaqueToStateCreateOrUpdateInput>()
                .ForMember(d => d.PlaqueCount, options => options.MapFrom(l =>  l.ToCode - l.FromCode + 1 ))
                .ForMember(d => d.SpeciesInfoId, options => options.MapFrom(l =>  l.PlaqueStore.SpeciesId ))
                .ForMember(d => d.SetTime, options => options.MapFrom(l => l.SetTime.GetShamsi()));
            
            configuration.CreateMap<PlaqueToContractor, PlaqueToContractorListDto>()
                .ForMember(d => d.SetTime, options => options.MapFrom(l => l.SetTime.GetShamsi()))
                .ForMember(d => d.PlaqueCount, options => options.MapFrom(l =>  l.ToCode - l.FromCode + 1 ))
                .ForMember(d => d.PlaqueAllocated, options => options.MapFrom(l => l.LastCode == 0 ? 0 : Convert.ToUInt64(l.LastCode - l.FromCode + 1)))
                .ForMember(d => d.ContractorName, options => options.MapFrom(l => l.Contractor.Name + " " + l.Contractor.Family))
                .ForMember(d => d.CityName, options => options.MapFrom(l => l.Contractor.CityInfo.Name))
                .ForMember(d => d.StateName, options => options.MapFrom(l => l.Contractor.CityInfo.StateInfo.Name))
                .ForMember(d => d.SpeciesName, options => options.MapFrom(l => l.PlaqueToState.PlaqueStore.Species.Name));
            configuration.CreateMap<PlaqueToContractorCreateOrUpdateInput, PlaqueToContractor>()
                .ForMember(d => d.SetTime, options => options.MapFrom(l => l.SetTime.GetMiladi()));
            configuration.CreateMap<PlaqueToContractor, PlaqueToContractorCreateOrUpdateInput>()
                .ForMember(d => d.PlaqueCount, options => options.MapFrom(l =>  l.ToCode - l.FromCode + 1 ))
                .ForMember(d => d.StateInfoId, options => options.MapFrom(l =>  l.Contractor.CityInfo.StateInfoId ))
                .ForMember(d => d.SpeciesInfoId, options => options.MapFrom(l =>  l.PlaqueToState.PlaqueStore.SpeciesId ))
                .ForMember(d => d.SetTime, options => options.MapFrom(l => l.SetTime.GetShamsi()));
            
            configuration.CreateMap<PlaqueToOfficer, PlaqueToOfficerListDto>()
                .ForMember(d => d.SetTime, options => options.MapFrom(l => l.SetTime.GetShamsi()))
                .ForMember(d => d.PlaqueCount, options => options.MapFrom(l =>  l.ToCode - l.FromCode + 1 ))
                .ForMember(d => d.ContractorName, options => options.MapFrom(l => l.PlaqueToContractor.Contractor.FirmName + " (" + l.PlaqueToContractor.Contractor.Name + "," + l.PlaqueToContractor.Contractor.Family + ")"))
                .ForMember(d => d.StateName, options => options.MapFrom(l => l.PlaqueToContractor.Contractor.CityInfo.StateInfo.Name))
                .ForMember(d => d.OfficerName, options => options.MapFrom(l => l.Officer.Name + " " + l.Officer.Family))
                .ForMember(d => d.OfficerCode, options => options.MapFrom(l => l.Officer.Code))
                .ForMember(d => d.SpeciesName, options => options.MapFrom(l => l.PlaqueToContractor.PlaqueToState.PlaqueStore.Species.Name));
            configuration.CreateMap<PlaqueToOfficerCreateOrUpdateInput, PlaqueToOfficer>()
                .ForMember(d => d.SetTime, options => options.MapFrom(l => l.SetTime.GetMiladi()));
            configuration.CreateMap<PlaqueToOfficer, PlaqueToOfficerCreateOrUpdateInput>()
                .ForMember(d => d.PlaqueCount, options => options.MapFrom(l =>  l.ToCode - l.FromCode + 1 ))
                .ForMember(d => d.StateInfoId, options => options.MapFrom(l =>  l.PlaqueToContractor.Contractor.CityInfo.StateInfoId ))
                .ForMember(d => d.ContractorId, options => options.MapFrom(l =>  l.PlaqueToContractor.ContractorId ))
                .ForMember(d => d.SpeciesInfoId, options => options.MapFrom(l =>  l.PlaqueToContractor.PlaqueToState.PlaqueStore.SpeciesId ))
                .ForMember(d => d.SetTime, options => options.MapFrom(l => l.SetTime.GetShamsi()));

        }

    }
}

