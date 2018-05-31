using SQLite;
using SQLiteNetExtensions.Attributes;

namespace INM.Models
{
	[Table("UserUser")]
	public class UserUser
	{
		[ForeignKey(typeof(User))]
		public int PrimaryUserId { get; set; }

		[ForeignKey(typeof(User))]
		public int ContactUserId { get; set; }
	}
}
