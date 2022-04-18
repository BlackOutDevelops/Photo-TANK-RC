using InTheHand.Net;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;

namespace JoshsTestApp
{
    class SenderBluetoothService : INotifyPropertyChanged
    {
        private BluetoothListener Listener;
        private BluetoothClient Client;
        private readonly Guid _serviceClassId;

        public event PropertyChangedEventHandler PropertyChanged;

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

        private string _receivedString;
        public string ReceivedString
        {
            get { return _receivedString; }
            set
            {
                if (value != _receivedString)
                {
                    _receivedString = value;
                    FirePropertyChanged(nameof(ReceivedString));
                }
            }
        }

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
            return await task;
        }

        public async Task ConnectToDevice(Device pendingDevice)
        {
            bool isPrinted = false;
            await Task.Run(() =>
            {
                    try
                    {
                        if (Client == null)
                            Client = new BluetoothClient();

                        BluetoothEndPoint endpoint = new BluetoothEndPoint(pendingDevice.DeviceInfo.DeviceAddress, BluetoothService.SerialPort);

                        Client.ConnectAsync(endpoint);
                        _ = StartReceiveFromDevice();
                        while (Client.Connected)
                        {
                            if (!isPrinted)
                            {
                                System.Diagnostics.Debug.WriteLine("Connected");
                                System.Diagnostics.Debug.Flush();
                                ConnectedDevice = pendingDevice;
                                isPrinted = true;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Client.Close();
                        System.Diagnostics.Debug.WriteLine(ex.HelpLink);
                    }
            });
        }

        public async Task SendToDevice(String data)
        {
            await Task.Run(() =>
            {
                var stream = Client.GetStream();
                var buffer = Encoding.ASCII.GetBytes(data);
                System.Diagnostics.Debug.WriteLine("Stream is: " + data);
                stream.WriteAsync(buffer);
                stream.FlushAsync();
            });
        }

        public async Task StartReceiveFromDevice()
        {
            await Task.Run(() =>
            {
                try
                {
                    while (Client.Connected)
                    {
                        var stream = Client.GetStream();
                        byte[] received = new byte[1024];
                        stream.Read(received, 0, received.Length);
                        ReceivedString = Encoding.ASCII.GetString(received);
                    }
                }
                catch (IOException ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.HelpLink);
                }
            });
        }

        public async Task Disconnect()
        {
            await Task.Run(() =>
            {
                Client.Close();
                Client = null;
                System.Diagnostics.Debug.WriteLine("Disconnecting from " + ConnectedDevice.DeviceName);
                ConnectedDevice = null;
            });
        }

        public void FirePropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        //public async Task ConnectToDevice(Device pendingDevice)
        //{
        //    await Task.Run(() =>
        //    {
        //        using (BluetoothClient client = new BluetoothClient())
        //        {
        //            try
        //            {
        //                BluetoothEndPoint endpoint = new BluetoothEndPoint(pendingDevice.DeviceInfo.DeviceAddress, BluetoothService.SerialPort);

        //                client.ConnectAsync(endpoint);
        //                var stream = client.GetStream();
        //                System.Diagnostics.Debug.WriteLine(data);
        //                while (client.Connected)
        //                {
        //                    if (stream != null)
        //                    {
        //                        var buffer = System.Text.Encoding.ASCII.GetBytes(data);
        //                        stream.Write(buffer);
        //                        stream.Flush();
        //                        stream.Close();
        //                        client.Close();
        //                    }
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                client.Close();
        //                System.Diagnostics.Debug.WriteLine(ex.HelpLink);
        //            }
        //        }
        //    });
        //}
    }
}
