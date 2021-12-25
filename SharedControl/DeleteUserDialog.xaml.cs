using GoninDigital.Models;
using ModernWpf.Controls;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for DeleteUserDialog.xaml
    /// </summary>
    public partial class DeleteUserDialog : UserControl
    {
        private int UserId;
        public DeleteUserDialog(int UserId)
        {
            InitializeComponent();
            this.UserId = UserId;
        }

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            string reason = txb_Reason.Text;
            string enddate = dp_EndDate.Text;
            if (reason == "" | enddate == "")
            {
                Error.Foreground = Brushes.Red;
                Error.Text = "Please enter full information";
            }
            else 
            {
                int IsCheck = 0;
                using(var db=new GoninDigitalDBContext())
                {
                    IsCheck = db.Bans.Where(x => x.UserId == UserId).Count();
                }
                if (IsCheck != 0)
                {
                    Error.Foreground = Brushes.Red;
                    Error.Text = "This account has been banned";
                }
                else
                {
                    Ban ban = new Ban();
                    ban.UserId = UserId;
                    ban.Reason = reason;
                    ban.EndDate = DateTime.ParseExact(enddate, "M/d/yyyy", CultureInfo.InvariantCulture);
                    using (var db = new GoninDigitalDBContext())
                    {
                        db.Bans.Add(ban);
                        _ = db.SaveChanges();
                    }
                    Error.Foreground = Brushes.Green;
                    Error.Text = "Banned Successfully";
                }

            }    
        }
    }
}
