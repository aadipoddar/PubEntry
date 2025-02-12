namespace PubWebReport.Components.Pages.Admin;

public partial class NavMenu
{
	[Inject] public NavigationManager NavManager { get; set; }

	[Parameter][SupplyParameterFromQuery] public int UserId { get; set; }
	[Parameter][SupplyParameterFromQuery] public string Password { get; set; }

#nullable enable
	private string? advanceHref;
	private string? usersHref;
	private string? locationsHref;
	private string? paymentModesHref;
	private string? reservationTypesHref;
	private string? personsHref;
#nullable disable

	protected override async Task OnParametersSetAsync()
	{
		await base.OnParametersSetAsync();

		if (!await ValidatePassword())
		{
			NavManager.NavigateTo("/");
			return;
		}

		GenerateHref();
	}

	private void GenerateHref()
	{
		advanceHref = $"/admin/advance?UserId={UserId}&Password={Password}";
		usersHref = $"/admin/users?UserId={UserId}&Password={Password}";
		locationsHref = $"/admin/locations?UserId={UserId}&Password={Password}";
		paymentModesHref = $"/admin/paymentModes?UserId={UserId}&Password={Password}";
		reservationTypesHref = $"/admin/reservationTypes?UserId={UserId}&Password={Password}";
		personsHref = $"/admin/persons?UserId={UserId}&Password={Password}";
	}

	private async Task<bool> ValidatePassword() =>
		!string.IsNullOrEmpty(Password) &&
		UserId != 0 &&
		BCrypt.Net.BCrypt.EnhancedVerify((await CommonData.LoadTableDataById<UserModel>(Table.User, UserId)).Password, Password);
}