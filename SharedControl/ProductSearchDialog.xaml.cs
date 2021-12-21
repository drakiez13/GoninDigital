using GoninDigital.Models;
using GoninDigital.Utils;
using GoninDigital.ViewModels;
using Microsoft.EntityFrameworkCore;
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
    /// Interaction logic for ProductSearchDialog.xaml
    /// </summary>
    public partial class ProductSearchDialog : UserControl
    {
        public ProductSearchDialog()
        {
            InitializeComponent();
        }

        public void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                var content = sender.Text;
                using (var context = new GoninDigitalDBContext())
                {
                    var productResult = context.Products
                        .Include(o => o.Vendor)
                        .Where(
                            product => product.StatusId == (int)Constants.ProductStatus.ACCEPTED &&
                                product.Vendor.ApprovalStatus == (int)Constants.ApprovalStatus.APPROVED
                            && product.Name.Contains(content)
                        ).ToList();

                    if (productResult.Any())
                        sender.ItemsSource = productResult;
                    else
                        sender.ItemsSource = new List<Product>()
                        {
                            new Product {
                                Name = "No Results Found",
                                Image = null, Description = null,
                            }
                        };
                }
            }
        }

        private void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (args.ChosenSuggestion != null)
            {
                Product searchItem = (Product)args.ChosenSuggestion;

                (DataContext as AdsPageViewModel).ProductIdToAdd = searchItem.Id;
            }
            else
            {
                // Use args.QueryText to determine what to do.
                //MessageBox.Show((string)args.QueryText);
                /*navigationView.IsPaneOpen = false;
                RootFrame.Navigate(new SearchResultPage(args.QueryText));*/
            }

        }
    }
}
