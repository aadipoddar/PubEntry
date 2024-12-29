namespace PubEntryLibrary.Data;

public class UserData
{
	public static async Task UserInsert(UserModel userModel) =>
			await SqlDataAccess.SaveData("UserInsert", userModel);

	public static async Task UserUpdate(UserModel userModel) =>
			await SqlDataAccess.SaveData("UserUpdate", userModel);

	public static async Task<IEnumerable<UserModel>> LoadActiveUserByLocationId(int locationId) =>
			await SqlDataAccess.LoadData<UserModel, dynamic>("LoadActiveUserByLocationId", new { LocationId = locationId });
}
