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
		const int LIMIT = 5; //max 5 clients, this actualy now makes 5 sepret client threds at run.
		public MessageWorker ()
		{

		}

		public void Start(){

			DateTime time = DateTime.Now;              // Use current time
			string format = "MMM ddd d HH:mm yyyy";    // Use this format
			Console.WriteLine("Started MessageWorker at - {0}", time.ToString(format));
			Console.Write (" I think the IP is: {0}",LocalIPAddress ()); //deprecated,  gan get the wrong ip addess, check with your settings
			//IPAddress ipAddress = Dns.GetHostEntry("localhost").AddressList[0]; //gets the loopback, comenting out
			IPAddress ip = IPAddress.Parse(LocalIPAddress ());
			Console.WriteLine (" The program thinks the ip: {0}", ip);

			listener = new TcpListener (ip,2001);
			listener.Start ();
			Console.WriteLine ("\nServer started on 2001");
			for (int i = 0; i <LIMIT; i++) {
				Thread t = new Thread (new ThreadStart (ListnerService));
				t.Start ();
			}
		}
		public static void ListnerService(){
			try {
			Byte[] bytes = new byte[256];
			string data = null;

			while (true){
				/*Socket soc = listener.AcceptSocket();
				Console.WriteLine("{0} :Connected", soc.RemoteEndPoint);
				try{
					Stream s = new NetworkStream(soc); 
					StreamReader sr = new StreamReader(s);
					while (true){
						string msg = sr.ReadLine();
						Console.WriteLine(msg);

					}
					s.Close();*/

				Console.WriteLine("Wating for conection");
				TcpClient client = listener.AcceptTcpClient();
				Console.WriteLine("Connected");
				data = null;

				NetworkStream stream = client.GetStream();

				int i;

					while((i = stream.Read(bytes, 0, bytes.Length))!=0){
					data = System.Text.Encoding.ASCII.GetString(bytes, 0,i);
					Console.WriteLine("{0}", data);

				}
				client.Close();
			}
			}catch(SocketException e){
				Console.WriteLine("SocketException: {0}", e);
			}finally{
				listener.Stop();
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

