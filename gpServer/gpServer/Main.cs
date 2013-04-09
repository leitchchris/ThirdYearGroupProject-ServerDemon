using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using System.Drawing;

namespace gpServer{
	class MainClass{
		static void Main(string[] args){
			//this is to assist in testing all the classes, propbaly dont want this in the final thing when i get round to it. 
			//probably some threding.
			try{
				string opt = args[0];

				switch (opt) {
				case "-Mw":
				case "-MW":
				case "-mw":
				case "-mW":
					Console.WriteLine("Starting MessageWorker");
					MessageWorker listner = new MessageWorker();
					listner.Start();
					break;
				case "-M":
				case "-m":
					//Console.WriteLine("Please enter the port");
					//int port = Console.ReadLine();
					//Console.WriteLine("Please enter the ip");
					//string ip = Console.ReadLine();
					Console.WriteLine("Starting Messager");
					Messager mgr = new Messager();
<<<<<<< HEAD
					mgr.Tx("192.168.1.65",2000);
=======
					Console.WriteLine("Please enter the ip and port as, \t ip:port ");
					string usrInput = Console.ReadLine();
					string ip = usrInput.Split(':').First();
					int port = int.Parse(usrInput.Split(':').Last());
					mgr.Tx(ip, port);
>>>>>>> tiday
					break;
				case "-I":
				case "-i":
					Console.WriteLine("Starting ID Worker");
					IdWorker id = new IdWorker();
					id.PicTx();
					break;
				case "-E":
				case "-e":
					Console.WriteLine("Starting EmbededWorker");
					EmbededWorker snap = new EmbededWorker ();
					snap.SocketSendReceive ("192.168.0.65", 2000);
					break;
				case "-?":
					Console.WriteLine("Hellp!");
					Console.WriteLine("------");
					Console.WriteLine("Start gpServer with thease options.");
					Console.WriteLine("\r");//to insert a blank line
					Console.WriteLine("Start MessageWorker: -Mw");
					Console.WriteLine("Start Messeger: -M");
					Console.WriteLine("Start Id Worker: -I");
					Console.WriteLine("Start EmbededWorker: -E");
					break;
				}
			}
			catch(IndexOutOfRangeException e){ //this is just to catch null option
				Console.WriteLine("Hellp!");
				Console.WriteLine("------");
				Console.WriteLine("Start gpServer with thease options.");
				Console.WriteLine("\r");
				Console.WriteLine("Start MessageWorker: -Mw");
				Console.WriteLine("Start Messeger: -M");
				Console.WriteLine("Start Id Worker: -I");
				Console.WriteLine("Start EmbededWorker: -E");
			}
		}
	}
}