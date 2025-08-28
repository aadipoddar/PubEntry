namespace PubWebReport.Components.Pages.Admin.AdminControls;

public partial class Locations
{
	[Inject] public NavigationManager NavManager { get; set; }
	[Inject] public IJSRuntime JS { get; set; }

	private LocationModel LocationModel { get; set; } = new();
	private readonly List<LocationModel> _locations = [];

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if (firstRender && !await ValidatePassword()) NavManager.NavigateTo("/");
	}

	private async Task<bool> ValidatePassword()
	{
		var userId = await JS.InvokeAsync<string>("getCookie", "UserId");
		var password = await JS.InvokeAsync<string>("getCookie", "Password");

		return !string.IsNullOrEmpty(userId) &&
			   !string.IsNullOrEmpty(password) &&
			   BCrypt.Net.BCrypt.EnhancedVerify((await CommonData.LoadTableDataById<UserModel>(TableNames.User, int.Parse(userId))).Password, password);
	}

	protected override async Task OnInitializedAsync() => await LoadData();

	private async Task LoadData()
	{
		_locations.Clear();
		foreach (var location in await CommonData.LoadTableData<LocationModel>(TableNames.Location))
			_locations.Add(location);

		LocationModel = new() { Status = true };
	}

	private void OnLocationSelect(ChangeEventArgs e)
	{
		if (int.TryParse(e.Value.ToString(), out int locationId))
			LocationModel = _locations.FirstOrDefault(u => u.Id == locationId) ?? new LocationModel();
		else LocationModel = new() { Status = true };
	}

	private bool ValidateForm() =>
		!string.IsNullOrEmpty(LocationModel.Name);

	private async Task OnSaveButtonClick()
	{
		if (!ValidateForm())
		{
			await JS.InvokeVoidAsync("alert", "Enter all fields");
			return;
		}

		await LocationData.InsertLocation(LocationModel);

		NavManager.NavigateTo(NavManager.Uri, forceLoad: true);
	}
}