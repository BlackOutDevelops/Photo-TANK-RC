using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoshsTestApp
{
	// PROBABLY WILL NEVER NEED
	class JetsonConnectionStatusViewModel : BaseViewModel
	{
  //      public bool JetsonIsConnected = false;
		//public Stream StreamService = new Stream();

		//private string _jetsonLoadingStatus;
  //      public string JetsonLoadingStatus
  //      {
  //          get { return _jetsonLoadingStatus; }
  //          set
  //          {
  //              if (value != _jetsonLoadingStatus)
  //              {
  //                  _jetsonLoadingStatus = value;
  //                  FirePropertyChanged(nameof(JetsonLoadingStatus));
  //              }
  //          }
  //      }
  //      public JetsonConnectionStatusViewModel()
		//{
		//}

  //      public void JetsonLoading()
  //      {
  //          int count = 0;
  //          while (!JetsonIsConnected)
  //          {
  //              if (count == 0)
		//		{
  //                  JetsonLoadingStatus = "Connecting to Jetson Nano";
  //                  count++;
  //              }
  //              else if (count == 1)
		//		{
  //                  JetsonLoadingStatus = "Connecting to Jetson Nano.";
  //                  count++;
  //              }
  //              else if (count == 2)
		//		{
  //                  JetsonLoadingStatus = "Connecting to Jetson Nano..";
  //                  count++;
  //              }
  //              else
  //              {
  //                  JetsonLoadingStatus = "Connecting to Jetson Nano...";
  //                  count = 0;
  //              }
  //              System.Threading.Thread.Sleep(500);
  //          }
  //      }

  //      public bool GetStream()
		//{
  //          try
		//	{
  //              Task.Run(() => JetsonLoading());
		//	}
		//	catch (Exception ex)
		//	{
  //              System.Diagnostics.Debug.WriteLine(ex.Message);
  //              JetsonIsConnected = false;
		//	}
  //          JetsonIsConnected = true;
  //          return JetsonIsConnected;
  //      }
    }
}
