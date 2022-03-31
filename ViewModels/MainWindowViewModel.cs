using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace JoshsTestApp
{
    class MainWindowViewModel: BaseViewModel
    {
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

        public MainWindowViewModel()
        {
            MaxWidthStream = 960;
            MaxHeightStream = 540;
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
    }
}
