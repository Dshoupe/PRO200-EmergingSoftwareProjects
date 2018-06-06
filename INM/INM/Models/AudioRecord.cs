using SQLite;
using SQLiteNetExtensions.Attributes;

namespace INM.Models
{
	[Table("AudioRecord")]
	public class AudioRecord
	{
        [PrimaryKey, AutoIncrement]
		public int ID { get; set; }

		public int CreatorId { get; set; }

		public string Title { get; set; }

		public byte[] AudioClip { get; set; }

        public AudioRecord() { }
		public AudioRecord(int creatorUserId)
		{
			CreatorId = creatorUserId;
		}
	}
}