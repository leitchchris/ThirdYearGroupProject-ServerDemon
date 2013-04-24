using System;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace gpServer
{
	public class Messager
	{
		public string host { get; set;}
		public int port { get; set; }

		public Messager ()
		{
		}

		public void Tx(string ip, int port, string msg){
			try{
				Socket droidSoc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				IPAddress ipAdder = IPAddress.Parse(ip);
				IPEndPoint droid = new IPEndPoint(ipAdder, port);
				droidSoc.Connect(droid);

				//while(true){

					//Rx(snapSoc);

					byte[] socketData = Encoding.ASCII.GetBytes("\r\n"+msg+"\r\n");
					droidSoc.Send(socketData);
					socketData = null;

					byte[] buffer = new byte[1024];
					int iRx = droidSoc.Receive(buffer);
					char[] chars = new char[iRx];
					
					Decoder d = Encoding.ASCII.GetDecoder();
					int charLen = d.GetChars(buffer, 0, iRx, chars, 0);
					String commandRecv = new String(chars);
					Console.WriteLine("Rx:"+commandRecv);
					//Rx(snapSoc);
					if (msg == ""){
						Console.Write("Closing");
					}
				//}
				droidSoc.Close ();
			}finally{
				//client.Close();
			}
		}
		public void Tx(string ip, int port){
			try{
				Socket snapSoc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				IPAddress ipAdder = IPAddress.Parse(ip);
				IPEndPoint snap = new IPEndPoint(ipAdder, port);
				snapSoc.Connect(snap);
				
				while(true){
					
					//Rx(snapSoc);
					
					Console.Write("Type some stuff\n");
					string command = Console.ReadLine();
					byte[] socketData = Encoding.ASCII.GetBytes("\r\n"+command+"\r\n");
					snapSoc.Send(socketData);
					socketData = null;
					
					byte[] buffer = new byte[1024];
					int iRx = snapSoc.Receive(buffer);
					char[] chars = new char[iRx];
					
					Decoder d = Encoding.ASCII.GetDecoder();
					int charLen = d.GetChars(buffer, 0, iRx, chars, 0);
					String commandRecv = new String(chars);
					Console.WriteLine("Rx:"+commandRecv);
					//Rx(snapSoc);
					if (command == ""){
						Console.Write("Closing");
						break;
					}
				}
				snapSoc.Close ();
			}finally{
				//client.Close();
			}
		}

		public void Rx (Socket soc){
			byte[] buffer = new byte[1024];
			int iRx = soc.Receive(buffer);
			char[] chars = new char[iRx];
			
			Decoder d = Encoding.ASCII.GetDecoder();
			int charLen = d.GetChars(buffer, 0, iRx, chars, 0);
			String commandRecv = new String(chars);
			Console.WriteLine ("Rx:"+commandRecv+" "+charLen+"");
		}

	}
}

