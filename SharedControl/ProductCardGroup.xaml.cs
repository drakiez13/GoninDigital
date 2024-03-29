﻿using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GoninDigital.Models;
using GoninDigital.ViewModels;
using GoninDigital.Views;
using GoninDigital.Views.SharedPages;
using static GoninDigital.Views.SharedPages.ProductListPage;

namespace GoninDigital.SharedControl
{
    /// <summary>
    /// Interaction logic for ProductCardGroup.xaml
    /// </summary>
    public partial class ProductCardGroup : UserControl
    {
        public object Title
        {
            get => (object)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }
        public object Subtitle
        {
            get => (object)GetValue(SubtitleProperty);
            set => SetValue(SubtitleProperty, value);
        }
        
        public object ProductList
        {
            get => (object)GetValue(ProductListProperty);
            set => SetValue(ProductListProperty, value);
        }
        public object GroupBackground
        {
            get => (object)GetValue(GroupBackgroundProperty);
            set => SetValue(GroupBackgroundProperty, value);
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(object), typeof(ProductCardGroup), new PropertyMetadata("Title"));
        public static readonly DependencyProperty SubtitleProperty =
            DependencyProperty.Register("Subtitle", typeof(object), typeof(ProductCardGroup), new PropertyMetadata("Subtitle"));
        public static readonly DependencyProperty ProductListProperty =
            DependencyProperty.Register("ProductList", typeof(object), typeof(ProductCardGroup));
        public static readonly DependencyProperty GroupBackgroundProperty =
            DependencyProperty.Register("GroupBackground", typeof(object), typeof(ProductCardGroup), new PropertyMetadata("/Resources/Images/HomeProductCardGroupBackground.png"), o => o != null);

        public ICommand OnSeeAllClick { get; set; }

        public ProductCardGroup()
        {
            OnSeeAllClick = new RelayCommand<object>(o => true, o =>
            {
                var parameters = new Parameter {
                    title = (string)Title,
                    subtitle = (string)Subtitle,
                    products = ProductList as List<Product>,
                    cover = (string)GroupBackground
                };
                DashBoard.RootFrame.Navigate(new ProductListPage(parameters));
            });
            InitializeComponent();
            
        }
    }
}
