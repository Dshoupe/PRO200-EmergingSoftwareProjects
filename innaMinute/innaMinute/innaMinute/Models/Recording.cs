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
	public class Recording
	{
		public byte[] AudioClip { get; set; }

		public int ID { get; set; }

		public Transcript Transcript { get; set; }

		public string Transcribe()
		{
			return null;
		}

		public override string ToString()
		{
			return base.ToString();
		}
	}
}