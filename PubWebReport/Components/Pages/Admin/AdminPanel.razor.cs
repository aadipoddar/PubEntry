namespace PubWebReport.Components.Pages.Admin;

public partial class AdminPanel
{
	[Inject] public NavigationManager NavManager { get; set; }

	[Parameter][SupplyParameterFromQuery] public int UserId { get; set; }
	[Parameter][SupplyParameterFromQuery] public string Password { get; set; }

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if (!await ValidatePassword()) NavManager.NavigateTo("/");

		else NavManager.NavigateTo(
				$"/admin/updateAdvance" +
				$"?UserId={UserId}" +
				$"&Password={Password}");
	}

	private async Task<bool> ValidatePassword() =>
		!string.IsNullOrEmpty(Password) &&
		UserId != 0 &&
		BCrypt.Net.BCrypt.EnhancedVerify((await CommonData.LoadTableDataById<UserModel>(Table.User, UserId)).Password, Password);
}