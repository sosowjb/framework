using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace SOSOWJB.Framework.Localization
{
    public static class FrameworkLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    FrameworkConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(FrameworkLocalizationConfigurer).GetAssembly(),
                        "SOSOWJB.Framework.Localization.Framework"
                    )
                )
            );
        }
    }
}