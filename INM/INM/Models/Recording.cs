﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INM.Models
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