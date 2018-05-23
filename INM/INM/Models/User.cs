using System;
using SQLite;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INM.Models
{
    [Table("User")]
	public class User
	{
		public List<User> Contacts { get; set; }
		public string Email { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public List<Group> Groups { get; set; }
        [PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public string PhoneNumber { get; set; }
        [NotNull, MaxLength(20)]
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