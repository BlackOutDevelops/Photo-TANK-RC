using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using InTheHand.Net;
using JoshTestApp;

namespace JoshsTestApp
{
    class MainWindowViewModel: BaseViewModel
    {
        public SenderBluetoothService SenderService = new SenderBluetoothService();
        private string DataToSend = null;
        private string PreviousData = null;

        private double _maxWidthStream;
        public double MaxWidthStream
        {
            get { return _maxWidthStream; }
            set
            {
                if (value != _maxWidthStream)
                {
                    _maxWidthStream = value;
                    FirePropertyChanged(nameof(MaxWidthStream));
                }
            }
        }

        private double _maxHeightStream;
        public double MaxHeightStream
        {
            get { return _maxHeightStream; }
            set
            {
                if (value != _maxHeightStream)
                {
                    _maxHeightStream = value;
                    FirePropertyChanged(nameof(MaxHeightStream));
                }
            }
        }

        private Task<ObservableCollection<Device>> _bluetoothDevicesTasks;
        public Task<ObservableCollection<Device>> BluetoothDevicesTasks
        {
            get { return _bluetoothDevicesTasks; }
            set
            {
                if (value != _bluetoothDevicesTasks)
                {
                    _bluetoothDevicesTasks = value;
                    FirePropertyChanged(nameof(BluetoothDevicesTasks));
                }
            }
        }

        private Device _selectedDevice;
        public Device SelectedDevice
        {
            get { return _selectedDevice; }
            set
            {
                if (value != _selectedDevice)
                {
                    _selectedDevice = value;
                    FirePropertyChanged(nameof(SelectedDevice));
                }
            }
        }

        private ObservableCollection<Device> _bluetoothDevices;
        public ObservableCollection<Device> BluetoothDevices
        {
            get { return _bluetoothDevices; }
            set
            {
                if (value != _bluetoothDevices)
                {
                    _bluetoothDevices = value;
                    FirePropertyChanged(nameof(BluetoothDevices));
                }
            }
        }

        private Device _connectedDevice;
        public Device ConnectedDevice
        {
            get { return _connectedDevice; }
            set
            {
                if (value != _connectedDevice)
                {
                    _connectedDevice = value;
                    FirePropertyChanged(nameof(ConnectedDevice));
                }
            }
        }
        public MainWindowViewModel()
        {
            BluetoothDevices = new ObservableCollection<Device>();
            MaxWidthStream = 960;
            MaxHeightStream = 540;
            SenderService.PropertyChanged += SenderService_PropertyChanged;
        }

        public void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var window = sender as MainWindow;
            
            if (window == null)
                return; 

            if ((e.NewSize.Width % 16 == 0 || e.NewSize.Height % 9 == 0) && window.WindowState != WindowState.Maximized)
            {
                MaxWidthStream = CalculateSize(window.Width / 2, 16);
                MaxHeightStream = CalculateSize(window.Height / 2, 9);
            }
        }

        public void Window_StateChanged(object sender, EventArgs e)
        {
            var window = sender as MainWindow;

            if (window == null)
                return;

            if (window.WindowState == WindowState.Maximized)
            {
                MaxWidthStream = 960;
                MaxHeightStream = 540;
            }
            else if (window.WindowState == WindowState.Normal)
            {
                MaxWidthStream = CalculateSize(window.Width / 2, 16);
                MaxHeightStream = CalculateSize(window.Height / 2, 9);
            }
        }

        public double CalculateSize(double n, double multiple)
        {
            if (n > 0)
                return Math.Ceiling(n / multiple) * multiple;
            else if (n < 0)
                return Math.Floor(n / multiple) * multiple;
            else
                return multiple;
        }

