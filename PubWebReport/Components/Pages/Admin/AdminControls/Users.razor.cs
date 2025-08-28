namespace PubWebReport.Components.Pages.Admin.AdminControls;

public partial class Users
{
	[Inject] public NavigationManager NavManager { get; set; }
	[Inject] public IJSRuntime JS { get; set; }

	private UserModel UserModel { get; set; } = new();

	private readonly List<UserModel> _users = [];
	private readonly List<LocationModel> _locations = [];

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if (firstRender && !await ValidatePassword())
			NavManager.NavigateTo("/");
	}

	private async Task<bool> ValidatePassword()
	{
		var userId = await JS.InvokeAsync<string>("getCookie", "UserId");
		var password = await JS.InvokeAsync<string>("getCookie", "Password");

		return !string.IsNullOrEmpty(userId) &&
			   !string.IsNullOrEmpty(password) &&
			   BCrypt.Net.BCrypt.EnhancedVerify((await CommonData.LoadTableDataById<UserModel>(TableNames.User, int.Parse(userId))).Password, password);
	}

	protected override async Task OnInitializedAsync() =>
		await LoadData();

	private async Task LoadData()
	{
		_users.Clear();
		foreach (var user in await CommonData.LoadTableData<UserModel>(TableNames.User))
			_users.Add(user);

		_locations.Clear();
		foreach (var location in await CommonData.LoadTableData<LocationModel>(TableNames.Location))
			_locations.Add(location);

		UserModel = new() { Status = true, LocationId = _locations.FirstOrDefault().Id };
	}

	private void OnUserSelect(ChangeEventArgs e)
	{
		if (int.TryParse(e.Value.ToString(), out int userId))
			UserModel = _users.FirstOrDefault(u => u.Id == userId) ?? new UserModel();
		else
			UserModel = new() { Status = true, LocationId = _locations.FirstOrDefault().Id };
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

		await UserData.InsertUser(UserModel);

		UserModel = new() { Status = true, LocationId = _locations.FirstOrDefault().Id };
		NavManager.NavigateTo(NavManager.Uri, forceLoad: true);
	}
}
