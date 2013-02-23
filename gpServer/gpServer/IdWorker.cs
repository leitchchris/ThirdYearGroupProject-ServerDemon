using System;

namespace gpServer
{
	public class IdWorker
	{
		private byte[] _pic; // pic might be a byte array not shure on this one.
		private string _name; // name of the person if we have it

		public byte[] Picture{
			get {return _pic;}
			set { _pic = value;}
		}

		public string Name {
			get { return _name;}
			set { _name = value;}
		}




		public IdWorker ()
		{
			// we nead to send the result of the kennects detection to the droid and listen for a responce if they are to be alowed.
			//



		}
	}
}

