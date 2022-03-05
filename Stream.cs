using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;
using Emgu.CV;

namespace JoshsTestApp
{
	class Stream
	{
		public Stream()
		{
		}
		
		public void GetCameraStream()
		{
			int dispW = 1920;
			int dispH = 1080;
			int flip = 2;
			int totalFrames;
			int fps;
			bool ret;
			Mat m = new Mat();


			// Command Line for Jetson Nano to start stream
			// gst-launch-1.0 -e nvarguscamerasrc ! 'video/x-raw(memory:NVMM), width=1920, height=1080, format=NV12, framerate=30/1' ! omxh265enc control-rate=2 bitrate=8000000  ! video/x-h265, stream-format=byte-stream ! rtph265pay mtu=1400 ! udpsink host=$CLIENT_IP port=5000 sync=false async=false
			string camSet = "gst-launch-1.0 udpsrc port=5000 ! application/x-rtp,encoding-name=H265,payload=96 ! rtph265depay ! h265parse ! queue ! avdec_h265 ! xvimagesink sync=false async=false -e";
			VideoCapture vc = new VideoCapture(camSet);	
			//cam = vc(camSet);
 
			//Or, if you have a WEB cam, uncomment the next line
			//(If it does not work, try setting to '1' instead of '0')
			//cam=cv2.VideoCapture(0)
			while(true)
			{
				totalFrames = Convert.ToInt32(vc.Get(Emgu.CV.CvEnum.CapProp.FrameCount));
				fps = Convert.ToInt32(vc.Get(Emgu.CV.CvEnum.CapProp.Fps));
				ret = vc.Read(m);

				CvInvoke.Imshow("NanoCam", m);
				//cv2.imshow('nanoCam', frame)

				if (CvInvoke.WaitKey(1) == char.ConvertToUtf32("q", 0))
					break;
						//if cv2.waitKey(1) == ord('q'):
					//break
			}

			vc.Dispose();
			CvInvoke.DestroyAllWindows();
			//cam.release()
			//cv2.destroyAllWindows()
		}
	}
}
