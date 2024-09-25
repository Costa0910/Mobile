﻿using Mobile.Interfaces;
using Mobile.Resources;
using System.Globalization;
using Xamarin.Forms;

namespace Mobile.Helpers
{
	public static class Languages
    {
		static Languages() {

            CultureInfo ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            Culture = ci.Name;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }

        public static string Culture { get; set; }
        public static string Accept => Resource.Accept;
        public static string ConnectionError => Resource.ConnectionError;
        public static string Error => Resource.Error;
    }
}

