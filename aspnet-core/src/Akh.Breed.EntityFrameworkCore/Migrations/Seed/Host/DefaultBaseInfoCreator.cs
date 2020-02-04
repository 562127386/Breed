using System.Linq;
using Akh.Breed.BaseInfo;
using Akh.Breed.Contractors;
using Akh.Breed.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Akh.Breed.Migrations.Seed.Host
{
    public class DefaultBaseInfoCreator
    {
        private readonly BreedDbContext _context;

        public DefaultBaseInfoCreator(BreedDbContext context)
        {
            _context = context;
        }
        
        public void Create()
        {
            CreateAcademicDegree();
            CreateProviderInfo();
            CreateSexInfo();
            CreateSpeciesInfo();
            CreatePlaqueState();
            CreateFirmType();
        }
        
        private void CreateAcademicDegree()
        {
            var defaultAcademicDegree = _context.AcademicDegrees.IgnoreQueryFilters().FirstOrDefault(e => e.Name == "ابتدایی");
            if (defaultAcademicDegree == null)
            {
                defaultAcademicDegree = new AcademicDegree() {Code = "1" ,Name = "ابتدایی"};
                _context.AcademicDegrees.Add(defaultAcademicDegree);
                defaultAcademicDegree = new AcademicDegree() {Code = "2" ,Name = "سیکل"};
                _context.AcademicDegrees.Add(defaultAcademicDegree);
                defaultAcademicDegree = new AcademicDegree() {Code = "3" ,Name = "دیپلم"};
                _context.AcademicDegrees.Add(defaultAcademicDegree);
                defaultAcademicDegree = new AcademicDegree() {Code = "4" ,Name = "فوق دیپلم"};
                _context.AcademicDegrees.Add(defaultAcademicDegree);
                defaultAcademicDegree = new AcademicDegree() {Code = "5" ,Name = "کارشناسی"};
                _context.AcademicDegrees.Add(defaultAcademicDegree);
                defaultAcademicDegree = new AcademicDegree() {Code = "6" ,Name = "کارشناسی ارشد"};
                _context.AcademicDegrees.Add(defaultAcademicDegree);
                defaultAcademicDegree = new AcademicDegree() {Code = "7" ,Name = "دکترا"};
                _context.AcademicDegrees.Add(defaultAcademicDegree);
                defaultAcademicDegree = new AcademicDegree() {Code = "8" ,Name = "حوزوی"};
                _context.AcademicDegrees.Add(defaultAcademicDegree);
                _context.SaveChanges();

            }
            
        }
        
        private void CreateProviderInfo()
        {
            var defaultProviderInfo = _context.ProviderInfos.IgnoreQueryFilters().FirstOrDefault(e => e.Name == "سازمان دامپزشکی");
            if (defaultProviderInfo == null)
            {
                defaultProviderInfo = new ProviderInfo() {Code = "50" ,Name = "سازمان دامپزشکی"};
                _context.ProviderInfos.Add(defaultProviderInfo);
                defaultProviderInfo = new ProviderInfo() {Code = "51" ,Name = "اتحادیه سراسری دامداران"};
                _context.ProviderInfos.Add(defaultProviderInfo);
                defaultProviderInfo = new ProviderInfo() {Code = "52" ,Name = "اتحادیه سراسری صنعت همگام"};
                _context.ProviderInfos.Add(defaultProviderInfo);
                defaultProviderInfo = new ProviderInfo() {Code = "53" ,Name = "اتحادیه دام عشایری"};
                _context.ProviderInfos.Add(defaultProviderInfo);
                defaultProviderInfo = new ProviderInfo() {Code = "54" ,Name = "صتدوق بیمه کشاورزی"};
                _context.ProviderInfos.Add(defaultProviderInfo);
                defaultProviderInfo = new ProviderInfo() {Code = "55" ,Name = "اتحادیه دام سبک"};
                _context.ProviderInfos.Add(defaultProviderInfo);
                defaultProviderInfo = new ProviderInfo() {Code = "99" ,Name = "مرکز اصلاح نژاد"};
                _context.ProviderInfos.Add(defaultProviderInfo);

                _context.SaveChanges();

            }
            
        }
        
        private void CreateSexInfo()
        {
            var defaultSexInfo = _context.SexInfos.IgnoreQueryFilters().FirstOrDefault(e => e.Name == "نر");
            if (defaultSexInfo == null)
            {
                defaultSexInfo = new SexInfo() {Code = "1" ,Name = "نر"};
                _context.SexInfos.Add(defaultSexInfo);
                defaultSexInfo = new SexInfo() {Code = "2" ,Name = "ماده"};
                _context.SexInfos.Add(defaultSexInfo);
                defaultSexInfo = new SexInfo() {Code = "3" ,Name = "نرفری مارتین"};
                _context.SexInfos.Add(defaultSexInfo);
                defaultSexInfo = new SexInfo() {Code = "4" ,Name = "ماده فری مارتین"};
                _context.SexInfos.Add(defaultSexInfo);
                defaultSexInfo = new SexInfo() {Code = "5" ,Name = "نر(اخته)"};
                _context.SexInfos.Add(defaultSexInfo);

                _context.SaveChanges();

            }
            
        }
        
        private void CreateSpeciesInfo()
        {
            var defaultSpeciesInfo = _context.SpeciesInfos.IgnoreQueryFilters().FirstOrDefault(e => e.Name == "گاو");
            if (defaultSpeciesInfo == null)
            {
                defaultSpeciesInfo = new SpeciesInfo() {Code = "1" ,Name = "گاو"};
                _context.SpeciesInfos.Add(defaultSpeciesInfo);
                defaultSpeciesInfo = new SpeciesInfo() {Code = "2" ,Name = "گاومیش"};
                _context.SpeciesInfos.Add(defaultSpeciesInfo);
                defaultSpeciesInfo = new SpeciesInfo() {Code = "3" ,Name = "شتر"};
                _context.SpeciesInfos.Add(defaultSpeciesInfo);
                defaultSpeciesInfo = new SpeciesInfo() {Code = "4" ,Name = "گوسفند"};
                _context.SpeciesInfos.Add(defaultSpeciesInfo);
                defaultSpeciesInfo = new SpeciesInfo() {Code = "5" ,Name = "بز"};
                _context.SpeciesInfos.Add(defaultSpeciesInfo);
                defaultSpeciesInfo = new SpeciesInfo() {Code = "6" ,Name = "اسب"};
                _context.SpeciesInfos.Add(defaultSpeciesInfo);
                defaultSpeciesInfo = new SpeciesInfo() {Code = "7" ,Name = "پرندگان و طیور"};
                _context.SpeciesInfos.Add(defaultSpeciesInfo);
                defaultSpeciesInfo = new SpeciesInfo() {Code = "8" ,Name = "حیوانات کوچک و زینتی"};
                _context.SpeciesInfos.Add(defaultSpeciesInfo);
                defaultSpeciesInfo = new SpeciesInfo() {Code = "9" ,Name = "آبزیان"};
                _context.SpeciesInfos.Add(defaultSpeciesInfo);
                defaultSpeciesInfo = new SpeciesInfo() {Code = "81" ,Name = "سگ سانان"};
                _context.SpeciesInfos.Add(defaultSpeciesInfo);
                defaultSpeciesInfo = new SpeciesInfo() {Code = "82" ,Name = "گربه سانان"};
                _context.SpeciesInfos.Add(defaultSpeciesInfo);
                defaultSpeciesInfo = new SpeciesInfo() {Code = "73" ,Name = "شترمرغ"};
                _context.SpeciesInfos.Add(defaultSpeciesInfo);

                _context.SaveChanges();

            }
            
        }
        
        private void CreatePlaqueState()
        {
            var defaultPlaqueState = _context.PlaqueStates.IgnoreQueryFilters().FirstOrDefault(e => e.Name == "نصب شده");
            if (defaultPlaqueState == null)
            {
                defaultPlaqueState = new PlaqueState() {Code = "1" ,Name = "نصب شده"};
                _context.PlaqueStates.Add(defaultPlaqueState);
                defaultPlaqueState = new PlaqueState() {Code = "2" ,Name = "مفقود"};
                _context.PlaqueStates.Add(defaultPlaqueState);
                defaultPlaqueState = new PlaqueState() {Code = "3" ,Name = "ابطال"};
                _context.PlaqueStates.Add(defaultPlaqueState);
                defaultPlaqueState = new PlaqueState() {Code = "4" ,Name = "غیرمجاز"};
                _context.PlaqueStates.Add(defaultPlaqueState);

                _context.SaveChanges();

            }
            
        }
        
        private void CreateFirmType()
        {
            var defaultFirmType = _context.FirmTypes.IgnoreQueryFilters().FirstOrDefault(e => e.Name == "جهادکشاورزی");
            if (defaultFirmType == null)
            {
                defaultFirmType = new FirmType() {Code = "1" ,Name = "جهادکشاورزی"};
                _context.FirmTypes.Add(defaultFirmType);
                defaultFirmType = new FirmType() {Code = "2" ,Name = "مرکز اصلاح نژاد"};
                _context.FirmTypes.Add(defaultFirmType);
                defaultFirmType = new FirmType() {Code = "3" ,Name = "تعاونی"};
                _context.FirmTypes.Add(defaultFirmType);
                defaultFirmType = new FirmType() {Code = "4" ,Name = "شرکت خصوصی"};
                _context.FirmTypes.Add(defaultFirmType);
                defaultFirmType = new FirmType() {Code = "5" ,Name = "خدمات مشاوره ای"};
                _context.FirmTypes.Add(defaultFirmType);
                defaultFirmType = new FirmType() {Code = "6" ,Name = "اتحادیه"};
                _context.FirmTypes.Add(defaultFirmType);
                defaultFirmType = new FirmType() {Code = "7" ,Name = "سایر"};
                _context.FirmTypes.Add(defaultFirmType);

                _context.SaveChanges();

            }
            
        }
    }
}