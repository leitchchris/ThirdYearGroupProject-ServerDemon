using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using NetworkCommsDotNet;


namespace gpClientEmu
{
	public class SecResever
	{
		public SecResever ()
		{
		}


		public void SecListner(){
			NetworkComms.AppendGlobalIncomingPacketHandler<byte[]>("Picture", IncomingMessage);
			TCPConnection.StartListening(true);

			Console.WriteLine ("Started to listen for gpServer to send the picture");

			NetworkComms.Shutdown();

		}

		private static void IncomingMessage (PacketHeader header, Connection connection, byte[] picture)
		{	
			Console.WriteLine("Received picture");
			File.WriteAllBytes("/Users/smashinimo/testing.jpg", picture);
		}

	}
}

