using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace JoshsTestApp
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	partial class MainWindow : Window
	{
		private ViewModelsHolder viewModels = new ViewModelsHolder();

		public MainWindow()
		{
			InitializeComponent();
			string currentDirectory = "C:\\Program Files\\VideoLAN\\VLC";
			DirectoryInfo vlcLibDirectory = new DirectoryInfo(currentDirectory);

			var options = new string[]
			{
                "--network-caching=210",
                "--live-caching=50",
                "--clock-jitter=10",
                "--input-fast-seek"
				// VLC options can be given here. Please refer to the VLC command line documentation.
			};

			NanoStream.SourceProvider.CreatePlayer(vlcLibDirectory, options);
			FileInfo fileInfo = new FileInfo(currentDirectory + "\\StreamForNano.sdp");
			// Load libvlc libraries and initializes stuff. It is important that the options (if you want to pass any) and lib directory are given before calling this method.
			NanoStream.SourceProvider.MediaPlayer.Play(fileInfo);
			DataContext = viewModels.MainWindowViewModel;
			Application.Current.MainWindow.Title = "Photo-TANK Controller";
			Application.Current.MainWindow.WindowState = WindowState.Maximized;
            Window_StateChanged(this, EventArgs.Empty);
			Joystick1.PropertyChanged += Joystick1_PropertyChanged;
			Joystick2.PropertyChanged += Joystick2_PropertyChanged;
			Joystick1.MouseUp += Joystick1_MouseUp;
			Joystick2.MouseUp += Joystick2_MouseUp;
		}

		//private void OnClickHandler_BluetoothDevices(object sender, RoutedEventArgs e)
		//{
		//	bool isWindowOpen = false;
		//	foreach (Window w in Application.Current.Windows)
		//	{
		//		if (w is AvailableBTDeviceWindow)
		//		{
		//			isWindowOpen = true;
		//			w.Activate();
		//		}
		//	}

		//	if (!isWindowOpen)
		//	{
		//		AvailableBTDeviceWindow bluetoothWindow = new AvailableBTDeviceWindow
		//		{
		//			DataContext = viewModels.AvailableBTDeviceViewModel
		//		};
		//		bluetoothWindow.Show();
		//	}
		//}

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
			viewModels.MainWindowViewModel.Window_SizeChanged(sender, e);
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
			viewModels.MainWindowViewModel.Window_StateChanged(sender, e);
        }

		private void Joystick1_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
			viewModels.MainWindowViewModel.Joystick1_PropertyChanged(sender, e);
        }

		private void Joystick2_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			viewModels.MainWindowViewModel.Joystick2_PropertyChanged(sender, e);
		}

		private void Joystick1_MouseUp(object sender, MouseButtonEventArgs e)
        {
			viewModels.MainWindowViewModel.Joystick1_MouseUp(sender, e);
		}

		private void Joystick2_MouseUp(object sender, MouseButtonEventArgs e)
		{
			viewModels.MainWindowViewModel.Joystick2_MouseUp(sender, e);
		}

		private void FireButtonClicked(object sender, RoutedEventArgs e)
        {
			viewModels.MainWindowViewModel.FireButtonClicked(sender, e);
        }
    }
}
