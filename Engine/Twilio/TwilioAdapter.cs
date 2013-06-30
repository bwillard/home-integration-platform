using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Twilio;
using ZWaveDeviceBridge.Adapters;

namespace ZWaveDeviceBridge.Twilio
{
    class TwilioAdapter : AdapterBase, INotifyingAdapter
    {
        private static AdapterConfiguration configuration = new AdapterConfiguration(new AdaperConfigurationValue[] {
            new AdaperConfigurationValue("Account Sid", "twilioAccountSid"),
            new AdaperConfigurationValue("Auth Token", "twilioAuthToken"),
            new AdaperConfigurationValue("TFrom Number", "twilioFromNumber"),
        });

        private TwilioRestClient client;
        private readonly string fromNumber;

        public TwilioAdapter(Settings settings) : base(configuration, settings)
        {
            client = new TwilioRestClient(configuration.GetValue("twilioAccountSid"), configuration.GetValue("twilioAuthToken"));
            this.fromNumber = configuration.GetValue("twilioFromNumber");
        }

        public void Notify(string toNumber, string message)
        {
            this.client.SendSmsMessage(this.fromNumber, toNumber, message);
        }
    }
}
