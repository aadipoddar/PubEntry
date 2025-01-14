namespace PubEntryLibrary.Data;

public class UserData
{
	public static async Task InsertUser(UserModel userModel) =>
			await SqlDataAccess.SaveData(StoredProcedure.InsertUser, userModel);

	public static async Task UpdateUser(UserModel userModel) =>
			await SqlDataAccess.SaveData(StoredProcedure.UpdateUser, userModel);

	public static async Task<List<UserModel>> LoadUsersByLocationId(int LocationId) =>
			await SqlDataAccess.LoadData<UserModel, dynamic>(StoredProcedure.LoadUsersByLocationId, new { LocationId });
}
