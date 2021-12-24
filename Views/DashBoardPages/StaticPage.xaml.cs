using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using GoninDigital.Models;
using GoninDigital.ViewModels;
using LiveCharts;
using LiveCharts.Wpf;
using Page = ModernWpf.Controls.Page;

namespace GoninDigital.Views.DashBoardPages
{
    /// <summary>
    /// Interaction logic for StaticPage.xaml
    /// </summary>
    public partial class StaticPage : Page
    {
        public StaticPage()
        { 
            InitializeComponent();
            DataContext = new StatisPageViewModel();
        }
        private void Chart_OnDataClick(object sender, ChartPoint chartpoint)
        {
            var chart = (LiveCharts.Wpf.PieChart)chartpoint.ChartView;

            //clear selected slice.
            foreach (PieSeries series in chart.Series)
                series.PushOut = 0;

            var selectedSeries = (PieSeries)chartpoint.SeriesView;
            selectedSeries.PushOut = 8;
        }
    }
}
