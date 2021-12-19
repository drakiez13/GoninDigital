using GoninDigital.Models;
using GoninDigital.Properties;
using GoninDigital.ViewModels;
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
using Page = ModernWpf.Controls.Page;

namespace GoninDigital.Views.SharedPages
{
    public partial class ProductListPage : Page
    {
        public static Stack<Parameter> OldParameters { get; set; } = new Stack<Parameter>();
        public struct Parameter
        {
            public List<Product> products { get; set; }
            public string title { get; set; }
            public string subtitle { get; set; }
            public string cover { get; set; }
        }

        public List<Product> Products { get; set; }
        public string PageTitle { get; set; }
        public string Subtitle { get; set; }
        public string Cover { get; set; }
        private Parameter parameters;

        public ProductListPage(Parameter parameters)
        {
            Products = parameters.products;
            PageTitle = parameters.title;
            Subtitle = parameters.subtitle;
            Cover = parameters.cover;

            this.parameters = parameters;

            InitializeComponent();
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            if (e.NavigationMode != NavigationMode.Back)
                OldParameters.Push(parameters);
        }

        public ProductListPage() : this(OldParameters.Pop()) { }
    }
}
