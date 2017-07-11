using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace PrismBarbearia.Helpers
{
    public static class Settings
    {
        private static ISettings AppSettings => CrossSettings.Current;

        const string UserIdKey = "userid";
        static readonly string UserIdDefault = string.Empty;

        const string AuthTokenKey = "authtoken";
        static readonly string AuthTokenDefault = string.Empty;

        public static string AuthToken
        {
            get { return AppSettings.GetValueOrDefault<string>(AuthTokenKey, AuthTokenDefault); }
            set { AppSettings.AddOrUpdateValue<string>(AuthTokenKey, value); }
        }

        public static string UserId
        {
            get { return AppSettings.GetValueOrDefault<string>(UserIdKey, UserIdDefault); }
            set { AppSettings.AddOrUpdateValue<string>(UserIdKey, value); }
        }

        public static bool IsLoggedIn => !string.IsNullOrWhiteSpace(UserId);
        //public static bool IsAdmin => UserId == "sid:a3b2dd9e678a67f991410b35207d0572";
        //facebook do admin barbeiro8ball@hotmail.com   senha: barbeiro@8ball "sid:f6b9bcd73e4f2c2a2c10bb40648731c2"

        public static bool IsAdmin => UserId == "sid:83bda311a371e8383417e8257d67557d";
        //fred
    }
}