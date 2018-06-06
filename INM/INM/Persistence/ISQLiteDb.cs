using System.Collections.Generic;
using INM.Models;

namespace INM.Persistence
{
	public interface ISQLiteDb
	{
		bool CreateGroup(Group newGroup);
		bool CreateRecording(AudioRecord newAudio);
		bool CreateContact(int lowerUserId, int upperUserId);
		bool CreateUser(User newUser);

		bool DeleteContact(int user1Id, int user2Id);
		bool DeleteGroup(int groupId);

		bool UpdateGroup(Group groupToUpdate);
		bool UpdateRecording(AudioRecord recordingToUpdate);
		bool UpdateUser(User userToUpdate);

		List<User> GetUsers();
		List<Group> GetUserGroups(int userId);
		List<AudioRecord> GetUserAudioRecordings(int userId);
		User GetUserByUsername(string userName);
		User GetUserByEmail(string email);
		User GetUserById(int userId);
		UserUser GetContact(int lowerUserId, int upperUserId);
		Group GetGroupByName(string name);
		bool CreateGroupUser(GroupUser gu);
		void Nuke();
        List<AudioRecord> GetRecordings();

        bool DeleteRecording(AudioRecord ar);

    }
}
