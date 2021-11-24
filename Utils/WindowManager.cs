using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GoninDigital.Utils
{
    public class WindowManager
    {
        public static Window ChangeWindowContent(Window window, object viewModel, string title, string controlPath, int h=600, int w=1000)
        {
            window.Title = title;
            //window.Background = Brushes.White;
            //window.Foreground = Brushes.Black;
            window.Height = h;
            window.Width = w;
            window.WindowState = WindowState.Normal;
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //window.Icon = BitmapFrame.Create(new Uri("pack://application:,,,/GoninDigital;component/Resources/Icon.ico", UriKind.RelativeOrAbsolute));

            var controlAssembly = Assembly.Load("GoninDigital");
            var controlType = controlAssembly.GetType(controlPath);
            var newControl = Activator.CreateInstance(controlType) as UserControl;
            newControl.DataContext = viewModel;
            window.Content = newControl;

            return window;
        }
        public static Window ChangeWindowContent(Window window, string title, string controlPath, int h = 600, int w = 1000)
        {
            window.Title = title;
            //window.Background = Brushes.White;
            //window.Foreground = Brushes.Black;
            window.Height = h;
            window.Width = w;
            window.WindowState = WindowState.Normal;
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //window.Icon = BitmapFrame.Create(new Uri("pack://application:,,,/GoninDigital;component/Resources/Icon.ico", UriKind.RelativeOrAbsolute));

            var controlAssembly = Assembly.Load("GoninDigital");
            var controlType = controlAssembly.GetType(controlPath);
            var newControl = Activator.CreateInstance(controlType) as UserControl;
            window.Content = newControl;

            return window;
        }
    }
}
