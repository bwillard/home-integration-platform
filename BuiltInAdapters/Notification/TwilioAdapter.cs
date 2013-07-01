using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Twilio;
using HomeIntegrationPlatform.Engine.Adapters;
using HomeIntegrationPlatform.Engine;

namespace HomeIntegrationPlatform.BuiltInAdapters.Notification
{
    class TwilioAdapter : AdapterBase, INotifyingAdapter
    {
        private static AdapterConfiguration configuration = new AdapterConfiguration(new AdaperConfigurationValue[] {
            new AdaperConfigurationValue("TwilioAccountSid"),
            new AdaperConfigurationValue("TwilioAuthToken"),
            new AdaperConfigurationValue("TwilioFromNumber"),
        });

        private TwilioRestClient client;
        private readonly string fromNumber;

        public TwilioAdapter(Settings settings) : base(configuration, settings)
        {
            client = new TwilioRestClient(configuration.GetValue("TwilioAccountSid"), configuration.GetValue("TwilioAuthToken"));
            this.fromNumber = configuration.GetValue("TwilioFromNumber");
        }

        public void Notify(string toNumber, string message)
        {
            this.client.SendSmsMessage(this.fromNumber, toNumber, message);
        }
    }
}
