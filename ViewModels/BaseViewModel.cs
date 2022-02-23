using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using InTheHand.Net.Sockets;
using InTheHand.Net;

namespace JoshsTestApp
{
    class BaseViewModel : INotifyPropertyChanged
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

        public event PropertyChangedEventHandler PropertyChanged;

        public void FirePropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        //public void CheckConnectedDevices()
        //{
            
            // Create      List of USBDeviceInfoClass with _devices named
            //ManagementClass classes = new ManagementClass(new ManagementPath("win32_NetworkAdapter"));
            //classes.Options.UseAmendedQualifiers = true;
            //PropertyDataCollection properties = classes.Properties;

            //System.Diagnostics.Debug.WriteLine("Win32_Process Property Names: ");
            //foreach (PropertyData property in properties)
            //{
            //    System.Diagnostics.Debug.WriteLine(property.Name);

            //    foreach (QualifierData q in property.Qualifiers)
            //    {
            //        if (q.Name.Equals("Description"))
            //        {
            //            System.Diagnostics.Debug.WriteLine(
            //                classes.GetPropertyQualifierValue(
            //                property.Name, q.Name) + "\n");
            //        }
            //    }
            //    Console.WriteLine();
            //}

            //ManagementObjectCollection collection; // Create a object of ManagementObjectCollection class which is hold the list of connected devices    
            //using (var searcher = new ManagementObjectSearcher("select * from win32_PnPEntity where PNPClass = 'Bluetooth' and not Name like '%Bluetooth%' and not Name like '%Generic%' and not Name like '%Service%'"))// Fetch all devices with the help of WQL (Windows Query Language)
            //{
            //    collection = searcher.Get();// save the list of connected devices with this statement to collection
            //}
 
            //foreach (var device in collection) // Set the DeviceID, PNPDeviceID and Description in _devices object properties.
            //{
            //    System.Diagnostics.Debug.WriteLine(device.GetPropertyValue("Name"));
                //BluetoothDeviceInfo deviceInfo = new BluetoothDeviceInfo((BluetoothAddress)device.GetPropertyValue("Association EndPoint Address"));
                //ConnectedDevices.Add(new Device(
                //(string) device.GetPropertyValue("DeviceID"),
                //(string) device.GetPropertyValue("Display name"),
                //(string) device.GetPropertyValue("Description")
                //));

            //}
    }
}
