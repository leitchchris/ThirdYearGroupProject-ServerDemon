using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Drawing;

namespace gpServer
{
	public class IDPicRx
	{
		/*
		 * This is only if neaded. It will reseave anything sent from the server if one is spesafied (server using IDworker.cs 
		 */

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

		public IDPicRx(){
		}

		public void PicRx(){
			Console.WriteLine("Receiver is starting...");
			byte[] data = new byte[1024];

			IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 2002);
			//IPAddress ip = IPAddress.Parse ("192.168.1.88");

			Socket newsock = new Socket(AddressFamily.InterNetwork,
			                            SocketType.Stream, ProtocolType.Tcp);
			
			newsock.Bind(ipep);
			newsock.Listen(10);
			Console.WriteLine("Waiting to receive...");
			
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
					bmp.Save("/Users/smashinimo/RxPic.jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
				}
				catch (ArgumentException e){
					Console.WriteLine("{0}", e);
				}
				
				if (data.Length == 0){
					newsock.Listen(10);
				}
				else{
					break;
				}
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

