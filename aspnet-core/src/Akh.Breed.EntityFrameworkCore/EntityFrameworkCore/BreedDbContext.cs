using Abp.IdentityServer4;
using Abp.Zero.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Akh.Breed.Authorization.Roles;
using Akh.Breed.Authorization.Users;
using Akh.Breed.BaseInfo;
using Akh.Breed.Chat;
using Akh.Breed.Editions;
using Akh.Breed.Friendships;
using Akh.Breed.MultiTenancy;
using Akh.Breed.MultiTenancy.Accounting;
using Akh.Breed.MultiTenancy.Payments;
using Akh.Breed.Storage;
using Akh.Breed.Contractors;
using Akh.Breed.Herds;
using Akh.Breed.Livestocks;
using Akh.Breed.Notices;
using Akh.Breed.Officers;
using Akh.Breed.Plaques;
using Akh.Breed.Unions;

namespace Akh.Breed.EntityFrameworkCore
{
    public class BreedDbContext : AbpZeroDbContext<Tenant, Role, User, BreedDbContext>, IAbpPersistedGrantDbContext
    {
        /* Define an IDbSet for each entity of the application */

        public virtual DbSet<BinaryObject> BinaryObjects { get; set; }

        public virtual DbSet<Friendship> Friendships { get; set; }

        public virtual DbSet<ChatMessage> ChatMessages { get; set; }

        public virtual DbSet<SubscribableEdition> SubscribableEditions { get; set; }

        public virtual DbSet<SubscriptionPayment> SubscriptionPayments { get; set; }

        public virtual DbSet<Invoice> Invoices { get; set; }

        public virtual DbSet<PersistedGrantEntity> PersistedGrants { get; set; }

        public virtual DbSet<SubscriptionPaymentExtensionData> SubscriptionPaymentExtensionDatas { get; set; }

        public virtual DbSet<Contractor> Contractors { get; set; }
        
        public virtual DbSet<Herd> Herds { get; set; }

        public virtual DbSet<Livestock> Livestocks { get; set; }

        public virtual DbSet<Officer> Officers { get; set; }

        public virtual DbSet<AcademicDegree> AcademicDegrees { get; set; }

        public virtual DbSet<Manufacturer> Manufacturers { get; set; }
        
        public virtual DbSet<Notice> Notices { get; set; }

        public virtual DbSet<EpidemiologicInfo> EpidemiologicInfos { get; set; }

        public virtual DbSet<FirmType> FirmTypes { get; set; }
        
        public virtual DbSet<PlaqueState> PlaqueStates { get; set; }
        
        public virtual DbSet<ProviderInfo> ProviderInfos { get; set; }
        
        public virtual DbSet<UnionInfo> UnionInfos { get; set; }
        
        public virtual DbSet<UnionEmployee> UnionEmployees { get; set; }

        public virtual DbSet<ActivityInfo> ActivityInfos { get; set; }

        public virtual DbSet<SexInfo> SexInfos { get; set; }
        
        public virtual DbSet<SpeciesInfo> SpeciesInfos { get; set; }
        
        public virtual DbSet<StateInfo> StateInfos { get; set; }
        
        public virtual DbSet<CityInfo> CityInfos { get; set; }

        public virtual DbSet<RegionInfo> RegionInfos { get; set; }

        public virtual DbSet<VillageInfo> VillageInfos { get; set; }

        public virtual DbSet<PlaqueInfo> PlaqueInfos { get; set; }

        public virtual DbSet<PlaqueStore> PlaqueStores { get; set; }

        public virtual DbSet<PlaqueToState> PlaqueToStates { get; set; }
        
        public virtual DbSet<PlaqueToCity> PlaqueToCities { get; set; }
        
        public virtual DbSet<PlaqueToOfficer> PlaqueToOfficers { get; set; }
        
        public virtual DbSet<PlaqueToHerd> PlaqueToHerds { get; set; }

        public virtual DbSet<PlaqueChange> PlaqueChanges { get; set; }

