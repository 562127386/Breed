using Abp.Dependency;
using Abp.Extensions;
using Microsoft.Extensions.Configuration;
using Akh.Breed.Configuration;

namespace Akh.Breed.Net.Sms
{
    public class Sms98SenderConfiguration : ITransientDependency
    {
        private readonly IConfigurationRoot _appConfiguration;

        public string AccountSid => _appConfiguration["Sms98:AccountSid"];

        public string AuthToken => _appConfiguration["Sms98:AuthToken"];

        public string SenderNumber => _appConfiguration["Sms98:SenderNumber"];

        public Sms98SenderConfiguration(IAppConfigurationAccessor configurationAccessor)
        {
            _appConfiguration = configurationAccessor.Configuration;
        }
    }
}

