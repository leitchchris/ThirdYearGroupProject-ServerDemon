using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using NetworkCommsDotNet;

namespace gpServer
{
	public class MessageWorker
	{
		public MessageWorker ()
		{

		}

		public void Start(){
			//Trigger the method PrintIncomingMessage when a packet of type 'Message' is received
			//We expect the incoming object to be a string which we state explicitly by using <string>
			NetworkComms.AppendGlobalIncomingPacketHandler<string>("Message", IncomingMessage);
			//Start listening for incoming connections
			TCPConnection.StartListening(true);
			
			//Print out the IPs and ports we are now listening on
			//Console.WriteLine("Server listening for TCP connection on:");
			//foreach (System.Net.IPEndPoint localEndPoint in TCPConnection.ExistingLocalListenEndPoints()) Console.WriteLine("{0}:{1}", localEndPoint.Address, localEndPoint.Port);
			DateTime time = DateTime.Now;              // Use current time
			string format = "MMM ddd d HH:mm yyyy";    // Use this format
			Console.WriteLine("Started at - {0}", time.ToString(format)); 
			//Let the user close the server
			
			
			
			Console.WriteLine("\nPress any key to close server.");
			Console.ReadKey(true);
			
			//We have used NetworkComms so we should ensure that we correctly call shutdown
			NetworkComms.Shutdown();
		}

		/// <summary>
		/// Writes the provided message to the console window
		/// </summary>
		/// <param name="header">The packetheader associated with the incoming message</param>
		/// <param name="connection">The connection used by the incoming message</param>
		/// <param name="message">The message to be printed to the console</param>
		
		// this mehtoud should take in mesages on the port its listning to and send them to the apropriout class
		
		private static void IncomingMessage (PacketHeader header, Connection connection, string message)
		{
			//Console.WriteLine("\nA message was recieved from " + connection.ToString() + " which said '" + message + "'.");
			Console.WriteLine (message);
			switch (message) {
			case "A":
				Console.WriteLine("YOu are agnolijing");
				break;
			case "K":
				Console.WriteLine("YOu are telling them to bugger of");
				break;
			case "Null":
				Console.WriteLine("Users is mashing the keys");
				break;
			}
			
		}
	}
}