        public virtual DbSet<HerdGeoLog> HerdGeoLogs { get; set; }

        
        public BreedDbContext(DbContextOptions<BreedDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BinaryObject>(b =>
            {
                b.HasIndex(e => new { e.TenantId });
            });

            modelBuilder.Entity<ChatMessage>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.UserId, e.ReadState });
                b.HasIndex(e => new { e.TenantId, e.TargetUserId, e.ReadState });
                b.HasIndex(e => new { e.TargetTenantId, e.TargetUserId, e.ReadState });
                b.HasIndex(e => new { e.TargetTenantId, e.UserId, e.ReadState });
            });

            modelBuilder.Entity<Friendship>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.UserId });
                b.HasIndex(e => new { e.TenantId, e.FriendUserId });
                b.HasIndex(e => new { e.FriendTenantId, e.UserId });
                b.HasIndex(e => new { e.FriendTenantId, e.FriendUserId });
            });

            modelBuilder.Entity<Tenant>(b =>
            {
                b.HasIndex(e => new { e.SubscriptionEndDateUtc });
                b.HasIndex(e => new { e.CreationTime });
            });

            modelBuilder.Entity<SubscriptionPayment>(b =>
            {
                b.HasIndex(e => new { e.Status, e.CreationTime });
                b.HasIndex(e => new { PaymentId = e.ExternalPaymentId, e.Gateway });
            });

            modelBuilder.Entity<SubscriptionPaymentExtensionData>(b =>
            {
                b.HasQueryFilter(m => !m.IsDeleted)
                    .HasIndex(e => new { e.SubscriptionPaymentId, e.Key, e.IsDeleted })
                    .IsUnique();
            });

            modelBuilder.Entity<StateInfo>(b =>
            {
                b.HasIndex(e => e.Code).IsUnique();
                b.HasIndex(e => e.Name).IsUnique();
            });
            
            modelBuilder.Entity<CityInfo>(b =>
            {
                b.HasIndex(e => new { e.Code, e.StateInfoId }).IsUnique();
                b.HasIndex(e => new { e.Name, e.StateInfoId }).IsUnique();
            });
            
            modelBuilder.Entity<RegionInfo>(b =>
            {
                b.HasIndex(e => new { e.Code, e.CityInfoId }).IsUnique();
                b.HasIndex(e => new { e.Name, e.CityInfoId }).IsUnique();
            });
            
            modelBuilder.Entity<VillageInfo>(b =>
            {
                b.HasIndex(e => new { e.Code, e.RegionInfoId }).IsUnique();
                b.HasIndex(e => new { e.Name, e.RegionInfoId }).IsUnique();
            });
            
            modelBuilder.Entity<AcademicDegree>(b =>
            {
                b.HasIndex(e => e.Code).IsUnique();
                b.HasIndex(e => e.Name).IsUnique();
            });

            modelBuilder.Entity<Manufacturer>(b =>
            {
                b.HasIndex(e => e.Code).IsUnique();
                b.HasIndex(e => e.Name).IsUnique();
            });
            
            modelBuilder.Entity<EpidemiologicInfo>(b =>
            {
                b.HasIndex(e => e.Code).IsUnique();
            });
            
            modelBuilder.Entity<FirmType>(b =>
            {
                b.HasIndex(e => e.Code).IsUnique();
                b.HasIndex(e => e.Name).IsUnique();
            });
            
            modelBuilder.Entity<PlaqueState>(b =>
            {
                b.HasIndex(e => e.Code).IsUnique();
                b.HasIndex(e => e.Name).IsUnique();
            });
            
            modelBuilder.Entity<ProviderInfo>(b =>
            {
                b.HasIndex(e => e.Code).IsUnique();
                b.HasIndex(e => e.Name).IsUnique();
            });
            
            modelBuilder.Entity<UnionInfo>(b =>
            {
                b.HasIndex(e => e.StateInfoId).IsUnique();
            });
            
            modelBuilder.Entity<ActivityInfo>(b =>
            {
                b.HasIndex(e => e.Code).IsUnique();
                b.HasIndex(e => e.Name).IsUnique();
            });
            
            modelBuilder.Entity<SexInfo>(b =>
            {
                b.HasIndex(e => e.Code).IsUnique();
                b.HasIndex(e => e.Name).IsUnique();
            });
            
            modelBuilder.Entity<SpeciesInfo>(b =>
            {
                b.HasIndex(e => e.Code).IsUnique();
                b.HasIndex(e => e.Name).IsUnique();
            });
            
            modelBuilder.Entity<StateInfo>(b =>
            {
                b.HasIndex(e => e.Code).IsUnique();
                b.HasIndex(e => e.Name).IsUnique();
            });
            
            modelBuilder.Entity<PlaqueInfo>(b =>
            {
                b.HasIndex(e => e.Code).IsUnique();
            });
            
            modelBuilder.Entity<PlaqueStore>(b =>
            {
                b.HasIndex(e => e.FromCode);
                b.HasIndex(e => e.ToCode);
            });
            
            modelBuilder.Entity<PlaqueToState>(b =>
            {
                b.HasIndex(e => e.FromCode);
                b.HasIndex(e => e.ToCode);
            });
            
            modelBuilder.Entity<PlaqueToCity>(b =>
            {
                b.HasIndex(e => e.FromCode);
                b.HasIndex(e => e.ToCode);
            });
            
            modelBuilder.Entity<PlaqueToOfficer>(b =>
            {
                b.HasIndex(e => e.FromCode);
                b.HasIndex(e => e.ToCode);
            });
            
            modelBuilder.Entity<PlaqueToHerd>(b =>
            {
                b.HasIndex(e => e.NationalCode);
            });

            modelBuilder.ConfigurePersistedGrantEntity();
        }
    }
}

