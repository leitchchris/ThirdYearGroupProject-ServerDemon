using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;


namespace gpServer
{
	class MainClass
	{
		static void Main(string[] args)
		{
			/*
			MessageWorker listner = new MessageWorker();
			listner.Start();
			/*EmbededWorker snap = new EmbededWorker ();
			snap.SocketSendReceive ("192.168.0.65", 2000);*/
			IdWorker person = new IdWorker();
			person.PicTx ();



		}
		

	}
}