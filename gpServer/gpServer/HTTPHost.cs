using System;
using System.Net;
using System.Threading;
using System.Linq;
using System.Text;
using System.IO;

namespace gpServer
{
	public class HTTPHost
	{
		public HTTPHost()
		{


		}
		public static void main(){
			
			httpd ws = new httpd(SendResponse, "http://localhost:8080/");
			httpd ws2 = new httpd(testPage, "http://localhost:8080/test/");
			httpd ws3 = new httpd(xml, "http://localhost:8080/xml/");
			ws.Run();
			ws2.Run();
			ws3.Run();
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
			return string.Format("<HTML><BODY><br>{0}</BODY></HTML>", DateTime.Now);    
		}
		public static string xml(HttpListenerRequest request)
		{
			string acl = File.ReadAllText(@"./accessList.xml");
			return string.Format(acl);    
		}
	}
}

