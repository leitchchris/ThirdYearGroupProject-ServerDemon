using System;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.XPath;

namespace gpServer {
	public class ACLs {

		int _id;
		string _firstName;
		string _lastName;
		string _picPath;
		string _allow;

		public int Id { get { return _id; } }
		public string FirstName { get { return _firstName; } }
		public string LastName { get { return _lastName; } }
		public string PicPath { get { return _picPath; } }
		public string Allow { get { return _allow; } }


		public ACLs (int id, string firstName, string lastName, string picPath, string allow){
			this._id = id;
			this._firstName = firstName;
			this._lastName = lastName;
			this._picPath = picPath;
			this._allow = allow;
		}

		public ACLs(){
		}

		public static void addUsr(int aclID){
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
				int idCheck = Convert.ToInt32(peopleCheck.InnerXml);
				if (idCheck != aclID){
					people = person.SelectSingleNode ("/People[last()]");
					Console.WriteLine ("ACL: " + person.InnerXml +" : ");
					XmlNode newPerson = acl.CreateElement ("person");
					/*newPerson.InnerXml = "<id>"+Id+"</id>" +
						"<firstName>"+FirstName+"</firstName>" +
							"<lastName>"+LastName+"</lastName>" +
							"<imageLocation>"+PicPath+"</imageLocation>" +
							"<allow>"+Allow+"</allow>";*/
					people.AppendChild (newPerson);
					acl.Save (@"./accessList.xml");
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
			// remove tags and save the file
			
		}

		public static void blockUsr(int aclID){
			string block = "BAN";
			XmlTextReader reader = new XmlTextReader (@"./accessList.xml");
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
			XmlTextReader reader = new XmlTextReader (@"./accessList.xml");
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

