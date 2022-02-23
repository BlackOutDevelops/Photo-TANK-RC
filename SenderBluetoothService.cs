using InTheHand.Net;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;

namespace JoshsTestApp
{
    class SenderBluetoothService
    {
        private readonly Guid _serviceClassId;
        public SenderBluetoothService()
        {
            _serviceClassId = new Guid("00000000-0000-0000-0000-000000000000");
        }

        public async Task<ObservableCollection<Device>> GetDevices()
        {
            Task<ObservableCollection<Device>> task = Task.Run(() =>
            {
                ObservableCollection<Device> devices = new ObservableCollection<Device>();
                using (BluetoothClient client = new BluetoothClient())
                {
                    IReadOnlyCollection<BluetoothDeviceInfo> temp = client.DiscoverDevices();

                    foreach (BluetoothDeviceInfo deviceInfo in temp)
                    {
                        Application.Current.Dispatcher.Invoke(() => devices.Add(new Device(deviceInfo)));
                        System.Diagnostics.Debug.WriteLine("Added a device!");
                    }
                }
                return devices;
            });
            System.Diagnostics.Debug.WriteLine("Here");
            return await task;
        }

        public async Task<BluetoothClient> ConnectDevice(Device pendingDevice)
        {
            var task = Task.Run(() =>
            {
                using (BluetoothClient client = new BluetoothClient())
                {
                    try
                    {
                        BluetoothEndPoint endpoint = new BluetoothEndPoint(pendingDevice.DeviceInfo.DeviceAddress, BluetoothService.PhonebookAccess);
                        
                        client.ConnectAsync(endpoint);
                        while (client.Connected)
                        {
                            

                        }
                        return client;
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.HelpLink);
                        return null;
                    }
                }
            });
            return await task;
        }
    }
}
