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
		bool _allow;

		public int Id { get { return _id; } }
		public string FirstName { get { return _firstName; } }
		public string LastName { get { return _lastName; } }
		public string PicPath { get { return _picPath; } }
		public bool Allow { get { return _allow; } }

		/*
		public newACL (int id, string firstName, string lastName, string picPath, bool allow){
			this._id = id;
			this._firstName = firstName;
			this._lastName = lastName;
			this._picPath = picPath;
			this._allow = allow;
		}*/

		public ACLs(){
		}

		public void addUsr(){
		
		}

		public void rmUsr(XmlDocument openAcl){

		}

		public void blockUsr(int aclID){
			string unblock = "TRUE";
			XmlTextReader reader = new XmlTextReader (@"./accessList.xml");
			XmlDocument acl = new XmlDocument();
			acl.Load (reader);
			reader.Close();
			
			XmlNode allow;
			XmlElement top = acl.DocumentElement;
			allow = top.SelectSingleNode("/People/person[id='" + aclID + "']/allow");
			Console.WriteLine ("ACL: " + allow.InnerXml +" : ");
			allow.InnerXml = unblock;
			Console.WriteLine ("ACL: " + allow.InnerXml +" : ");
			acl.Save(@"./accessList.xml");
		}

		public void allowUsr(int aclID){
			String block = "BAN";
			XmlTextReader reader = new XmlTextReader (@"./accessList.xml");
			XmlDocument acl = new XmlDocument();
			acl.Load (reader);
			reader.Close();

			XmlNode allow;
			XmlElement top = acl.DocumentElement;
			allow = top.SelectSingleNode("/People/person[id='" + aclID + "']/allow");
			Console.WriteLine ("ACL: " + allow.InnerXml +" : ");
			allow.InnerXml = block;
			Console.WriteLine ("ACL: " + allow.InnerXml +" : ");
			acl.Save(@"./accessList.xml");
		}



	}
}

