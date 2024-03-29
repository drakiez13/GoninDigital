﻿using GoninDigital.ViewModels;
using System;
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

namespace GoninDigital.SharedControl
{
    /// <summary>
    /// Interaction logic for EditProductDialog.xaml
    /// </summary>
    public partial class EditProductDialog : UserControl
    {
        public EditProductDialog()
        {
            InitializeComponent();
        }

        private void cbCategory_Selected(object sender, RoutedEventArgs e)
        {
            
        }

        private void cbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            (this.DataContext as MyShopViewModel).UpdateCategorySpecDetails();
        }
    }
}
