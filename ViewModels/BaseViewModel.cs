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
        public BaseViewModel()
		{
		}

        public event PropertyChangedEventHandler PropertyChanged;

        public void FirePropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }   
    }
}
