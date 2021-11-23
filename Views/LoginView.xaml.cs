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
using GoninDigital.ViewModels;
using ModernWpf.Demo.ThreadedUI;

namespace GoninDigital.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
        }

        public ThreadedUIPageViewModel ViewModel { get; } = new ThreadedUIPageViewModel();

        private void ProgressControlHost_ChildChanged(object sender, EventArgs e)
        {
            var host = (ThreadedVisualHost)sender;
            if (host.Child is ThreadedProgressBar progressBar)
            {
                progressBar.SetBinding(ThreadedProgressBar.IsIndeterminateProperty, new Binding(nameof(ViewModel.IsBusy)) { Source = ViewModel });
            }
            else if (host.Child is ThreadedProgressRing progressRing)
            {
                progressRing.SetBinding(ThreadedProgressRing.IsActiveProperty, new Binding(nameof(ViewModel.IsBusy)) { Source = ViewModel });
            }
        }
    }
    public class ThreadedUIPageViewModel : BaseViewModel
    {
        private bool _isBusy = true;
        public bool IsBusy
        {
            get => _isBusy;
            set => Set(ref _isBusy, value);
        }

        private bool _isVisbile = true;
        public bool IsVisible
        {
            get => _isVisbile;
            set => Set(ref _isVisbile, value);
        }
    }
}
