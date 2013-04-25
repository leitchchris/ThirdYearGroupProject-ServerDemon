using System;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.XPath;

namespace gpServer {
	public class ACLs {

		private int _id;
		private string _firstName;
		private string _lastName;
		private string _picPath;
		private string _allow;

		public int Id {
			get {return _id;}
			set { _id = value;}
		}
		public string FirstName {
			get {return _firstName;}
			set { _firstName = value;}
		}
		public string LastName {
			get {return _lastName;}
			set { _lastName = value;}
		}
		public string PicPath {
			get {return _picPath;}
			set { _picPath = value;}
		}
		public string Allow {
			get {return _allow;}
			set { _allow = value;}
		}

		public ACLs(){
		}
		
		public void addUsr(int aclID){
			XmlTextReader reader = new XmlTextReader (@"./accessList.xml");
			XmlDocument acl = new XmlDocument();
			acl.Load (reader);
			reader.Close();
			
			XmlNode people;
			XmlElement person = acl.DocumentElement;
			XPathDocument aclCheck = new XPathDocument (@"./accessList.xml");
			XPathNavigator peopleCheck = aclCheck.CreateNavigator();
			try{
				XPathExpression expr = peopleCheck.Compile("/People/person[id]");
				XPathNodeIterator iterator = peopleCheck.Select(expr);
				while (iterator.MoveNext()){
					XPathNavigator idNav = iterator.Current.Clone();
					int idCheck = Convert.ToInt32(idNav);
					if (idCheck != aclID){
						people = person.SelectSingleNode ("/People[last()]");
						Console.WriteLine ("ACL: " + person.InnerXml +" : ");
						XmlNode newPerson = acl.CreateElement ("person");
						newPerson.InnerXml = "<id>"+Id+"</id>" +
							"<firstName>"+FirstName+"</firstName>" +
								"<lastName>"+LastName+"</lastName>" +
								"<imageLocation>"+PicPath+"</imageLocation>" +
								"<allow>"+Allow+"</allow>";
						people.AppendChild (newPerson);
						acl.Save (@"./accessList.xml");
						break;
					}
					Console.WriteLine(@"Person oredy in the ACL");
					break;
				}
			}
			catch{
				Console.WriteLine(@"Person oredy in the ACL");
			}
		}

		public static void rmUsr(int aclID){
			XmlTextReader reader = new XmlTextReader (@"./accessList.xml");
			XmlDocument acl = new XmlDocument();
			acl.Load (reader);
			reader.Close();
			
			XmlNode allow;
			XmlElement people = acl.DocumentElement;
			allow = people.SelectSingleNode("/People/person[id='" + aclID + "']");
			Console.WriteLine ("ACL: " + allow.InnerXml +" : ");
			people.RemoveChild (allow);
			acl.Save (@"./accessList.xml");
		}

		public static void blockUsr(int aclID){
			string block = "BAN";
			XmlTextReader reader = new XmlTextReader (@"/var/www/accessList.xml");
			XmlDocument acl = new XmlDocument();
			acl.Load (reader);
			reader.Close();
			
			XmlNode allow;
			XmlElement people = acl.DocumentElement;
			allow = people.SelectSingleNode("/People/person[id='" + aclID + "']/allow");
			Console.WriteLine ("ACL: " + allow.InnerXml +" : ");
			allow.InnerXml = block;
			Console.WriteLine ("ACL: " + allow.InnerXml +" : ");
			acl.Save(@"./accessList.xml");
		}

		public static void allowUsr(int aclID){
			string unblock = "TRUE";
			XmlTextReader reader = new XmlTextReader (@"/var/www/accessList.xml");
			XmlDocument acl = new XmlDocument();
			acl.Load (reader);
			reader.Close();

			XmlNode allow;
			XmlElement people = acl.DocumentElement;
			allow = people.SelectSingleNode("/People/person[id='" + aclID + "']/allow");
			Console.WriteLine ("ACL: " + allow.InnerXml +" : ");
			allow.InnerXml = unblock;
			Console.WriteLine ("ACL: " + allow.InnerXml +" : ");
			acl.Save(@"./accessList.xml");
		}



	}
}

