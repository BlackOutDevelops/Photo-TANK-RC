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
            BluetoothClient client = await Task.Run(() => SenderService.ConnectDevice(SelectedDevice));
            if (client.Connected)
            {
                if (ConnectedDevices != null && !ConnectedDevices.Contains(SelectedDevice))
                {
                    System.Diagnostics.Debug.WriteLine("Connected Sucessfully");
                    ConnectedDevices.Add(SelectedDevice);
                }
            }
        }
        #endregion
    }
}
