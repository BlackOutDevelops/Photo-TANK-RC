using InTheHand.Net;
using InTheHand.Net.Sockets;
using JoshTestApp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace JoshsTestApp
{
    class AvailableBTDeviceViewModel : BaseViewModel
    {
        public SenderBluetoothService SenderService = new SenderBluetoothService();

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

        private ObservableCollection<Device> _connectedDevices;
		public ObservableCollection<Device> ConnectedDevices
        {
            get { return _connectedDevices; }
            set
            {
                if (value != _connectedDevices)
                {
                    _connectedDevices = value;
                    FirePropertyChanged(nameof(ConnectedDevices));
                }
            }
        }
        public AvailableBTDeviceViewModel()
        {
            ConnectedDevices = new ObservableCollection<Device>();
            BluetoothDevices = new ObservableCollection<Device>();
        }

        #region Commands
        private ICommand _getDevicesCommand;
        public ICommand GetDevicesCommand => _getDevicesCommand ??= new CommandHandler((a) => GetDevices(), () => true);
        private ICommand _connectToDeviceCommand;
        public ICommand ConnectToDeviceCommand => _connectToDeviceCommand ??= new CommandHandler((a) => ConnectDevice(), () => true);

        public async void GetDevices()
        {
            ObservableCollection<Device> temp = await Task.Run(() => BluetoothDevicesTasks = SenderService.GetDevices());

            BluetoothDevices.Clear();
            foreach (Device pendingDevice in temp)
            {
                BluetoothDevices.Add(pendingDevice);
            }
        }

        private async void ConnectDevice()
        {
            if (ConnectedDevices != null && !ConnectedDevices.Contains(SelectedDevice))
            {
                ConnectedDevices.Add(SelectedDevice);
            }
            BluetoothEndPoint endPoint = await Task.Run(() => SenderService.ConnectDevice(SelectedDevice));
			ConnectedDevices.Remove(SelectedDevice);
		}
		#endregion
	}
}
