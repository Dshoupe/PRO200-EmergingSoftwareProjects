using SQLite;
using SQLiteNetExtensions.Attributes;

namespace INM.Models
{
	[Table("UserUser")]
	public class UserUser
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; }

		[ForeignKey(typeof(User))]
		public int PrimaryUserId { get; set; }

		[ForeignKey(typeof(User))]
		public int ContactUserId { get; set; }

		public UserUser() { }
		public UserUser(int user1Id, int user2Id)
		{
			PrimaryUserId = user1Id < user2Id ? user1Id : user2Id;
			ContactUserId = user2Id > user1Id ? user2Id : user1Id;
		}
	}
}
