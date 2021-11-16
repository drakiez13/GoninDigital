﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GoninDigital.Views.DashBoardPages;
using ModernWpf.Controls;
using ModernWpf.Navigation;
using Frame = System.Windows.Controls.Frame;

namespace GoninDigital.Views
{
    /// <summary>
    /// Interaction logic for DashBoard.xaml
    /// </summary>
    public partial class DashBoard : UserControl
    {
        private static Frame rootFrame;
        public static Frame RootFrame
        {
            get => rootFrame;
        }

        public DashBoard()
        {
            InitializeComponent();
            rootFrame = contentFrame;
        }

        private void NavigationView_SelectionChanged(ModernWpf.Controls.NavigationView sender, ModernWpf.Controls.NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                contentFrame.Navigate(typeof(SettingPage));
            }
            else
            {
                var selectedItem = (ModernWpf.Controls.NavigationViewItem)args.SelectedItem;
                if (selectedItem != null) {
                    string selectedItemTag = (string)selectedItem.Tag;
                    string pageName = "GoninDigital.Views.DashBoardPages." + selectedItemTag;
                    Type pageType = typeof(HomePage).Assembly.GetType(pageName);
                    contentFrame.Navigate(pageType);
                }
                else
                {
                    contentFrame.Navigate(typeof(HomePage));
                }
            }
                
        }

        private void navigationView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            contentFrame.GoBack();
        }

        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            if (contentFrame.CanGoBack)
                navigationView.IsBackEnabled = true;
            else
                navigationView.IsBackEnabled = false;
        }
    }
}
