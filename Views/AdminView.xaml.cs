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
using GoninDigital.Models;
using GoninDigital.Properties;
using GoninDigital.Utils;
using GoninDigital.Views.AdminPages;
using GoninDigital.Views.DashBoardPages;
using ModernWpf.Controls;
using ModernWpf.Controls.Primitives;
using Frame = System.Windows.Controls.Frame;
using Page = ModernWpf.Controls.Page;
using ListViewItem = ModernWpf.Controls.ListViewItem;

namespace GoninDigital.Views
{
    /// <summary>
    /// Interaction logic for AdminView.xaml
    /// </summary>
    public partial class AdminView : UserControl
    {
        private static Frame rootFrame;
        public static Frame RootFrame
        {
            get => rootFrame;
        }
        Dictionary<string, Page> pages;
        public User currentUser = null;

        // Flyout currently not support binding data
        // Use behind code to generate UI instead
        public StackPanel userFlyoutContent = null;
        public AdminView()
        {
            InitializeComponent();
            DataContext = this;
            rootFrame = contentFrame;
            pages = new Dictionary<string, Page>();
        }

        private void NavigationView_SelectionChanged(ModernWpf.Controls.NavigationView sender, ModernWpf.Controls.NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                contentFrame.Navigate(typeof(SettingPage));
                return;
            }
            var selectedItem = (ModernWpf.Controls.NavigationViewItem)args.SelectedItem;
            if (selectedItem != null)
            {
                string selectedItemTag = (string)selectedItem.Tag;
                
                if (selectedItemTag != null)
                {
                    Page togo;
                    string pageName = "GoninDigital.Views.AdminPages." + selectedItemTag;
                    if (!pages.TryGetValue(pageName, out togo))
                    {
                        Type pageType = typeof(ShopsManagerPage).Assembly.GetType(pageName);
                        contentFrame.Navigate(pageType);
                    }
                        
                }
            }
            else
            {
                contentFrame.Navigate(typeof(UsersManagerPage));
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

            var desType = contentFrame.SourcePageType;
            if (desType == typeof(UsersManagerPage))
            {
                userItem.IsSelected = true;
            }
            else if (desType == typeof(BrandsManagerPage))
            {
                brandItem.IsSelected = true;
            }
            else if (desType == typeof(SettingPage))
            {
                ((NavigationViewItem)navigationView.SettingsItem).IsSelected = true;
            }
            else if (desType == typeof(ProductsManagerPage))
            {
                productItem.IsSelected = true;
            }
            else if (desType == typeof(ShopsManagerPage))
            {
                shopItem.IsSelected = true;
            }
            else if (desType==typeof(AdsPage))
            {
                adsItem.IsSelected = true;
            }

        }
        private void NavigationViewItem_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (currentUser == null)
            {
                using (var db = new GoninDigitalDBContext())
                {
                    currentUser = db.Users.FirstOrDefault(o => o.UserName == Settings.Default.usrname);
                }
                userFlyoutContent = new StackPanel()
                {
                    Orientation = Orientation.Vertical,
                };
                var avatar = new PersonPicture()
                {
                    DisplayName = currentUser.FirstName + " " + currentUser.LastName,
                    ProfilePicture = currentUser.Avatar != null ? new BitmapImage(new Uri(currentUser.Avatar, UriKind.Absolute)) : null,
                    Margin = new Thickness(20, 10, 20, 5),
                };
                var name = new Label()
                {
                    Content = currentUser.FirstName + " " + currentUser.LastName,
                    FontWeight = FontWeights.Bold,
                    FontSize = 18,
                    HorizontalAlignment = HorizontalAlignment.Center,
                };
                var username = new Label()
                {
                    Content = "@" + currentUser.UserName,
                    HorizontalAlignment = HorizontalAlignment.Center,
                };
                userFlyoutContent.Children.Add(avatar);
                userFlyoutContent.Children.Add(name);
                userFlyoutContent.Children.Add(username);
                userFlyoutContent.Children.Add((UIElement)Resources["ok"]);
                flyout.Content = userFlyoutContent;
            }

            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }
        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            flyout.Hide();
            var selection = (e.ClickedItem as ModernWpf.Controls.ListViewItem).Name;
            if (selection == "accountInfo")
            {
                RootFrame.Navigate(new UserPage());
            }
            else if (selection == "logout")
            {
                // clear
                Settings.Default.usrname = "";
                Settings.Default.passwod = "";

                //var loginWindow = new LoginViewModel(Application.Current.MainWindow);
                WindowManager.ChangeWindowContent(Application.Current.MainWindow, Properties.Resources.LoginWindowTitle, Properties.Resources.LoginControlPath);
            }
        }
    }
}