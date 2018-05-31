using System.Collections.Generic;
using INM.Models;

namespace INM.Persistence
{
	public interface ISQLiteDb
    {
	    void GetConnection();
		bool CreateUser(User newUser);
		bool CreateGroup(Group newGroup);
		bool CreateRecording(AudioRecord newAudio);
		bool CreateContact(User fromUser, User toUser);

		bool UpdateRecording();
		bool UpdateContact();
		bool UpdateUser();
		bool UpdateGroup();
		List<User> GetUsers();
		List<Group> GetGroups();
		
    }
}
