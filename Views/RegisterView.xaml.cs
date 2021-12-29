using ModernWpf.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace GoninDigital.Views
{
    /// <summary>
    /// Interaction logic for RegisterView.xaml
    /// </summary>
    public partial class RegisterView : UserControl
    {
        public RegisterView()
        {
            InitializeComponent();
        }
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {

            if (tbUsername.Text=="" || tbEmail.Text=="" || tbFirstName.Text=="" || tbLastName.Text=="" || tbPhoneNum.Text=="")
            {
                ContentDialog content = new()
                {
                    Title = "Warning",
                    Content = "Miss Information",
                    PrimaryButtonText = "Ok"
                };
                content.ShowAsync();
            }
            else if (!Regex.IsMatch(tbEmail.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                ContentDialog content = new()
                {
                    Title = "Warning",
                    Content = "Enter a valid email.",
                    PrimaryButtonText = "Ok"
                };
                content.ShowAsync();
            }

            if (FloatingPasswordBox.Password != FloatingrePasswordBox.Password)
            {
                FloatingPasswordBox.Password = null;
                FloatingrePasswordBox.Password = null;
                ContentDialog content = new()
                {
                    Title = "Warning",
                    Content = "Your Password not match, Please try again!",
                    PrimaryButtonText = "Ok"
                };
                content.ShowAsync();
            }
        }
    }
}
