using GoninDigital.Models;
using GoninDigital.Utils;
using GoninDigital.ViewModels;
using GoninDigital.ViewModels.Validator;
using ModernWpf.Controls;
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
    /// Interaction logic for AddUserDialog.xaml
    /// </summary>
    public partial class AddUserDialog : UserControl
    {
        enum LGender
        {
            Other = 0,
            Female = 1,
            Male = 2
        };
        enum LTypeU
        {
            Admin = 1,
            Seller = 2,
            Customer = 3
        }
        private string _UserName;
        public string UserName
        {
            get => _UserName;
            set
            {
                _UserName = value;
            }
        }
        private string _Password;
        public string Password
        {
            get => _Password;
            set
            {
                _Password = value;
            }
        }
        private string _CPassword;
        public string CPassword
        {
            get => _CPassword;
            set
            {
                _CPassword = value;
            }
        }
        private string _FirstName;
        public string FirstName
        {
            get => _FirstName;
            set
            {
                _FirstName = value;
            }
        }
        private string _LastName;
        public string LastName
        {
            get => _LastName;
            set
            {
                _LastName = value;
            }
        }
        private int _Gender = (int)LGender.Male;
        public int Gender
        {
            get => _Gender;
            set
            {
                _Gender = value;
            }
        }
        private DateTime _DoB = DateTime.Now;
        public DateTime DoB
        {
            get => _DoB;
            set
            {
                _DoB = value;
            }
        }
        private string _Email;
        public string Email
        {
            get => _Email;
            set
            {
                _Email = value;
            }
        }
        private string _PhoneNumber;
        public string PhoneNumber
        {
            get => _PhoneNumber;
            set
            {
                _PhoneNumber = value;
            }
        }
        public ICommand PasswordChangedCommand { get; set; }
        public AddUserDialog()
        {
            InitializeComponent();
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { Password = p.Password; });
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            Password = FloatingPasswordBox.Password;
            CPassword = FloatingrePasswordBox.Password;
            if (UserName =="" | FirstName =="" | LastName =="" | PhoneNumber =="" | Email =="")
            {
                Error.Foreground = Brushes.Red;
                Error.Text = "Please enter full information";
            }
            else if(Password!=CPassword)
            {
                Error.Foreground = Brushes.Red;
                Error.Text = "Re-entered password is incorrect";
            }
            else if (AccountManager.AccountExists(Email, UserName))
            {
                Error.Foreground = Brushes.Red;
                Error.Text = "Your username or email already exists";
            }
            else
            {
                try
                {
                    var Validator = new PersonalAccountValidator().ValidatePassword(Password);
                    if (!Validator.IsValid)
                    {
                        throw new Exception(Validator.ValidationMessage);
                    }
                    User new_user = new()
                    {
                        UserName = UserName,
                        Password = Cryptography.MD5Hash(Cryptography.Base64Encode(Password)),
                        TypeId = (int)LTypeU.Customer,
                        FirstName = FirstName,
                        LastName = LastName,
                        PhoneNumber = PhoneNumber,
                        Email = Email,
                        Gender = (byte)Gender,
                        DateOfBirth = DoB
                    };

                    AccountManager.RegisterAccount(new_user);

                    Error.Foreground = Brushes.Green;
                    Error.Text = "Congratulations! You are successfully signed up!";
                }
                catch (Exception ex)
                {
                    Error.Foreground = Brushes.Red;
                    Error.Text = ex.Message;
                    return;
                }
            }
        }

    }
}