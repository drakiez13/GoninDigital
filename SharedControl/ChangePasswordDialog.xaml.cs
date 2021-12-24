using GoninDigital.Models;
using GoninDigital.Properties;
using GoninDigital.Utils;
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
    /// Interaction logic for ChangePasswordDialog.xaml
    /// </summary>
    public partial class ChangePasswordDialog : UserControl
    {
        public ChangePasswordDialog()
        {
            
            InitializeComponent();
        }

        private void OnChangeClick(object sender, RoutedEventArgs e)
        {
            string passEncode = Cryptography.MD5Hash(Cryptography.Base64Encode(OldPasswordBox.Password));
            if (passEncode == Settings.Default.passwod)
            {
                
                if (NewPasswordBox.Password == ConfirmNewPasswordBox.Password)
                {
                    if (NewPasswordBox.Password == OldPasswordBox.Password)
                    {
                        Error.Text = "Old password and new password is same";
                    }
                    else
                    {
                        using (var db = new GoninDigitalDBContext())
                        {
                            string Encode = Cryptography.MD5Hash(Cryptography.Base64Encode(NewPasswordBox.Password));
                            db.Users.First(o => o.UserName == Settings.Default.usrname).Password = Encode;
                            db.SaveChanges();
                        }
                        NewPasswordBox.Password = "";
                        OldPasswordBox.Password = "";
                        ConfirmNewPasswordBox.Password = "";
                        Error.Text = "Your password is changed";
                        Error.Foreground = new SolidColorBrush(Colors.Green);
                    }
                    
                }
                else
                {
                    Error.Text = "Password and confirm password is not match";
                }
            }
            else
            {
                Error.Text = "";
                Error.Text = "Your password is wrong";
            }
        }
    }
}
