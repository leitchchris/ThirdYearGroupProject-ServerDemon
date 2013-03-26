using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace gpClientEmu
{
	public class IdWorker
	{
		private string _pic; // pic might be a byte array not shure on this one.
		private string _name; // name of the person if we have it
		private string _clientIP;

		public string Picture{
			get {return _pic;}
			set { _pic = value;}
		}

		public string Name {
			get { return _name;}
			set { _name = value;}
		}

		public string clientIP {
			get { return _clientIP;}
			set { _clientIP = value;}
		}

		public IdWorker(){
		}
		
		public void PicTx(){
			IPAddress ip = IPAddress.Parse (clientIP);
			//byte[] data = new byte[1024];
			Socket droid = new Socket(AddressFamily.InterNetwork,SocketType.Stream, ProtocolType.Tcp);
			try{
				droid.Connect(ip,2001);
			}
			catch (SocketException e){
				Console.WriteLine("Unable to connect to server: {0}", e.ToString());
			}
			Picture = "/Users/smashinimo/face.jpg";

			SendVarData(droid, File.ReadAllBytes(Picture));
			
			//Console.WriteLine("Disconnecting from server...");
			droid.Shutdown(SocketShutdown.Both);
			droid.Close();

		}
		private static int SendVarData(Socket s, byte[] data)
		{
			int total = 0;
			int size = data.Length;
			int dataleft = size;
			int sent;
			
			byte[] datasize = new byte[4];
			datasize = BitConverter.GetBytes(size);
			sent = s.Send(datasize);
			
			while (total < size){
				sent = s.Send(data, total, dataleft, SocketFlags.None);
				total += sent;
				dataleft -= sent;
			}
			return total;
		}

		public void PicRx(){
			
		}
	}
}

