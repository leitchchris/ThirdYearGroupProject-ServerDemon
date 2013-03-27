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

			Messager client = new Messager ();
			Console.WriteLine ("Please enter the ip of the server\n");
			string ip = Console.ReadLine ();
			client.Tx (ip, 2000);
			//above is rubbish and neaded replaced

			//call your class hear

			//IDWorker = Kenect
			IdWorker person = new IdWorker ();
			person.PicRx ();
			//MessageWorker = Droid

			//EmbededWorker = Snap Net


	
		}

	}




}