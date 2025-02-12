namespace PubWebReport.Components.Pages.Admin.AdminControls;

public partial class Persons
{
	[Inject] public NavigationManager NavManager { get; set; }
	[Inject] public IJSRuntime JS { get; set; }

	[Parameter][SupplyParameterFromQuery] public int UserId { get; set; }
	[Parameter][SupplyParameterFromQuery] public string Password { get; set; }

	private PersonModel PersonModel { get; set; } = new();

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if (!await ValidatePassword()) NavManager.NavigateTo("/");
	}

	private async Task<bool> ValidatePassword() =>
		!string.IsNullOrEmpty(Password) &&
		UserId != 0 &&
		BCrypt.Net.BCrypt.EnhancedVerify((await CommonData.LoadTableDataById<UserModel>(Table.User, UserId)).Password, Password);

	private async Task OnPersonNumberChanged()
	{
		var foundPerson = await PersonData.LoadPersonByNumber(PersonModel.Number);
		if (foundPerson is not null) PersonModel = foundPerson;
		else PersonModel = new() { Number = PersonModel.Number };
	}

	private bool ValidateForm() =>
		!string.IsNullOrEmpty(PersonModel.Number) &&
		!string.IsNullOrEmpty(PersonModel.Name);

	private async Task OnSaveButtonClick()
	{
		if (!ValidateForm())
		{
			await JS.InvokeVoidAsync("alert", "Enter all fields");
			return;
		}

		if (PersonModel.Id == 0)
			await PersonData.InsertPerson(PersonModel);
		else
			await PersonData.UpdatePerson(PersonModel);

		PersonModel = new();
	}
}