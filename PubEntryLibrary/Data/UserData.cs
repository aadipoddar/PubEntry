namespace PubEntryLibrary.Data;

public class UserData
{
	public static async Task InsertUser(UserModel userModel) =>
			await SqlDataAccess.SaveData(StoredProcedureNames.InsertUser, userModel);

	public static async Task UpdateUser(UserModel userModel) =>
			await SqlDataAccess.SaveData(StoredProcedureNames.UpdateUser, userModel);

	public static async Task<List<UserModel>> LoadUsersByLocationId(int LocationId) =>
			await SqlDataAccess.LoadData<UserModel, dynamic>(StoredProcedureNames.LoadUsersByLocationId, new { LocationId });
}
