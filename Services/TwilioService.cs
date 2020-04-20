using IEvangelist.Razor.VideoChat.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using Twilio.Jwt.AccessToken;

namespace IEvangelist.Razor.VideoChat.Services
{
    public class TwilioService : ITwilioService
    {
        readonly TwilioSettings _twilioSettings;

        public TwilioService(IOptions<TwilioSettings> twilioOptions) =>
            _twilioSettings = twilioOptions?.Value ?? throw new ArgumentException(nameof(twilioOptions));

        string ITwilioService.GetTwilioJwt(string identity) =>
            new Token(_twilioSettings.AccountSid,
                      _twilioSettings.ApiKey,
                      _twilioSettings.ApiSecret,
                      identity ?? Guid.NewGuid().ToString(),
                      grants: new HashSet<IGrant> { new VideoGrant() }).ToJwt();
    }
}