        public void Joystick1_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var joy1 = sender as Joystick;
            if (e.PropertyName == nameof(joy1.OutputJoystickCoordinateX) || e.PropertyName == nameof(joy1.OutputJoystickCoordinateY))
            {
                double angle = CalculateAngle(new Point(joy1.OutputJoystickCoordinateX, joy1.OutputJoystickCoordinateY));
                switch (angle)
                {
                    case >= 337.5 and <= 360:
                        DataToSend = "1";
                        SendToDevice(DataToSend);
                        break;
                    case >= 0 and < 22.5:
                        DataToSend = "1";
                        SendToDevice(DataToSend);
                        break;
                    case >= 22.5 and < 67.5:
                        DataToSend = "2";
                        SendToDevice(DataToSend);
                        break;
                    case >= 67.5 and < 112.5:
                        DataToSend = "3";
                        SendToDevice(DataToSend);
                        break;
                    case >= 112.5 and < 157.5:
                        DataToSend = "4";
                        SendToDevice(DataToSend);
                        break;
                    case >= 157.5 and < 202.5:
                        DataToSend = "5";
                        SendToDevice(DataToSend);
                        break;
                    case >= 202.5 and < 247.5:
                        DataToSend = "6";
                        SendToDevice(DataToSend);
                        break;
                    case >= 247.5 and < 292.5:
                        DataToSend = "7";
                        SendToDevice(DataToSend);
                        break;
                    case >= 292.5 and < 337.5:
                        DataToSend = "8";
                        SendToDevice(DataToSend);
                        break;
                }
            }
        }

        public void Joystick2_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var joy2 = sender as Joystick;
            if (e.PropertyName == nameof(joy2.OutputJoystickCoordinateX) || e.PropertyName == nameof(joy2.OutputJoystickCoordinateY))
            {
                double angle = CalculateAngle(new Point(joy2.OutputJoystickCoordinateX, joy2.OutputJoystickCoordinateY));
                System.Diagnostics.Debug.WriteLine(angle);
            }
        }

        public void Joystick1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            DataToSend = "0";
            Thread.Sleep(100);
            SendToDevice(DataToSend);
        }

        private double CalculateAngle(Point coordinates)
        {
            double xRadians = coordinates.X * (Math.PI / 180);
            double yRadians = coordinates.Y * (Math.PI / 180);
            double radians = Math.Atan2(xRadians, yRadians);
            double angle = radians * (180 / Math.PI);
            
            if (angle < 0)
                angle += 360;

            return angle;
        }

        public void FireButtonClicked(object sender, RoutedEventArgs e)
        {
            if (DataToSend.Equals("1"))
            {
                SendToDevice(DataToSend);
                DataToSend = DataToSend.Replace('1', '2');
            }
            else if (DataToSend.Equals("2"))
            {
                SendToDevice(DataToSend);
                DataToSend = DataToSend.Replace('2', '1');
            }
        }

        private void SenderService_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SenderService.ConnectedDevice))
            {
                ConnectedDevice = SenderService.ConnectedDevice;
            }
        }

        #region Commands
        private ICommand _getDevicesCommand;
        public ICommand GetDevicesCommand => _getDevicesCommand ??= new CommandHandler((a) => GetDevices(), () => true);
        private ICommand _connectToDeviceCommand;
        public ICommand ConnectToDeviceCommand => _connectToDeviceCommand ??= new CommandHandler((a) => ConnectToDevice(), () => true);
        private ICommand _DisconnectCommand;
        public ICommand DisconnectCommand => _DisconnectCommand ??= new CommandHandler((a) => DisconnectFromDevice(), () => true);

        public async void GetDevices()
        {
            ObservableCollection<Device> temp = await Task.Run(() => BluetoothDevicesTasks = SenderService.GetDevices());

            BluetoothDevices.Clear();
            foreach (Device pendingDevice in temp)
            {
                BluetoothDevices.Add(pendingDevice);
            }
        }

        //private void StoreDevice()
        //{
        //    if (StoredDevice != null && !StoredDevice.Equals(SelectedDevice))
        //    {
        //        StoredDevice = SelectedDevice;
        //        System.Diagnostics.Debug.WriteLine("Exists");
        //    }
        //    else
        //    {
        //        StoredDevice = new Device(SelectedDevice.DeviceInfo);
        //        System.Diagnostics.Debug.WriteLine("Doesn't Exist");
        //    }
        //}

        private void SendToDevice(String data)
        {
            if (PreviousData != DataToSend)
                _ = Task.Run(() => SenderService.SendToDevice(data));
            
            if (PreviousData == null)
                PreviousData = DataToSend;
            else
            {
                PreviousData = String.Empty;
                PreviousData = DataToSend;
            }

            DataToSend = String.Empty;
        }
        private async void ConnectToDevice()
        {
            await Task.Run(() => SenderService.ConnectToDevice(SelectedDevice));
        }
        
        private async void DisconnectFromDevice()
        {
            await Task.Run(() => SenderService.Disconnect());
        }
        #endregion
    }
}
