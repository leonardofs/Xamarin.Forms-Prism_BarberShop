using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using PrismBarbearia.Helpers;
using PrismBarbearia.Authentication;
using PrismBarbearia.UWP;

[assembly: Xamarin.Forms.Dependency(typeof(SocialAuthenticationUWP))]
namespace PrismBarbearia.UWP
{
    public class SocialAuthenticationUWP : IAuthentication
    {
        public async Task<MobileServiceUser> LoginAsync(MobileServiceClient client, MobileServiceAuthenticationProvider provider,
            IDictionary<string, string> parameters = null)
        {
            try
            {
                var user = await client.LoginAsync(provider);

                Settings.AuthToken = user?.MobileServiceAuthenticationToken ?? string.Empty;
                Settings.UserId = user?.UserId;

                return user;
            }
            catch
            {
                throw;
            }
        }
    }
}