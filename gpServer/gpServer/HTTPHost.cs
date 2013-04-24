using System;
using System.Threading;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;

namespace gpServer
{
	public class HTTPHost
	{
		public HTTPHost()
		{


		}
		public static void Start(){
			
			httpd ws = new httpd(SendResponse, "http://localhost:8080/");
			httpd ws2 = new httpd(testPage, "http://localhost:8080/test/");
			httpd ws3 = new httpd(xml, "http://localhost:8080/xml/"); //when sending replace localhost with server ip
			httpd ws4 = new httpd(pic, "http://localhost:8080/pic/");
			ws.Run();
			ws2.Run();
			ws3.Run();
			ws4.Run();
			Console.WriteLine("A simple webserver. Press a key to quit.");
			Console.ReadKey();
			ws.Stop();
		}
		
		public static string SendResponse(HttpListenerRequest request)
		{
			return string.Format("<HTML><BODY>Home Automation<br>{0}</BODY></HTML>", DateTime.Now);    
		}
		public static string testPage(HttpListenerRequest request)
		{
			return string.Format("<HTML><BODY>IT WORKS<br>{0}</BODY></HTML>", DateTime.Now);    
		}
		public static string xml(HttpListenerRequest request)
		{
			string acl = File.ReadAllText(@"./accessList.xml");
			return string.Format(acl);    
		}
		public static string pic(HttpListenerRequest request)
		{
			Image capturedPic = Image.FromFile (@"./foo.png");
			return string.Format("<HTML><BODY><img border=\"0\" src=\"./{0}\"></BODY></HTML>",capturedPic);
		}
	}
}

