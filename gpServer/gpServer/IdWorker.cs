using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace gpServer
{
	public class IdWorker
	{
		private byte _pic; // pic might be a byte array not shure on this one.
		private string _name; // name of the person if we have it
		private string _clientIP;

		public byte Picture{
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
			string ip = "192.168.1.107";
			int port = 2002;
			Socket droidSoc = new Socket(AddressFamily.InterNetwork,SocketType.Stream, ProtocolType.Tcp);
			IPAddress ipAdder = IPAddress.Parse(ip);
			IPEndPoint droid = new IPEndPoint(ipAdder, port);
			//byte[] data = new byte[1024];
			
			try{
				droidSoc.Connect(droid);
				Picture = "/Users/smashinimo/face.jpeg";
			}
			catch(Exception ex){
				if ( ex is SocketException ||
				    ex is FileNotFoundException){
					Console.WriteLine("{0}",ex);
				}else{
					throw;
				}
			}
			SendVarData(droidSoc, File.ReadAllBytes(Picture));
			
			//Console.WriteLine("Disconnecting from server...");
			droidSoc.Shutdown(SocketShutdown.Both);
			droidSoc.Close();

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

		public void idDirWatch(){

		}
	}
}

