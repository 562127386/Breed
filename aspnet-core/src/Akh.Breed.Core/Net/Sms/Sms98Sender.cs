using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Abp.Dependency;
using Akh.Breed.Identity;
using Castle.Core.Logging;
using Microsoft.AspNetCore.WebUtilities;

namespace Akh.Breed.Net.Sms
{
    public class Sms98Sender : ISms98Sender, ITransientDependency
    {
        private Sms98SenderConfiguration _sms98SenderConfiguration;
        public ILogger Logger { get; set; }
        
        public Sms98Sender(Sms98SenderConfiguration sms98SenderConfiguration)
        {
            _sms98SenderConfiguration = sms98SenderConfiguration;
            Logger = NullLogger.Instance;
        }

        public async Task SendAsync(string number, string message)
        {
            using (var httpClient = new HttpClient())
            {

                const string url = "http://www.0098sms.com/sendsmslink.aspx";
                var param = new Dictionary<string, string>()
                {
                    { "FROM", "50002201430" },
                    { "TO", number },
                    { "TEXT", message },
                    { "USERNAME", "smsq2547" },
                    { "PASSWORD", "hamgam410009" },
                    { "DOMAIN", "0098" },
                };

                var newUrl = new Uri(QueryHelpers.AddQueryString(url, param));
                var response = await httpClient.GetAsync(newUrl);

                if (!response.IsSuccessStatusCode)
                {
                    Logger.Warn("Sending SMS has error! Message content:");
                }
            }
            
        }
    }
}

