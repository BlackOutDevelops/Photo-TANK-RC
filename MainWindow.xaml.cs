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

namespace JoshsTestApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    partial class MainWindow : Window
    {
        //private MainWindowViewModel mainViewModel = new MainWindowViewModel();
        private AvailableBTDeviceViewModel availableDeviceVM = new AvailableBTDeviceViewModel();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = availableDeviceVM;
            Application.Current.MainWindow.WindowState = WindowState.Maximized;
        }

        private void OnClickHandler(object sender, RoutedEventArgs e)
        {
            bool isWindowOpen = false;
            foreach (Window w in Application.Current.Windows)
            {
                if (w is AvailableBTDeviceWindow)
                {
                    isWindowOpen = true;
                    w.Activate();
                }
            }

            if (!isWindowOpen)
            {
                AvailableBTDeviceWindow bluetoothWindow = new AvailableBTDeviceWindow
                {
                    DataContext = availableDeviceVM
                };
                bluetoothWindow.Show();
            }
        }
    }
}
