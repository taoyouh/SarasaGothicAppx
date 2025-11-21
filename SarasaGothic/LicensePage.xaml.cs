using Microsoft.Toolkit.Uwp.UI.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace SarasaGothic
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    internal sealed partial class LicensePage : Page, INotifyPropertyChanged
    {
        public static int VersionNumber = 1;
        private readonly Settings _settings = new Settings();
        private NavigationArgs _args;
        private bool _isContentLoaded = false;

        public LicensePage()
        {
            this.InitializeComponent();

            LoadLicenseContent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is NavigationArgs args)
            {
                _args = args;
            }

            _bottomPanel.Visibility = _args?.NeedAgree == true ? Visibility.Visible : Visibility.Collapsed;
        }

        public class NavigationArgs
        {
            public bool NeedAgree { get; set; }

            public Type NextPageAfterAgree { get; set; }
        }

        public bool IsContentLoaded
        {
            get => _isContentLoaded;
            set
            {
                if (_isContentLoaded != value)
                {
                    _isContentLoaded = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsContentLoaded)));
                }
            }
        }

        private async void LoadLicenseContent()
        {
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/License.md"));
            string text = await FileIO.ReadTextAsync(file);
            _contentTextBlock.Text = text;
            IsContentLoaded = true;
        }

        private void AgreeButton_Click(object sender, RoutedEventArgs e)
        {
            _settings.AcceptedLicenseVersion = VersionNumber;
            Frame.Navigate(_args?.NextPageAfterAgree, null, new EntranceNavigationTransitionInfo());
            Frame.BackStack.RemoveAt(Frame.BackStack.Count - 1);
        }

        private void DisagreeButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }

        private async void MarkdownTextBlock_OnLinkClicked(object sender, LinkClickedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri(e.Link));
        }


        public static void CheckAgreedAndNavigate(Settings settings, Frame frame, Type pageType)
        {
            if (settings.AcceptedLicenseVersion != VersionNumber)
            {
                frame.Navigate(typeof(LicensePage), new NavigationArgs()
                {
                    NeedAgree = true,
                    NextPageAfterAgree = pageType,
                });
            }
            else
            {
                frame.Navigate(pageType);
            }
        }
    }
}
