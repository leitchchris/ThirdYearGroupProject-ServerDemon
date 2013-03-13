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

		public  void SocketSendReceive(string server, int port) 
		{
			TcpClient client = new TcpClient (server, port);
			try{
				Stream s = client.GetStream();
				//StreamReader sr = new StreamReader(s);
				StreamWriter sw = new StreamWriter(s);
				sw.AutoFlush = true;
				while(true){
					Console.Write("Type some stuff\n");
					string msg = Console.ReadLine();
					sw.WriteLine(msg+"\r");
					if (msg == ""){
						Console.Write("Closing");
						break;
					}
				}
				s.Close ();
			}finally{
				client.Close();
			}
		}

	}
}

