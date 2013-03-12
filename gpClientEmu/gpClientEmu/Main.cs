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
			client.SocketSendReceive ("172.20.108.36", 2000);
	
		}

	}




}