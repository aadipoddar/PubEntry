namespace PubWebReport.Components.Pages.Admin.AdminControls;

public partial class Locations
{
	[Inject] public NavigationManager NavManager { get; set; }
	[Inject] public IJSRuntime JS { get; set; }

	[Parameter][SupplyParameterFromQuery] public int UserId { get; set; }
	[Parameter][SupplyParameterFromQuery] public string Password { get; set; }

	private LocationModel LocationModel { get; set; } = new();
	private readonly List<LocationModel> locations = [];

	protected override async Task OnInitializedAsync()
	{
		LocationModel = new();
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
		locations.Clear();
		foreach (var location in await CommonData.LoadTableData<LocationModel>(Table.Location))
			locations.Add(location);

		LocationModel = new() { Status = true };
	}

	private void OnLocationSelect(ChangeEventArgs e)
	{
		if (int.TryParse(e.Value.ToString(), out int locationId))
			LocationModel = locations.FirstOrDefault(u => u.Id == locationId) ?? new LocationModel();
		else
			LocationModel = new() { Status = true };
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

		if (LocationModel.Id == 0)
			await LocationData.InsertLocation(LocationModel);
		else
			await LocationData.UpdateLocation(LocationModel);

		LocationModel = new() { Status = true };
		NavManager.NavigateTo(NavManager.Uri, forceLoad: true);
	}
}