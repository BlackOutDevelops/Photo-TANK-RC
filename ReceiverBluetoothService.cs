using InTheHand.Net.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoshsTestApp
{
    class ReceiverBluetoothService
    {
        private bool _isStarted;
        private readonly Guid _serviceClassId;
        private Action<string> _serviceAction;
        private BluetoothListener _listener;
        
        public ReceiverBluetoothService()
        {
            _serviceClassId = new Guid("893a29da-8afe-4e3a-8af4-a87df5a4f234");
        }
        public void Start(Action<string> serviceAction)
        {
            _isStarted = true;
            _serviceAction = serviceAction;
            _listener = new BluetoothListener(_serviceClassId)
            {
                ServiceName = "GetFeed"
            };
            _listener.Start();
        }

        public void Stop()
        {
            _isStarted = false;
        }
    }
}
