/*
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using PrismBarbearia.Helpers;
using PrismBarbearia.Services;
using PrismBarbearia.iOS;

[assembly: Xamarin.Forms.Dependency(typeof(SocialAuthenticationiOS))]
namespace PrismBarbearia.iOS
{
    public class SocialAuthenticationiOS : IAuthentication
    {
        public async Task<MobileServiceUser> LoginAsync(MobileServiceClient client, MobileServiceAuthenticationProvider provider,
            IDictionary<string, string> parameters = null)
        {
            try
            {
                var current = GetController();

                var user = await client.LoginAsync(current, provider);

                Settings.AuthToken = user?.MobileServiceAuthenticationToken ?? string.Empty;
                Settings.UserId = user?.UserId;

                return user;
            }
            catch (Exception ex)
            {
                //TODO: Log error
                throw;
            }
        }

        private UIKit.UIViewController GetController()
        {
            var window = UIKit.UIApplication.SharedApplication.KeyWindow;
            var root = window.RootViewController;

            if (root == null) return null;

            var current = root;
            while (current.PresentedViewController != null)
            {
                current = current.PresentedViewController;
            }

            return current;
        }
    }
}
*/