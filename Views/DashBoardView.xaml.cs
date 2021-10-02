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
using GoninDigital.Views.DashBoardPages;
using ModernWpf.Controls;
using ModernWpf.Navigation;

namespace GoninDigital.Views
{
    /// <summary>
    /// Interaction logic for DashBoard.xaml
    /// </summary>
    public partial class DashBoard : UserControl
    {
        public DashBoard()
        {
            InitializeComponent();
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
    }
}
