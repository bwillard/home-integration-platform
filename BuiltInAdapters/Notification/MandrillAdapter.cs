using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Twilio;
using HomeIntegrationPlatform.Engine.Adapters;
using HomeIntegrationPlatform.Engine;
using MailChimp;

namespace HomeIntegrationPlatform.BuiltInAdapters.Notification
{
    class MandrillAdapter : AdapterBase, INotifyingAdapter
    {
        private static AdapterConfiguration configuration = new AdapterConfiguration(new AdaperConfigurationValue[] {
            new AdaperConfigurationValue("MandrillApiKey"),
            new AdaperConfigurationValue("MandrillFromAddress"),
        });

        private readonly MandrillApi client;
        private readonly string fromAddress;

        public MandrillAdapter(Settings settings)
            : base(configuration, settings)
        {
            client = new MandrillApi(configuration.GetValue("MandrillApiKey"));
            this.fromAddress = configuration.GetValue("MandrillFromAddress");
        }

        public void Notify(string toNumber, string body)
        {
            var message = new MailChimp.Types.Mandrill.Messages.Message();
            message.Subject = "Message from Home Automation Platform";
            message.Text = body;
            message.FromEmail = this.fromAddress;
            this.client.Send(message);
        }
    }
}
