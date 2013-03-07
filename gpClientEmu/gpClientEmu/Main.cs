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

				NetSender client = new NetSender ();
				while (true) {
					Console.WriteLine ("Message\n");
					string message = Console.ReadLine ();
					client.SocketSendReceive (message, "192.168.43.205", 2000);
					message = null;
					Console.ReadKey ();
			}

		}



	}
}