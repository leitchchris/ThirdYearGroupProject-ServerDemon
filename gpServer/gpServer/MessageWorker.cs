using System;
using System.Threading;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Drawing;


namespace gpServer
{
	public class MessageWorker
	{
		static TcpListener listener;
		const int LIMIT = 5; //max 5 clients
		public MessageWorker ()
		{

		}

		public void Start(){

			DateTime time = DateTime.Now;              // Use current time
			string format = "MMM ddd d HH:mm yyyy";    // Use this format
			Console.WriteLine("Started MessageWorker at - {0}", time.ToString(format));
			Console.Write ("IP: {0}",LocalIPAddress ());

			listener = new TcpListener (2000);
			listener.Start ();
			Console.WriteLine ("Server started on 2000");
			for (int i = 0; i <LIMIT; i++) {
				Thread t = new Thread (new ThreadStart (ListnerService));
				t.Start ();
			}
		}
		public void ListnerService(){
			while (true){
				Socket soc = listener.AcceptSocket();
				Console.WriteLine("{0} :Connected", soc.RemoteEndPoint);
				try{
					Stream s = new NetworkStream(soc); 
					StreamReader sr = new StreamReader(s);
					while (true){
						string msg = sr.ReadLine();
						Console.WriteLine(msg);
					}
					s.Close();
				}catch(Exception e){
					Console.WriteLine(e.Message);
				}
				Console.WriteLine("Disconnected: {0}", soc.RemoteEndPoint);
				soc.Close();
			}
		}

		public string LocalIPAddress()
		{
			IPHostEntry host;
			string localIP = "";
			host = Dns.GetHostEntry(Dns.GetHostName());
			foreach (IPAddress ip in host.AddressList)
			{
				if (ip.AddressFamily == AddressFamily.InterNetwork)
				{
					localIP = ip.ToString();
				}
			}
			return localIP;
		}
	}
}

