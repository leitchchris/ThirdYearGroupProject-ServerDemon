using System;
using System.Text;
using System.IO;
using System.Xml;

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
		public ACL (int id, string firstName, string lastName, string picPath, bool allow){
			this._id = id;
			this._firstName = firstName;
			this._lastName = lastName;
			this._picPath = picPath;
			this._allow = allow;
		}*/

		public ACLs(){
		}

		public void addUsr(){
			XmlDocument acl = new XmlDocument();
			acl.Load (@"/Users/smashinimo/accessList.xml");
			XmlNodeList node = acl.SelectNodes ("//People");
			Console.WriteLine(node);
			/*
			using (XmlReader reader = XmlReader.Create(acl)) {
				while (reader.Read()){
					if (reader.IsStartElement()){
						switch (reader.Name){
							case "person":
							Console.WriteLine(reader.Name);
							break;
						}
					}
				}
			}*/
		}

		public void rmUsr(XmlDocument openAcl){

		}

		public void blockUsr(XmlDocument openAcl){

		}

		public void allowUsr(XmlDocument openAcl){

		}



	}
}

