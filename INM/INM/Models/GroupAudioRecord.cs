using SQLite;
using SQLiteNetExtensions.Attributes;

namespace INM.Models
{
	[Table("GroupAudioRecord")]
    public class GroupAudioRecord
    {
		[ForeignKey(typeof(AudioRecord))]
		public int AudioRecordId { get; set; }

		[ForeignKey(typeof(Group))]
		public int GroupId { get; set; }

		public bool IsGroupAudioCreator { get; set; }
	}
}
