using LoLTeamSearch.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Resources;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 빈 페이지 항목 템플릿에 대한 설명은 https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x412에 나와 있습니다.

namespace LoLTeamSearch
{
    /// <summary>
    /// 자체적으로 사용하거나 프레임 내에서 탐색할 수 있는 빈 페이지입니다.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static MainPage Current;
        public static Frame RootFrame = null;

        //private ResourceLoader _resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();
        public MainPage()
        {
            this.InitializeComponent();

            this.Loaded += (sender, args) =>
              {
                  Current = this;
                  var titleBar = Windows.ApplicationModel.Core.CoreApplication.GetCurrentView().TitleBar;
              };
        }

        public Frame AppFrame { get { return this.rootFrame; } }

        
        private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                rootFrame.Navigate(typeof(SettingControl));
            }
            else
            {
                NavigationViewItem item = args.SelectedItem as NavigationViewItem;
                NavView_Navigate(item);
            }
        }

        private void NavView_Navigate(NavigationViewItem item)
        {
            switch (item.Tag)
            {
                case "SearchPage":
                    rootFrame.Navigate(typeof(MatchSearchControl)); break;
                case "MultiPage":
                    rootFrame.Navigate(typeof(MultiSearchControl)); break;
            }
        }

        private void NavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                rootFrame.Navigate(typeof(SettingControl));
            }
            else
            {
                // find NavigationViewItem with Content that equals InvokedItem
                var item = sender.MenuItems.OfType<NavigationViewItem>().First(x => (string)x.Content == (string)args.InvokedItem);
                NavView_Navigate(item as NavigationViewItem);

            }
        }

        private void NaviView_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (NavigationViewItemBase item in NaviView.MenuItems)
            {
                if (item is NavigationViewItem && item.Tag.ToString() == "home")
                {
                    NaviView.SelectedItem = item;
                    break;
                }
            }
        }
    }
}
