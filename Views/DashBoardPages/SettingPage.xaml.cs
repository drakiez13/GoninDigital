﻿using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;
using System.Windows.Navigation;
using GoninDigital.ViewModels;

namespace GoninDigital.Views.DashBoardPages
{
    /// <summary>
    /// Interaction logic for SettingPage.xaml
    /// </summary>
    public partial class SettingPage
    {
        public SettingPage()
        {
            InitializeComponent();
            this.DataContext = new SettingPageViewModel();
        }
    }
}
