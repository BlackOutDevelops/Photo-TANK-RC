using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoshsTestApp
{
	internal class ViewModelsHolder
	{
        public AvailableBTDeviceViewModel AvailableBTDeviceViewModel;
        public MainWindowViewModel MainWindowViewModel;

        public ViewModelsHolder()
        {
            AvailableBTDeviceViewModel = new AvailableBTDeviceViewModel();
            MainWindowViewModel = new MainWindowViewModel();
        }
    }
}
