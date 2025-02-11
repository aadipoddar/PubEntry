namespace PubWebReport.Components.Pages.Admin.AdminControls;

public partial class Users
{
	[Inject] public NavigationManager NavManager { get; set; }
	[Inject] public IJSRuntime JS { get; set; }

	[Parameter][SupplyParameterFromQuery] public int UserId { get; set; }
	[Parameter][SupplyParameterFromQuery] public string Password { get; set; }

	private UserModel UserModel { get; set; } = new();

	private readonly List<UserModel> users = [];
	private readonly List<LocationModel> locations = [];

	protected override async Task OnInitializedAsync()
	{
		UserModel = new();
		await LoadData();
	}

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if (!await ValidatePassword()) NavManager.NavigateTo("/");
	}

	private async Task<bool> ValidatePassword() =>
		!string.IsNullOrEmpty(Password) &&
		UserId != 0 &&
		BCrypt.Net.BCrypt.EnhancedVerify((await CommonData.LoadTableDataById<UserModel>(Table.User, UserId)).Password, Password);

	private async Task LoadData()
	{
		users.Clear();
		foreach (var user in await CommonData.LoadTableData<UserModel>(Table.User))
			users.Add(user);

		locations.Clear();
		foreach (var location in await CommonData.LoadTableData<LocationModel>(Table.Location))
			locations.Add(location);

		UserModel = new() { Status = true, LocationId = locations.FirstOrDefault().Id };
	}

	private void OnUserSelect(ChangeEventArgs e)
	{
		if (int.TryParse(e.Value.ToString(), out int userId))
			UserModel = users.FirstOrDefault(u => u.Id == userId) ?? new UserModel();
		else
			UserModel = new() { Status = true, LocationId = locations.FirstOrDefault().Id };
	}

	private bool ValidateForm() =>
		!string.IsNullOrEmpty(UserModel.Name) &&
		!string.IsNullOrEmpty(UserModel.Password);

	private async Task OnSaveButtonClick()
	{
		if (!ValidateForm())
		{
			await JS.InvokeVoidAsync("alert", "Enter all fields");
			return;
		}

		if (UserModel.Id == 0)
			await UserData.InsertUser(UserModel);
		else
			await UserData.UpdateUser(UserModel);

		UserModel = new() { Status = true, LocationId = locations.FirstOrDefault().Id };
		NavManager.NavigateTo(NavManager.Uri, forceLoad: true);
	}
}
