using System;
using System.Globalization;
using System.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Reflection;
using Mobile.Interfaces;

namespace Mobile.Helpers
{
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        private readonly CultureInfo ci;
        private const string ResourceId = "Mobile.Resources.Resource";
        private static readonly Lazy<ResourceManager> ResMgr =
            new Lazy<ResourceManager> (() => new ResourceManager(ResourceId, typeof(TranslateExtension).GetTypeInfo().Assembly));

        public string Text { get; set; }

        public TranslateExtension()
        {
            ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {

            if (Text == null)
                return "";

            string translation = ResMgr.Value.GetString(Text, ci);

            if (translation == null)
            {
                #if DEBUG
                throw new ArgumentException(string.Format("Key 'Oy was not found in resources '(1}' for culture '(2}.", Text, ResourceId, ci.Name), "Text");
                #else
                                translation = Text; // returns the key, which GETS DISPLAYED TO THE USER
                #endif
            }

            return translation;
        }
    }
}

