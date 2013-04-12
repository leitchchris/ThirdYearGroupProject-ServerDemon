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
			XmlDocument acl = new XmlDocument();
			acl.Load (@"./accessList.xml");
			XPathNavigator list = acl.CreateNavigator();
			// Compile a standard XPath expression
			XPathExpression xpath; 
			xpath = list.Compile("/People/person[ID='"+aclID+"']/allow");
			XPathNodeIterator iterator = list.Select(xpath);
			
			// Iterate on the node set
			try
			{
				while (iterator.MoveNext())
				{
					XPathNavigator nav = iterator.Current.Clone();
					Console.WriteLine("price: " + nav.Value);
				}
			}
			catch(Exception ex) 
			{
				Console.WriteLine(ex.Message);
			}
		}

		public void allowUsr(int aclID){

			XmlTextReader reader = new XmlTextReader (@"./accessList.xml");
			XmlDocument acl = new XmlDocument();
			acl.Load (reader);
			reader.Close();
			XPathNavigator list = acl.CreateNavigator();
			// Compile a standard XPath expression
			XPathExpression xpath; 
			xpath = list.Compile("/People/person[id='" + aclID + "']");
			XPathNodeIterator iterator = list.Select(xpath);
			
			// Iterate on the node set
			try
			{
				while (iterator.MoveNext())
				{
					XPathNavigator nav = iterator.Current.Clone();
					Console.WriteLine("price: " + nav.Value);
				}
			}
			catch(Exception ex) 
			{
				Console.WriteLine(ex.Message);
			}
		}



	}
}

