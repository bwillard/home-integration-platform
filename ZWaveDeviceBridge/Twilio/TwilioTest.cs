using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Twilio;

namespace ZWaveDeviceBridge.Twilio
{
    class TwilioTest
    {
        private TwilioRestClient client;
        private readonly string fromNumber;

        public TwilioTest(string accountSid, string authToken, string fromNumber)
        {
            client = new TwilioRestClient(accountSid, authToken);
            this.fromNumber = fromNumber;
        }

        public void SendText(string toNumber, string message)
        {
            this.client.SendSmsMessage(this.fromNumber, toNumber, message);
        }
    }
}
