using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using OsuDb.ReplayMasterUI.Pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace OsuDb.ReplayMasterUI
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        // List of ValueTuple holding the Navigation Tag and the relative Navigation Page
        private readonly List<(string Tag, Type Page)> _pages = new List<(string Tag, Type Page)>
        {
            ("home", typeof(HomePage)),
            ("replay", typeof(ReplayPage)),
            ("settings", typeof(SettingsPage)),
        };

        private void NavigationView_Loaded(object sender, RoutedEventArgs e)
        {
            var navView = sender as NavigationView;
            navView.SelectedItem = navView.MenuItems.First();
        }

        private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                NavView_Navigate("settings", args.RecommendedNavigationTransitionInfo);
            }
            else
            {
                var tag = args.SelectedItemContainer.Tag.ToString();
                NavView_Navigate(tag, args.RecommendedNavigationTransitionInfo);
            }
        }

        private void NavView_Navigate(string navItemTag, NavigationTransitionInfo transitionInfo)
        {
            Type _page = null;
            var item = _pages.FirstOrDefault(p => p.Tag == navItemTag);
            _page = item.Page;

            var preNavPageType = contentFrame.CurrentSourcePageType;
            if (_page is not null && !Equals(preNavPageType, _page))
            {
                contentFrame.Navigate(_page, null, transitionInfo);
            }
        }
    }
}
