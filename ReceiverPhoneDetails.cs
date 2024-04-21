using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGifting
{
	public class ReceiverPhoneDetails
	{
		private string _manufacturer;

		public string Manufacturer
		{
			get { return _manufacturer; }
			set { _manufacturer = value; }
		}

		private string _deviceName;

		public string DeviceName
		{
			get { return _deviceName; }
			set { _deviceName = value; }
		}
	}
}
