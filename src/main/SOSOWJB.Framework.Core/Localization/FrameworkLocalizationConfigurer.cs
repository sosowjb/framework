using System;
using System.IO;
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
                    new XmlFileLocalizationDictionaryProvider(
                        Path.Combine(Path.GetDirectoryName(typeof(FrameworkCoreModule).GetAssembly().Location) ?? throw new InvalidOperationException(), "Localization", "Framework")
                    )
                )
            );
        }
    }
}