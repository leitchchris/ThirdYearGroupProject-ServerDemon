using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using NetworkCommsDotNet;

namespace gpClientEmu
{
	class MainClass
	{

		static void Main(string[] args)
		{ 
			Messenger client = new Messenger ();
			client.Send();

		}



	}
}