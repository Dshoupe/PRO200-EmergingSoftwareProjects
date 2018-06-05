using SQLite;
using SQLiteNetExtensions.Attributes;

namespace INM.Models
{
	[Table("AudioRecord")]
	public class AudioRecord
	{
		public int ID { get; set; }

		[ForeignKey(typeof(User))]
		public int CreatorId { get; }

		[ForeignKey(typeof(Transcript))]
		public int Transcript { get; set; }

		public string Title { get; set; }

		public byte[] AudioClip { get; set; }

        public AudioRecord() { }
		public AudioRecord(int creatorUserId)
		{
			CreatorId = creatorUserId;
		}
	}
}