using SQLite;
using SQLiteNetExtensions.Attributes;

namespace INM.Models
{
	[Table("AudioRecord")]
	public class AudioRecord
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }

		[ForeignKey(typeof(User))]
		public int CreatorId { get; set; }

		[ForeignKey(typeof(Transcript))]
		public int Transcript { get; set; }

		public byte[] AudioClip { get; set; }

	}
}