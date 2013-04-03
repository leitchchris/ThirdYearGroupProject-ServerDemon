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
			try{
				string opt = args[0];
				
				switch (opt) {
				case "-M":
				case "-m":
					Console.WriteLine("Starting Messager..");
					Messager client = new Messager ();
					Console.WriteLine ("Please enter the ip of the server\n");
					string ip = Console.ReadLine ();
					client.Tx (ip, 2000);
					break;
				case "-I":
				case "-i":
					Console.WriteLine("Starting IDWorker..");
					IdWorker id = new IdWorker();
					id.PicRx();
					break;
				case "-?":
					Console.WriteLine("Hellp!");
					Console.WriteLine("------");
					Console.WriteLine("Start gpClientEmu with thease options.");
					Console.WriteLine("\r");
					Console.WriteLine("Start Messeger: -M");
					Console.WriteLine("Start Id Worker: -I");
					break;
				}
			}
			catch(IndexOutOfRangeException e){ //this is just to catch null option
				Console.WriteLine("Hellp!");
				Console.WriteLine("------");
				Console.WriteLine("Start gpClientEmu with thease options.");
				Console.WriteLine("\r");
				Console.WriteLine("Start Messeger: -M");
				Console.WriteLine("Start Id Worker: -I");
			}


	
		}

	}




}