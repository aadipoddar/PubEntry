namespace PubWebReport.Components.Pages.Admin;

public partial class NavMenu
{
	[Inject] public NavigationManager NavManager { get; set; }
	[Inject] public IJSRuntime JS { get; set; }

#nullable enable
	private string? _updateAdvanceHref;
	private string? _updateTransactionHref;
	private string? _usersHref;
	private string? _locationsHref;
	private string? _paymentModesHref;
	private string? _reservationTypesHref;
	private string? _personsHref;
#nullable disable

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

	protected override async Task OnParametersSetAsync()
	{
		await base.OnParametersSetAsync();
		GenerateHref();
	}

	private void GenerateHref()
	{
		_updateAdvanceHref = $"/admin/updateAdvance";
		_updateTransactionHref = $"/admin/updateTransaction";
		_usersHref = $"/admin/users";
		_locationsHref = $"/admin/locations";
		_paymentModesHref = $"/admin/paymentModes";
		_reservationTypesHref = $"/admin/reservationTypes";
		_personsHref = $"/admin/persons";
	}
}