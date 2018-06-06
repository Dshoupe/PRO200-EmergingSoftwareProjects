using SQLite;
using SQLiteNetExtensions.Attributes;

namespace INM.Models
{
	[Table("GroupAudioRecord")]
    public class GroupAudioRecord
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get;}
        public int AudioRecordId { get; set; }

		public int GroupId { get; set; }

		public bool IsGroupAudioCreator { get; set; }
	}
}
