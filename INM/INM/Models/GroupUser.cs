using SQLite;
using SQLiteNetExtensions.Attributes;

namespace INM.Models
{
	[Table("GroupUser")]
    public class GroupUser
    {
		[ForeignKey(typeof(User))]
		public int UserId { get; set; }

		[ForeignKey(typeof(Group))]
		public int GroupId { get; set; }

		public bool IsUserGroupLeader { get; set; }
	}
}
