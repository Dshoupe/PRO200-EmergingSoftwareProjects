using SQLite;
using SQLiteNetExtensions.Attributes;

namespace INM.Models
{
	[Table("Transcript")]
	public class Transcript
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }

		public string Script { get; set; }
	}
}