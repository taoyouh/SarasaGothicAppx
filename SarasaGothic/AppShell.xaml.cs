using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
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
    public sealed partial class AppShell : Page
    {
        public AppShell()
        {
            this.InitializeComponent();

            var titleBar = CoreApplication.GetCurrentView().TitleBar;
            titleBar.ExtendViewIntoTitleBar = true;
            _appTitleTextBlock.Text = Package.Current.DisplayName;
            Window.Current.SetTitleBar(_appTitleBar);

            Dispatcher.AcceleratorKeyActivated += Dispatcher_AcceleratorKeyActivated;
            SystemNavigationManager.GetForCurrentView().BackRequested += AppShell_BackRequested;
            Window.Current.CoreWindow.PointerPressed += CoreWindow_PointerPressed;
        }

        private void CoreWindow_PointerPressed(CoreWindow sender, PointerEventArgs args)
        {
            if (args.CurrentPoint.Properties.IsXButton1Pressed)
            {
                if (_contentFrame.CanGoBack)
                {
                    _contentFrame.GoBack();
                    args.Handled = true;
                }
            }
        }

        public Frame ContentFrame => _contentFrame;

        private void Dispatcher_AcceleratorKeyActivated(CoreDispatcher sender, AcceleratorKeyEventArgs args)
        {
            if (args.EventType == CoreAcceleratorKeyEventType.SystemKeyDown
                && (args.VirtualKey == VirtualKey.Left)
                && args.KeyStatus.IsMenuKeyDown == true)
            {
                args.Handled = TryGoBack();
            }
        }

        private void AppShell_BackRequested(object sender, BackRequestedEventArgs e)
        {
            e.Handled = TryGoBack();
        }

        private void BackButton_Click(object sender, RoutedEventArgs args)
        {
            TryGoBack();
        }

        private bool TryGoBack()
        {
            if (_contentFrame.CanGoBack)
            {
                _contentFrame.GoBack();
                return true;
            }
            return false;
        }
    }
}
