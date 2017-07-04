using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using PrismBarbearia.Droid;
using Xamarin.Forms;
using PrismBarbearia.Helpers;
using PrismBarbearia.Authentication;

[assembly: Xamarin.Forms.Dependency(typeof(SocialAuthenticationAndroid))]
namespace PrismBarbearia.Droid
{
    public class SocialAuthenticationAndroid : IAuthentication
    {
        public async Task<MobileServiceUser> LoginAsync(MobileServiceClient client, MobileServiceAuthenticationProvider provider,
            IDictionary<string, string> parameters = null)
        {
            try
            {
                var user = await client.LoginAsync(Forms.Context, provider);

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