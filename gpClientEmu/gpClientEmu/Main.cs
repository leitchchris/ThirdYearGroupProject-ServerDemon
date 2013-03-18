using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

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
			//above is rubbish and neaded replaced

			//call your class hear

			//IDWorker = Kenect

			//MessageWorker = Droid

			//EmbededWorker = Snap Net


	
		}

	}




}