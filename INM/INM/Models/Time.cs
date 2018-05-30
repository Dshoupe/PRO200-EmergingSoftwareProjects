using System;
using System.Collections.Generic;
using System.Text;

namespace INM.Models
{
    public class Time
    {
								private int seconds;

								public int Seconds
								{
												get { return seconds; }
												set { seconds = value;
																if (seconds >= 60 )
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
												set { minutes = value;
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
												return $"{Hours}:{Minutes}:{Seconds}";
								}
				}
}