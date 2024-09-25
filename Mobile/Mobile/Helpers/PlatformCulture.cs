using System;

namespace Mobile.Helpers
{
    public class PlatformCulture
    {
        public string PlatformString { get; private set; }
        public string LanguageCode { get; private set; }
        public string LocaleCode { get; private set; }

        public PlatformCulture(string platformCultureString)
        {
            if (string.IsNullOrEmpty(platformCultureString))
                throw new ArgumentException("Expected culture identifier", "platformCultureString"); // in C# 6 use nameof(platformCultureString)



            PlatformString = platformCultureString.Replace("_", "-"); //.NET expects dash, not underscore

            int dashindex = PlatformString.IndexOf("-", StringComparison.Ordinal);

            if (dashindex > 0)
            {
                string[] parts = PlatformString.Split('_');
                LanguageCode = parts[0];
                LocaleCode = parts[1];
            }
            else
            {
                LanguageCode = PlatformString;
                LocaleCode = "";
            }
        }
        public override string ToString()
        {
            return PlatformString;
        }   
    }
}