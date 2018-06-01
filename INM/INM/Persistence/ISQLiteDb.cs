﻿using System.Collections.Generic;
using INM.Models;

namespace INM.Persistence
{
	public interface ISQLiteDb
    {
		bool CreateGroup(Group newGroup);
		bool CreateRecording(AudioRecord newAudio);
		bool CreateContact(int lowerUserId, int upperUserId);
		bool CreateUser(User newUser);

		bool DeleteContact(int lowerUserId, int upperUserId);
		bool DeleteGroup(int groupId);

		bool UpdateGroup(Group groupToUpdate);
		bool UpdateRecording(AudioRecord recordingToUpdate);		
		bool UpdateUser(User userToUpdate);

		List<User> GetUsers();
		List<Group> GetGroups();
		User GetUserByUsername(string userName);
		User GetUserByEmail(string email);
		User GetUserById(int userId);
		UserUser GetContact(int lowerUserId, int upperUserId);
    }
}