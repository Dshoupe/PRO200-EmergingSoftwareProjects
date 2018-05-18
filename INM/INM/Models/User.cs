﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace innaMinute.Droid.Models
{
				public class User
				{
								public List<User> Contacts { get; set; }
								public string Email { get; set; }
								public string FirstName { get; set; }
								public string LastName { get; set; }
								public List<Group> Groups { get; set; }
								public int ID { get; set; }
								public string PhoneNumber { get; set; }
								public string Username { get; set; }

								public void UpdateUser()
								{

								}

								public override string ToString()
								{
												return $"{FirstName} {LastName} - {Username} - {Email} - {PhoneNumber}";
								}
				}
}