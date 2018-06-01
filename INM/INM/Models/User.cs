using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace INM.Models
{
	[Table("User")]
	public class User
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }

		public string Username { get; set; }

		public string Email { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string PhoneNumber { get; set; }
		
		public string Password { get; set; }

		[ManyToMany(typeof(UserUser), CascadeOperations = CascadeOperation.All)]
		public List<User> Contacts { get; set; } = new List<User>();

		[OneToMany(CascadeOperations = CascadeOperation.All)]
		public List<Group> Groups { get; set; } = new List<Group>();

		[OneToMany(CascadeOperations = CascadeOperation.All)]
		public List<AudioRecord> Recordings { get; set; } = new List<AudioRecord>();

		public override string ToString()
		{
			return $"ID={ID}\nUser name={Username}\nEmail={Email}\n" +
				$"Full name={FirstName} {LastName}\nPhone={PhoneNumber}";
		}
	}
}
