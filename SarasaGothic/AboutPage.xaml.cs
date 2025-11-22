using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace SarasaGothic
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    internal sealed partial class AboutPage : Page
    {
        public AboutPage()
        {
            this.InitializeComponent();

            PackageVersion packageVersion = Package.Current.Id.Version;
            _appVersionBlock.Text = $"{packageVersion.Major}.{packageVersion.Minor}.{packageVersion.Build}.{packageVersion.Revision}";

            LoadFontVersion();
        }

        private void LicensePageButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LicensePage));
        }

        private async void LoadFontVersion()
        {
            _fontVersionBlock.Text = "正在加载";
            StorageFile fontVersionFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/FontVersion.txt"));
            string fontVersion = (await FileIO.ReadTextAsync(fontVersionFile)).Trim();
            _fontVersionBlock.Text = fontVersion;
        }
    }
}
