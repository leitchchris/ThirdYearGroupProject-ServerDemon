using System;
using System.Collections.Generic;
using System.Text;
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
				case "-A":
				case "-a":
					ACLs list =  new ACLs();
					list.addUsr();
					break;
				case "-H":
				case "-h":
					Console.WriteLine("Starting httpd..");
					HTTPHost.main();
					break;
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
					//Console.Write("Please enter the ip");
					//string ip = Console.ReadLine();
					mgr.Tx("129.215.232.2",2001);
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
				Console.WriteLine("Start HTTPd: -H");
				Console.WriteLine("Start MessageWorker: -Mw");
				Console.WriteLine("Start Messeger: -M");
				Console.WriteLine("Start Id Worker: -I");
				Console.WriteLine("Start EmbededWorker: -E");
			}
		}
	}
}