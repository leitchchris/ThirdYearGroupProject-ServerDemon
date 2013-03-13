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
			Console.WriteLine ("Please enter the ip of the server\n");
			string ip = Console.ReadLine ();
			client.SocketSendReceive (ip, 2001);
	
		}

	}




}