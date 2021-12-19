using GoninDigital.Models;
using GoninDigital.Properties;
using GoninDigital.Utils;
using Microsoft.Win32;
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
    /// Interaction logic for AddAdDialog.xaml
    /// </summary>
    public partial class AddAdDialog : UserControl
    {
        public AddAdDialog()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string title = txb_Title.Text;
            string subtitle = txb_Subtitle.Text;
            string cover = txb_Cover.Text;
            if (title == "" | subtitle == "")
            {
                Error.Foreground = Brushes.Red;
                Error.Text = "Please enter full information";
            }
            else if(cover=="")
            {
                Error.Foreground = Brushes.Red;
                Error.Text = "Please choose image";
            }
            else
            {
                btn_Add.IsEnabled = false;
                Error.Foreground = Brushes.Black;
                Error.Text = "Waiting...";
                Ad ad = new Ad();
                var linkCover = await ImageUploader.UploadAsync(txb_Cover.Text);
                ad.Title = title;
                ad.Subtitle = subtitle;
                ad.Cover = linkCover;
                using (var db=new GoninDigitalDBContext())
                {
                    db.Ads.Add(ad);
                    _ = db.SaveChanges();
                }
                Error.Foreground = Brushes.Green;
                Error.Text = "Add new Ad successfully";
                btn_Add.IsEnabled = true;
            }
        }

        private void btn_Cover_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Title = "Choose Image..";

            openFileDialog.InitialDirectory = @"C:\";
            openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                txb_Cover.Text = openFileDialog.FileName;
            }
        }

    }
}
