using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INM.Models
{
	public class Group
	{
		public string GroupName { get; set; }

		public List<Recording> GroupRecordings { get; set; }

		public int ID { get; set; }


		public void AddGroupMember(int contactId)
		{

		}

		public void RemoveGroupMember(int id)
		{

		}

	}
}