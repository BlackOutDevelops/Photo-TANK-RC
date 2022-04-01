using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InTheHand.Net;
using InTheHand.Net.Sockets;

namespace JoshsTestApp
{
    class Device
    {
        public string DeviceName { get; set; }
        public bool IsAuthenticated { get; set; }
        public bool IsRemembered { get; set; }
        public bool IsConnected { get; set; }
        public DateTime LastSeen { get; set; }
        public DateTime LastUsed { get; set; }
        public IReadOnlyCollection<Guid> InstalledServices { get; set; }
        public BluetoothDeviceInfo DeviceInfo { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Device"/> class.
        /// </summary>
        /// <param name="device_info">
        /// The device_info.
        /// </param>
        public Device(BluetoothDeviceInfo device_info)
        {
            if (device_info != null)
            {
                DeviceName = device_info.DeviceName;
                DeviceInfo = device_info;
                IsAuthenticated = device_info.Authenticated;
                IsRemembered = device_info.Remembered;
                IsConnected = device_info.Connected;
                LastSeen = device_info.LastSeen;
                LastUsed = device_info.LastUsed;
                InstalledServices = device_info.InstalledServices;
            }
        }

        // TESTING
        public Device()
        {
                DeviceName = "Test";
                DeviceInfo = null;
                IsAuthenticated = true;
                IsRemembered = true;
                IsConnected = true;
                LastSeen = new DateTime();
                LastUsed = new DateTime();
                InstalledServices = null;
        }
    }
}
