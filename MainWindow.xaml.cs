using System;
using System.Collections.Generic;
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
                // VLC options can be given here. Please refer to the VLC command line documentation.
            };

			NanoStream.SourceProvider.CreatePlayer(vlcLibDirectory, options);

			// Load libvlc libraries and initializes stuff. It is important that the options (if you want to pass any) and lib directory are given before calling this method.
			NanoStream.SourceProvider.MediaPlayer.Play(@"rtsp://192.168.50.220:5000/test");
			DataContext = viewModels.AvailableBTDeviceViewModel;	
			Application.Current.MainWindow.WindowState = WindowState.Maximized;

		}

		private void OnClickHandler_BluetoothDevices(object sender, RoutedEventArgs e)
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
					DataContext = viewModels.AvailableBTDeviceViewModel
				};
				bluetoothWindow.Show();
			}
		}

		private void OnClickHandler_JetsonConnection(object sender, RoutedEventArgs e)
		{
			//bool isWindowOpen = false;
			//foreach (Window w in Application.Current.Windows)
			//{
			//	if (w is JetsonConnectionStatus)
			//	{
			//		isWindowOpen = true;
			//		w.Activate();
			//	}
			//}

			//if (!isWindowOpen)
			//{
			//	JetsonConnectionStatus jetsonWindow = new JetsonConnectionStatus
			//	{
			//		DataContext = viewModels.JetsonConnectionStatusViewModel
			//	};
			//	jetsonWindow.Show();
			//}
			//if (!viewModels.JetsonConnectionStatusViewModel.JetsonIsConnected)
			//	viewModels.JetsonConnectionStatusViewModel.GetStream();
		}
	}
}
