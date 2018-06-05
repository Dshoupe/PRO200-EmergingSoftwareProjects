using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace INM.Models
{
	public class Time
	{

		public void Reset()
		{
			Seconds = 0;
			Minutes = 0;
			Hours = 0;
		}

		private int seconds;

		public int Seconds
		{
			get { return seconds; }
			set
			{
				seconds = value;
				if (seconds >= 60)
				{
					Minutes++;
					seconds -= 60;
				}
			}
		}

		private int minutes;

		public int Minutes
		{
			get { return minutes; }
			set
			{
				minutes = value;
				if (minutes >= 60)
				{
					Hours++;
					minutes -= 60;
				}
			}
		}

		public int Hours { get; set; } = 0;

		public override string ToString()
		{
			if (seconds == 0 && minutes == 0 && Hours == 0)
			{
				return "0:0:0";
			}
			else
			{
				return $"{Hours}:{Minutes}:{Seconds}";
			}
		}
	}
}