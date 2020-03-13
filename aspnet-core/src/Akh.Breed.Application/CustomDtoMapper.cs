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
using Akh.Breed.Editions;
using Akh.Breed.Editions.Dto;
using Akh.Breed.Friendships;
using Akh.Breed.Friendships.Cache;
using Akh.Breed.Friendships.Dto;
using Akh.Breed.Localization.Dto;
using Akh.Breed.MultiTenancy;
using Akh.Breed.MultiTenancy.Dto;
using Akh.Breed.MultiTenancy.HostDashboard.Dto;
using Akh.Breed.MultiTenancy.Payments;
using Akh.Breed.MultiTenancy.Payments.Dto;
using Akh.Breed.Notifications.Dto;
using Akh.Breed.Officers;
using Akh.Breed.Officers.Dto;
using Akh.Breed.Organizations.Dto;
using Akh.Breed.Plaques;
using Akh.Breed.Plaques.Dto;
using Akh.Breed.Sessions.Dto;
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
                .ForMember(regionD => regionD.FirmTypeName,
                    options => options.MapFrom(l => l.FirmType.Name));
            configuration.CreateMap<ContractorCreateOrUpdateInput, Contractor>();
            configuration.CreateMap<Contractor, ContractorCreateOrUpdateInput>();
            configuration.CreateMap<Contractor, ContractorEditDto>();
            
            //Officer
            configuration.CreateMap<Officer, OfficerListDto>();
            configuration.CreateMap<OfficerCreateOrUpdateInput, Officer>();
            configuration.CreateMap<Officer, OfficerCreateOrUpdateInput>();
            configuration.CreateMap<Officer, OfficerEditDto>();

            //BaseInfo
            configuration.CreateMap<ProviderInfo, ProviderInfoListDto>();
            configuration.CreateMap<ProviderInfoCreateOrUpdateInput, ProviderInfo>();
            configuration.CreateMap<ProviderInfo, ProviderInfoCreateOrUpdateInput>();

            configuration.CreateMap<AcademicDegree, AcademicDegreeListDto>();
            configuration.CreateMap<AcademicDegreeCreateOrUpdateInput, AcademicDegree>();
            configuration.CreateMap<AcademicDegree, AcademicDegreeCreateOrUpdateInput>();

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
                .ForMember(regionD => regionD.StateInfoName, options => options.MapFrom(l => l.CityInfo.StateInfo.Name))
                .ForMember(regionD => regionD.CityInfoName, options => options.MapFrom(l => l.CityInfo.Name));
            configuration.CreateMap<RegionInfoCreateOrUpdateInput, RegionInfo>();
            configuration.CreateMap<RegionInfo, RegionInfoCreateOrUpdateInput>()
                .ForMember(regionD => regionD.StateInfoId, options => options.MapFrom(l => l.CityInfo.StateInfoId));
            
            configuration.CreateMap<VillageInfo, VillageInfoListDto>()
                .ForMember(regionD => regionD.StateInfoName, options => options.MapFrom(l => l.RegionInfo.CityInfo.StateInfo.Name))
                .ForMember(regionD => regionD.CityInfoName, options => options.MapFrom(l => l.RegionInfo.CityInfo.Name))
                .ForMember(regionD => regionD.RegionInfoName, options => options.MapFrom(l => l.RegionInfo.Name));
            configuration.CreateMap<VillageInfoCreateOrUpdateInput, VillageInfo>();
            configuration.CreateMap<VillageInfo, VillageInfoCreateOrUpdateInput>()
                .ForMember(regionD => regionD.StateInfoId, options => options.MapFrom(l => l.RegionInfo.CityInfo.StateInfoId))
                .ForMember(regionD => regionD.CityInfoId, options => options.MapFrom(l => l.RegionInfo.CityInfoId));
            
            configuration.CreateMap<PlaqueStore, PlaqueStoreListDto>()
                .ForMember(regionD => regionD.PlaqueUsed, options => options.MapFrom(l =>  l.PlaqueOfficers.Sum( u => u.ToCode + u.FromCode + 1)))
                .ForMember(regionD => regionD.PlaqueDedicated, options => options.MapFrom(l =>  l.PlaqueOfficers.Sum( u => u.ToCode + u.FromCode + 1)))
                .ForMember(regionD => regionD.PlaqueCount, options => options.MapFrom(l =>  l.ToCode - l.FromCode + 1 ))
                .ForMember(regionD => regionD.SpeciesName, options => options.MapFrom(l => l.Species.Name))
                .ForMember(regionD => regionD.FinishedCode, options => options.MapFrom(l => l.FinishedPlaque.Code))
                .ForMember(regionD => regionD.FinishedDate, options => options.MapFrom(l => l.FinishedPlaque.SetTime));
            configuration.CreateMap<PlaqueStoreCreateOrUpdateInput, PlaqueStore>();
            configuration.CreateMap<PlaqueStore, PlaqueStoreCreateOrUpdateInput>();
            
            configuration.CreateMap<PlaqueOfficer, PlaqueOfficerListDto>()
                .ForMember(regionD => regionD.PlaqueCount, options => options.MapFrom(l =>  l.ToCode - l.FromCode + 1 ))
                .ForMember(regionD => regionD.OfficerName, options => options.MapFrom(l => l.Officer.Name))
                .ForMember(regionD => regionD.PlaqueStoreName, options => options.MapFrom(l => l.PlaqueStore.FromCode.ToString()))
                .ForMember(regionD => regionD.FinishedCode, options => options.MapFrom(l => l.FinishedPlaque.Code))
                .ForMember(regionD => regionD.FinishedDate, options => options.MapFrom(l => l.FinishedPlaque.SetTime));
            configuration.CreateMap<PlaqueOfficerCreateOrUpdateInput, PlaqueOfficer>();
            configuration.CreateMap<PlaqueOfficer, PlaqueOfficerCreateOrUpdateInput>();
        }

    }
}

