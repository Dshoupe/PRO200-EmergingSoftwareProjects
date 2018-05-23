using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INM.Models
{
	public class User
	{
		public List<User> Contacts { get; set; }
		public string Email { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public List<Group> Groups { get; set; }
		public int ID { get; set; }
		public string PhoneNumber { get; set; }
		public string Username { get; set; }
		public List<Recording> Recordings { get; set; }

		public User()
		{
			Contacts = new List<User>();
			Groups = new List<Group>();
			Recordings = new List<Recording>();
		}
		public void UpdateUser()
		{

		}

		public override string ToString()
		{
			return $"{FirstName} {LastName} - {Username} - {Email} - {PhoneNumber}";
		}
	}
}