using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace SarasaGothic
{
    internal class Settings
    {
        private readonly ApplicationDataContainer settings = ApplicationData.Current.LocalSettings;

        public int? AcceptedLicenseVersion
        {
            get => settings.Values[Keys.AcceptedLicenseVersion] as int?;
            set => settings.Values[Keys.AcceptedLicenseVersion] = value;
        }

        private static class Keys
        {
            public const string AcceptedLicenseVersion = nameof(AcceptedLicenseVersion);
        }
    }
}
