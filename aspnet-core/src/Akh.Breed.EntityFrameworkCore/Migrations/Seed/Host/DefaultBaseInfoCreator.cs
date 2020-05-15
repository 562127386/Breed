using System.Linq;
using Akh.Breed.BaseInfo;
using Akh.Breed.Contractors;
using Akh.Breed.EntityFrameworkCore;
using Akh.Breed.Unions;
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
            CreateUnionInfo();
            //CreateActivityInfo();
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
        
        private void CreateUnionInfo()
        {
            var defaultUnionInfo = _context.UnionInfos.IgnoreQueryFilters().FirstOrDefault(e => e.UnionName == "اتحاديه صنعت  فارس");
            if (defaultUnionInfo == null)
            {
                defaultUnionInfo = new UnionInfo {Code = "5210" ,UnionName = "اتحاديه صنعت  فارس"};
                _context.UnionInfos.Add(defaultUnionInfo);
                defaultUnionInfo = new UnionInfo {Code = "5217" ,UnionName = "اتحاديه صنعت اصفهان"};
                _context.UnionInfos.Add(defaultUnionInfo);
                defaultUnionInfo = new UnionInfo {Code = "5223" ,UnionName = "اتحاديه صنعت کهگيلويه و بوير احمد"};
                _context.UnionInfos.Add(defaultUnionInfo);
                defaultUnionInfo = new UnionInfo {Code = "5216" ,UnionName = "اتحاديه صنعت تهران"};
                _context.UnionInfos.Add(defaultUnionInfo);
                defaultUnionInfo = new UnionInfo {Code = "5214" ,UnionName = "اتحاديه صنعت ايلام"};
                _context.UnionInfos.Add(defaultUnionInfo);
                defaultUnionInfo = new UnionInfo {Code = "5220" ,UnionName = "اتحاديه صنعت چهارمحال و بختياري"};
                _context.UnionInfos.Add(defaultUnionInfo);
                defaultUnionInfo = new UnionInfo {Code = "5226" ,UnionName = "اتحاديه صنعت سمنان"};
                _context.UnionInfos.Add(defaultUnionInfo);
                defaultUnionInfo = new UnionInfo {Code = "5202" ,UnionName = "اتحاديه صنعت قزوين"};
                _context.UnionInfos.Add(defaultUnionInfo);
                defaultUnionInfo = new UnionInfo {Code = "5200" ,UnionName = "اتحاديه صنعت مازندران"};
                _context.UnionInfos.Add(defaultUnionInfo);
                defaultUnionInfo = new UnionInfo {Code = "5222" ,UnionName = "اتحاديه صنعت مرکزي"};
                _context.UnionInfos.Add(defaultUnionInfo);
                defaultUnionInfo = new UnionInfo {Code = "5215" ,UnionName = "اتحاديه صنعت هرمزگان"};
                _context.UnionInfos.Add(defaultUnionInfo);
                defaultUnionInfo = new UnionInfo {Code = "5204" ,UnionName = "اتحاديه صنعت البرز"};
                _context.UnionInfos.Add(defaultUnionInfo);
                defaultUnionInfo = new UnionInfo {Code = "52111" ,UnionName = "اتحاديه صنعت آدربايجان  غربي"};
                _context.UnionInfos.Add(defaultUnionInfo);
                defaultUnionInfo = new UnionInfo {Code = "5227" ,UnionName = "اتحاديه صنعت دامداران و دامپروران سيستان"};
                _context.UnionInfos.Add(defaultUnionInfo);
                defaultUnionInfo = new UnionInfo {Code = "5213" ,UnionName = "اتحاديه صنعت گلستان"};
                _context.UnionInfos.Add(defaultUnionInfo);
                defaultUnionInfo = new UnionInfo {Code = "5208" ,UnionName = "اتحاديه صنعت همدان"};
                _context.UnionInfos.Add(defaultUnionInfo);
                defaultUnionInfo = new UnionInfo {Code = "5209" ,UnionName = "اتحاديه صنعت کرمان"};
                _context.UnionInfos.Add(defaultUnionInfo);
                defaultUnionInfo = new UnionInfo {Code = "5206" ,UnionName = "اتحاديه صنعت خراسان رضوي"};
                _context.UnionInfos.Add(defaultUnionInfo);
                defaultUnionInfo = new UnionInfo {Code = "5203" ,UnionName = "اتحاديه صنعت خوزستان"};
                _context.UnionInfos.Add(defaultUnionInfo);
                defaultUnionInfo = new UnionInfo {Code = "5281" ,UnionName = "اتحاديه صنعت آذربايجان شرقي"};
                _context.UnionInfos.Add(defaultUnionInfo);
                defaultUnionInfo = new UnionInfo {Code = "5205" ,UnionName = "اتحاديه صنعت جنوب کرمان"};
                _context.UnionInfos.Add(defaultUnionInfo);
                defaultUnionInfo = new UnionInfo {Code = "5211" ,UnionName = "اتحاديه صنعت کرمانشاه"};
                _context.UnionInfos.Add(defaultUnionInfo);
                defaultUnionInfo = new UnionInfo {Code = "5201" ,UnionName = "اتحاديه صنعت سيستان بلوچستان"};
                _context.UnionInfos.Add(defaultUnionInfo);
                defaultUnionInfo = new UnionInfo {Code = "5219" ,UnionName = "اتحاديه صنعت گيلان"};
                _context.UnionInfos.Add(defaultUnionInfo);
                defaultUnionInfo = new UnionInfo {Code = "5218" ,UnionName = "اتحاديه صنعت زنجان"};
                _context.UnionInfos.Add(defaultUnionInfo);
                defaultUnionInfo = new UnionInfo {Code = "52155" ,UnionName = "اتحاديه صنعت بوشهر"};
                _context.UnionInfos.Add(defaultUnionInfo);
                defaultUnionInfo = new UnionInfo {Code = "52241" ,UnionName = "اتحاديه صنعت آذربايجان غربي"};
                _context.UnionInfos.Add(defaultUnionInfo);
                defaultUnionInfo = new UnionInfo {Code = "11111" ,UnionName = "اتحاديه صنعت اردبيل"};
                _context.UnionInfos.Add(defaultUnionInfo);
                _context.SaveChanges();

            }
            
        }

        private void CreateActivityInfo()
        {
            var defaultActivityInfo = _context.ActivityInfos.IgnoreQueryFilters()
                .FirstOrDefault(e => e.Name == "مزرعه گوسفندی پرواری");
            if (defaultActivityInfo == null)
            {
                defaultActivityInfo = new ActivityInfo() {Code = "50", Name = "سازمان دامپزشکی"};
                _context.ActivityInfos.Add(defaultActivityInfo);
                defaultActivityInfo = new ActivityInfo {Code = "1" ,Name = "مزرعه گوسفندی پرواری"};
                _context.ActivityInfos.Add(defaultActivityInfo);
                defaultActivityInfo = new ActivityInfo {Code = "2" ,Name = "مزرعه گوسفندی داشتی"};
                _context.ActivityInfos.Add(defaultActivityInfo);
                defaultActivityInfo = new ActivityInfo {Code = "3" ,Name = "مزرعه بز"};
                _context.ActivityInfos.Add(defaultActivityInfo);
                defaultActivityInfo = new ActivityInfo {Code = "4" ,Name = "مزرعه اسب و استر"};
                _context.ActivityInfos.Add(defaultActivityInfo);
                defaultActivityInfo = new ActivityInfo {Code = "5" ,Name = "مزرعه گوزن"};
                _context.ActivityInfos.Add(defaultActivityInfo);
                defaultActivityInfo = new ActivityInfo {Code = "6" ,Name = "مزرعه شتر"};
                _context.ActivityInfos.Add(defaultActivityInfo);
                defaultActivityInfo = new ActivityInfo {Code = "7" ,Name = "مزرعه لاما"};
                _context.ActivityInfos.Add(defaultActivityInfo);
                defaultActivityInfo = new ActivityInfo {Code = "8" ,Name = "واحد پرورش سگ"};
                _context.ActivityInfos.Add(defaultActivityInfo);
                defaultActivityInfo = new ActivityInfo {Code = "9" ,Name = "واحد پرورش حیوانات پوستی"};
                _context.ActivityInfos.Add(defaultActivityInfo);
                defaultActivityInfo = new ActivityInfo {Code = "10" ,Name = "واحد پرورش آزمایشگاهی"};
                _context.ActivityInfos.Add(defaultActivityInfo);
                defaultActivityInfo = new ActivityInfo {Code = "11" ,Name = "پرورش دام روستایی"};
                _context.ActivityInfos.Add(defaultActivityInfo);
                defaultActivityInfo = new ActivityInfo {Code = "12" ,Name = "گاو شیری"};
                _context.ActivityInfos.Add(defaultActivityInfo);
                defaultActivityInfo = new ActivityInfo {Code = "13" ,Name = "گاو میش "};
                _context.ActivityInfos.Add(defaultActivityInfo);
                defaultActivityInfo = new ActivityInfo {Code = "14" ,Name = "مزرعه دام چند منظوره"};
                _context.ActivityInfos.Add(defaultActivityInfo);
                defaultActivityInfo = new ActivityInfo {Code = "15" ,Name = "مزرعه پرورش دام مستقر در مجتمع دامپروری"};
                _context.ActivityInfos.Add(defaultActivityInfo);
                defaultActivityInfo = new ActivityInfo {Code = "16" ,Name = "واحد پرورش دام غیر صنعتی "};
                _context.ActivityInfos.Add(defaultActivityInfo);
                defaultActivityInfo = new ActivityInfo {Code = "17" ,Name = "مزرعه گاو دو منظوره"};
                _context.ActivityInfos.Add(defaultActivityInfo);
                defaultActivityInfo = new ActivityInfo {Code = "18" ,Name = "مزرعه گاو گوشتی"};
                _context.ActivityInfos.Add(defaultActivityInfo);
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
                defaultPlaqueState = new PlaqueState() {Code = "5" ,Name = "نزد دامدار"};
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