using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JoshsTestApp
{
    /// <summary>
    /// Interaction logic for Joystick.xaml
    /// </summary>
    public partial class Joystick : UserControl, INotifyPropertyChanged
    {
        private Point _mouseOriginalPosition;
        private Point _joystickTopOriginalPosition;
        private Point _outputJoystickCoordinates;
        private Point _centerOfJoystickBase;
        private Point _centerOfJoystickTop;

        private double _outputJoystickCoordinateX;
        public double OutputJoystickCoordinateX
        {
            get { return _outputJoystickCoordinateX; }
            set
            {
                if (value != _outputJoystickCoordinateX)
                {
                    _outputJoystickCoordinateX = value;
                    FirePropertyChanged(nameof(OutputJoystickCoordinateX));
                }
            }
        }

        private double _outputJoystickCoordinateY;
        public double OutputJoystickCoordinateY
        {
            get { return _outputJoystickCoordinateY; }
            set
            {
                if (value != _outputJoystickCoordinateY)
                {
                    _outputJoystickCoordinateY = value;
                    FirePropertyChanged(nameof(OutputJoystickCoordinateY));
                }
            }
        }

        public Joystick()
        {
            InitializeComponent();
        }

        private void HandleEllipseMouseDown(object sender, MouseButtonEventArgs e)
        {
            _mouseOriginalPosition = Mouse.GetPosition(JoystickTop);
            _joystickTopOriginalPosition = new Point(Canvas.GetLeft(JoystickTop), Canvas.GetTop(JoystickTop));

            JoystickTop.CaptureMouse();
        }

        private void HandleMouseUp(object sender, MouseButtonEventArgs e)
        {
            JoystickTop.ReleaseMouseCapture();
            Canvas.SetLeft(JoystickTop, _joystickTopOriginalPosition.X);
            Canvas.SetTop(JoystickTop, _joystickTopOriginalPosition.Y);
            GetXandYCoordinates(_centerOfJoystickBase, _centerOfJoystickTop);
        }

        private void HandleMouseMove(object sender, MouseEventArgs e)
        {
            if (JoystickTop.IsMouseCaptured)
            {
                Point mousePosition = Mouse.GetPosition(JoystickCanvas);
                _centerOfJoystickTop = new(Canvas.GetLeft(JoystickTop) + JoystickTop.Width / 2, Canvas.GetTop(JoystickTop) + JoystickTop.Height / 2);
                _centerOfJoystickBase = new(Canvas.GetLeft(JoystickBase) + JoystickBase.Width / 2, Canvas.GetTop(JoystickBase) + JoystickBase.Height / 2);
                double xCenter = Canvas.GetLeft(JoystickBase) + JoystickBase.Width / 2;
                double yCenter = Canvas.GetTop(JoystickBase) + JoystickBase.Height / 2;
                double distanceFromMouse = Math.Sqrt(Math.Pow(mousePosition.X - xCenter, 2) + Math.Pow(mousePosition.Y - yCenter, 2));
                double radiusJoystickBase = JoystickBase.Width / 2;
                double radiusJoystickTop = JoystickTop.Width / 2;
                if (Math.Sqrt(Math.Pow(_centerOfJoystickTop.X - xCenter, 2) + Math.Pow(_centerOfJoystickTop.Y - yCenter, 2)) < radiusJoystickBase
                    || Math.Sqrt(Math.Pow(mousePosition.X - xCenter, 2) + Math.Pow(mousePosition.Y - yCenter, 2)) < radiusJoystickBase - 5)
                {
                    Canvas.SetLeft(JoystickTop, mousePosition.X - _mouseOriginalPosition.X);
                    Canvas.SetTop(JoystickTop, mousePosition.Y - _mouseOriginalPosition.Y);
                }
                else
                {
                    double deltaX = Math.Ceiling(xCenter - ((radiusJoystickBase + 2) * (xCenter - mousePosition.X) / distanceFromMouse));
                    double deltaY = Math.Ceiling(yCenter - ((radiusJoystickBase + 2) * (yCenter - mousePosition.Y) / distanceFromMouse));

                    Canvas.SetLeft(JoystickTop, deltaX - radiusJoystickTop);
                    Canvas.SetTop(JoystickTop, deltaY - radiusJoystickTop);
                }
                GetXandYCoordinates(_centerOfJoystickBase, _centerOfJoystickTop);
            }
        }

        public void GetXandYCoordinates(Point centerOfJoystickBase, Point centerOfJoystickTop)
        {
            _centerOfJoystickTop = new(Canvas.GetLeft(JoystickTop) + JoystickTop.Width / 2, Canvas.GetTop(JoystickTop) + JoystickTop.Height / 2);
            _centerOfJoystickBase = new(Canvas.GetLeft(JoystickBase) + JoystickBase.Width / 2, Canvas.GetTop(JoystickBase) + JoystickBase.Height / 2);
            _outputJoystickCoordinates = new Point(_centerOfJoystickTop.X - _centerOfJoystickBase.X, _centerOfJoystickTop.Y - _centerOfJoystickBase.Y);
            OutputJoystickCoordinateX = Math.Ceiling(_outputJoystickCoordinates.X);
            OutputJoystickCoordinateY = Math.Ceiling(_outputJoystickCoordinates.Y);
            //System.Diagnostics.Debug.WriteLine(OutputJoystickCoordinateX + ", " + OutputJoystickCoordinateY);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void FirePropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
