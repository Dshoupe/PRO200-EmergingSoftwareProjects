using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace innaMinute.Droid.Models
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