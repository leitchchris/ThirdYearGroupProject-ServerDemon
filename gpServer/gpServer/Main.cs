using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using NetworkCommsDotNet;

namespace gpServer
{
	class MainClass
	{
		static void Main(string[] args)
		{

			MessageWorker listner = new MessageWorker();
			listner.Start();




		}
		

	}
}