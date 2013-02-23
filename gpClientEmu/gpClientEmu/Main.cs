using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkCommsDotNet;

namespace gpClientEmu
{
	class MainClass
	{

		static void Main(string[] args)
		{
			//Request server IP and port number
			//Console.WriteLine("Please enter the server IP and port in the format 192.168.0.1:10000 and press return:");
			//string serverInfo = Console.ReadLine();
			
			//Parse the necessary information out of the provided string
			string serverIP = "192.168.1.88"; //serverInfo.Split(':').First();
			int serverPort = 10000; //int.Parse(serverInfo.Split(':').Last());
			
			//Keep a loopcounter
			int loopCounter = 1;
			while (true)
			{

				//Write some information to the console window This will be handedld by the android using a button press
				Console.Write("Please select A to allow or K to deniy\n");
				string messageToSend = "";
				var input = Console.ReadKey();
				if (input.Key == ConsoleKey.A || input.Key == ConsoleKey.K){
					switch (input.Key){
						case ConsoleKey.A:
							messageToSend = "A";//"This is message #" + loopCounter;
							break;
						case ConsoleKey.K:
							messageToSend = "K";
							break;
					}
				}
				else{
					messageToSend = "Null";
				}
				Console.WriteLine("\nSending message to server saying {0}", messageToSend );


				
				//Send the message in a single line
				NetworkComms.SendObject("Message", serverIP, serverPort, messageToSend);
				
				//Check if user wants to go around the loop
				Console.WriteLine("\nPress q to quit or any other key to send another message.");
				if (Console.ReadKey(true).Key == ConsoleKey.Q) break;
				else loopCounter++;
			}
			
			//We have used comms so we make sure to call shutdown
			NetworkComms.Shutdown();
		}
	}
}