using System;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace gpClientEmu
{
	public class NetSender
	{
		public string host { get; set;}
		public int port { get; set; }



		public NetSender ()
		{
		}

		public  void SocketSendReceive(string request, string server, int port) 
		{
			string socRequest = request ;
			Byte[] bytesSent = Encoding.ASCII.GetBytes(request);
			Byte[] bytesReceived = new Byte[256];
			
			// Create a socket connection with the specified server and port.
			Socket s = ConnectSocket(server, port);
			
			if (s == null)
				Console.Write("Connection failed");
			
			// Send request to the server.
			s.Send(bytesSent, bytesSent.Length, 0);  

			/*
			// Receive the server home page content. 
			int bytes = 0;
			string page = "Default HTML page on " + server + ":\r\n";
			
			// The following will block until te page is transmitted. 
			do {
				bytes = s.Receive(bytesReceived, bytesReceived.Length, 0);
				page = page + Encoding.ASCII.GetString(bytesReceived, 0, bytes);
			}
			while (bytes > 0);
			
			return page; */
		}



		private  Socket ConnectSocket(string server, int port)
		{
			Socket s = null;
			IPHostEntry hostEntry = null;
			
			// Get host related information.
			hostEntry = Dns.GetHostEntry(server);
			
			// Loop through the AddressList to obtain the supported AddressFamily. This is to avoid 
			// an exception that occurs when the host IP Address is not compatible with the address family 
			// (typical in the IPv6 case). 
			foreach(IPAddress address in hostEntry.AddressList)
			{
				IPEndPoint ipe = new IPEndPoint(address, port);
				Socket tempSocket = 
					new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
				
				tempSocket.Connect(ipe);
				
				if(tempSocket.Connected)
				{
					s = tempSocket;
					break;
				}
				else
				{
					continue;
				}
			}
			return s;
		}



	}
}

