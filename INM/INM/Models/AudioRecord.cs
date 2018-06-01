using SQLite;
using SQLiteNetExtensions.Attributes;

namespace INM.Models
{
	[Table("AudioRecord")]
	public class AudioRecord
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; }

		[ForeignKey(typeof(User))]
		public int CreatorId { get; }

		[ForeignKey(typeof(Transcript))]
		public int Transcript { get; set; }

		public byte[] AudioClip { get; set; }


		public AudioRecord(int creatorUserId)
		{
			CreatorId = creatorUserId;
		}
	}
}