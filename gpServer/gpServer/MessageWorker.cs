using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;


namespace gpServer{
	public class MessageWorker{
		static TcpListener listener;
		const int LIMIT = 5; //max 5 clients, this actualy now makes 5 sepret client threds at run.
		public static string clientIP { get; set; }

		public MessageWorker()
		{
			//this deals with all messages sent to the server
		}

		public void Start(){

			DateTime time = DateTime.Now;              // Use current time
			string format = "MMM ddd d HH:mm yyyy";    // Use this format eg May Fri 12 15:16 2013
			Console.WriteLine("Started MessageWorker at - {0}", time.ToString(format));
			//Console.Write (" I think the IP is: {0}",); //deprecated,  gan get the wrong ip addess, check with your settings
			//IPAddress ipAddress = Dns.GetHostEntry("localhost").AddressList[0]; //gets the loopback, comenting out
			try{
				IPAddress ip = IPAddress.Parse(LocalIPAddress());
				Console.WriteLine (" The program thinks the ip: {0}", ip); //this is the corect IP
				
				listener = new TcpListener (ip,2001);
				listener.Start ();
				Console.WriteLine ("\nServer started on 2001");
				for (int i = 0; i <LIMIT; i++) {
					Thread t = new Thread (new ThreadStart (ListnerService));
					t.Start ();
				}
			}catch {
				Console.WriteLine("Faild to get host ip. PLease spesafy local ip: eg 192.168.1.1\n");
				string userIP = Console.ReadLine();
				IPAddress localIp = IPAddress.Parse(userIP);
				listener = new TcpListener (localIp,2001);
				Console.WriteLine (" The program thinks the ip: {0}", localIp); //this is the corect IP
				
				listener = new TcpListener (localIp,2001);
				listener.Start ();
				Console.WriteLine ("\nServer started on 2001");
				for (int i = 0; i <LIMIT; i++) {
					Thread t = new Thread (new ThreadStart (ListnerService));
					t.Start ();
				}
			}
			//IPAddress ip = IPAddress.Parse("192.168.0.1"); //mac

		}
		public static void ListnerService(){
			try {
			Byte[] bytes = new byte[256];
			string data = null;

			while (true){
				Console.WriteLine("Wating for conection");
				TcpClient client = listener.AcceptTcpClient();
				Console.WriteLine("Connected");
				
				data = null;

				NetworkStream stream = client.GetStream();

				int i;

				while((i = stream.Read(bytes, 0, bytes.Length))!=0){
						data = System.Text.Encoding.ASCII.GetString(bytes, 0,i);
						//Console.WriteLine("{0}", data);  //pritns out message to console
						MessageParse(data);
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

		public string GetIP(){
			string strHostName = "";
			strHostName = System.Net.Dns.GetHostName();
			IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(strHostName);
			IPAddress[] addr = ipEntry.AddressList;
			return addr[addr.Length-1].ToString();
		}

		public static void MessageParse(string data){
			//Console.WriteLine (data);
			string input = data;
			Match matchIP = Regex.Match (input, @"D:(\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b)"); //match for an ip sent to the server
			if (matchIP.Success) {
				string[] message = Regex.Split (input, "^D:"); //get only the ip
				foreach (string ip in message) {
					clientIP = ip;
					Console.WriteLine (clientIP);
				}
			}
			string msg = data;
			Match matchEOL = Regex.Match (input, @"$\n"); //match for an ip sent to the server
			if (matchEOL.Success) {
				msg = data.Split ('\n').First ();
			}else{
				msg = data.Split ('\r').First ();
			}

			switch (msg) {
			case "Y":
				Console.WriteLine ("You acknowledge");
				break;
			case "N":
				Console.WriteLine ("You are telling them to go away");
				break;
			case "Null":
				Console.WriteLine ("Users is mashing the keys");
				break;
			case "K:Active":
				//kenect has been activates and nead to colect the aprochers information and send it to the droid
				Messager tellDroidActive = new Messager();
				//tellDroid.Tx //tell droid there is somwon at the door
				HTTPHost.Start();
				Console.Write(msg);
				break;
			case "K:Unknown":
				//unknowen person at the kenect activate http web picture and tell the droid
				Messager tellDroidUnknown = new Messager();
				//tellDroid.Tx //tell droid there is somwon at the door
				HTTPHost.Start();
				Console.Write(msg);
				break;
			case "D:H-Lights-ON":
				//droid has requested the hall light be turned on, send to embedded LightsON
				/*
				 * light commands
				 * greenOn greenOff
				 * pulseBlue pulseRed
				 * lampOn LampOff
				 */
				Messager snap = new Messager ();
				snap.Tx("192.168.0.65", 2000,"greenOn");
				Console.Write(msg);
				break;
			case "D:H-Lights-OFF":
				//droid has requested the hall light be turned of, send to embedded LightsOF
				Console.Write(msg);
				break;
			case "D:B-Lights-ON":
				//droid has requested the bedroom light be turned on, send to embedded LightsON
				Console.Write(msg);
				break;
			case "D:B-Lights-OFF":
				//droid has requested the bedroom light be turned of, send to embedded LightsOF
				Console.Write(msg);
				break;
			case "D:L-Lights-ON":
				//droid has requested the living room light be turned on, send to embedded LightsON
				Console.Write(data);
				break;
			case "D:L-Lights-OFF":
				//droid has requested the living room light be turned of, send to embedded LightsOF
				Console.Write(data);
				break;
			case "D:WC-Lights-ON":
				//droid has requested the bathroom light be turned on, send to embedded LightsON
				Console.Write(msg);
				break;
			case "D:WC-Lights-OFF":
				//droid has requested the bathroom light be turned of, send to embedded LightsOF
				Console.Write(msg);
				break;
			case "D:addUsr":
				//droid has requested to add user to acl
				Console.Write(msg);
				break;
			case "D:rmUsr":
				//droid has requested to remove user
				Console.Write(msg);
				break;
			case "D:blockUsr":
				//droid has requested user be blocked
				Console.Write(msg);
				break; 
			}
		}//end messageParse

	}//end class
}

