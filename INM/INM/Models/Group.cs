using SQLite;
using SQLiteNetExtensions.Attributes;

namespace INM.Models
{
	[Table("Group")]
	public class Group
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }

		[ForeignKey(typeof(User))]
		public int LeadUserId { get; set; }

		public string GroupName { get; set; }

        public Group() { }

        public Group(int leadUserId, string groupName)
		{
			LeadUserId = leadUserId;
			GroupName = groupName;
		}
	}
}