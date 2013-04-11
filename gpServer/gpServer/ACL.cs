using System;
using System.Text;
using System.IO;
using System.Xml;

namespace gpServer {
	public class ACL {

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

		public ACL (int id, string firstName, string lastName, string picPath, bool allow){
			this._id = id;
			this._firstName = firstName;
			this._lastName = lastName;
			this._picPath = picPath;
			this._allow = allow;
		}

		public static string openAcl (){
			string acl = File.ReadAllText(@"/Users/smashinimo/Code/accessList.xml");
			return string.Format(acl);    
		}

		public void addUsr(string acl){

		}

		public void rmUsr(string acl){

		}

		public void blockUsr(string acl){

		}

		public void allowUsr(string acl){

		}



	}
}

