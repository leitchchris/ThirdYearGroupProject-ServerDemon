using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Drawing;

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
			Console.WriteLine("Server is starting...");
			byte[] data = new byte[1024];
			IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 9050);
			
			Socket newsock = new Socket(AddressFamily.InterNetwork,
			                            SocketType.Stream, ProtocolType.Tcp);
			
			newsock.Bind(ipep);
			newsock.Listen(10);
			Console.WriteLine("Waiting for a client...");
			
			Socket client = newsock.Accept();
			IPEndPoint newclient = (IPEndPoint)client.RemoteEndPoint;
			Console.WriteLine("Connected with {0} at port {1}",
			                  newclient.Address, newclient.Port);
			
			while (true){
				data = ReceiveVarData(client);
				MemoryStream ms = new MemoryStream(data);
				try{
					Image bmp = Image.FromStream(ms);
					//save the picture
				}
				catch (ArgumentException e){
					Console.WriteLine("something broke");
				}
				
				if (data.Length == 0)
					newsock.Listen(10);
			}
			//Console.WriteLine("Disconnected from {0}", newclient.Address);
			client.Close();
			newsock.Close();
		}
			
		private static byte[] ReceiveVarData(Socket s){
			int total = 0;
			int recv;
			byte[] datasize = new byte[4];
			
			recv = s.Receive(datasize, 0, 4, 0);
			int size = BitConverter.ToInt32(datasize, 0);
			int dataleft = size;
			byte[] data = new byte[size];
			
			while (total < size){
				recv = s.Receive(data, total, dataleft, 0);
				if (recv == 0){
					break;
				}
				total += recv;
				dataleft -= recv;
			}
			return data;
		}
	}
}

