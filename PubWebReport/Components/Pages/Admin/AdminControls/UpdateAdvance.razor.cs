using Syncfusion.Blazor.Calendars;

namespace PubWebReport.Components.Pages.Admin.AdminControls;

public partial class UpdateAdvance
{
	[Inject] public NavigationManager NavManager { get; set; }
	[Inject] public IJSRuntime JS { get; set; }

	private LocationModel LocationModel { get; set; } = new();
	private PersonModel PersonModel { get; set; } = new();
	private AdvanceModel AdvanceModel { get; set; } = new() { AdvanceDate = DateTime.Now.AddHours(5).AddMinutes(30).Date };

	private readonly List<LocationModel> locations = [];

	#region LoadData

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
		locations.Clear();
		foreach (var location in await CommonData.LoadTableDataByStatus<LocationModel>(TableNames.Location))
			locations.Add(location);

		LocationModel = locations.FirstOrDefault();
	}

	private async Task OnLoadAdvanceButtonClicked()
	{
		var advance = await CommonData.LoadTableDataById<AdvanceModel>(TableNames.Advance, AdvanceModel.Id);

		if (advance is null || advance.TransactionId != 0)
		{
			await JS.InvokeVoidAsync("alert", "Invalid Advance Id");
			return;
		}

		AdvanceModel = advance;

		PersonModel = await CommonData.LoadTableDataById<PersonModel>(TableNames.Person, AdvanceModel.PersonId);
		LocationModel = await CommonData.LoadTableDataById<LocationModel>(TableNames.Location, AdvanceModel.LocationId);
	}

	private async Task OnLocationSelect(ChangeEventArgs e)
	{
		if (int.TryParse(e.Value.ToString(), out int locationId))
			LocationModel = locations.FirstOrDefault(u => u.Id == locationId) ?? new LocationModel();
		else LocationModel = new() { Status = true };

		await LoadPersonAdvance();
	}

	private async Task OnPersonNumberChanged()
	{
		var foundPerson = await PersonData.LoadPersonByNumber(PersonModel.Number);
		if (foundPerson is not null) PersonModel = foundPerson;
		else PersonModel = new() { Number = PersonModel.Number };

		await LoadPersonAdvance();
	}

	public async Task OnBookingDateChanged(ChangedEventArgs<DateTime> args)
	{
		AdvanceModel.AdvanceDate = args.Value;
		await LoadPersonAdvance();
	}

	private async Task LoadPersonAdvance()
	{
		if (PersonModel.Id != 0)
		{
			var foundAdvance = await AdvanceData.LoadAdvanceByDateLocationPerson(
				LocationModel.Id,
				PersonModel.Id,
				AdvanceModel.AdvanceDate.Date);

			if (foundAdvance is not null)
			{
				if (foundAdvance.Id != AdvanceModel.Id)
				{
					await JS.InvokeVoidAsync("alert", "Advance Present for this Person and Date, Please Check Again");
					NavManager.NavigateTo(NavManager.Uri, forceLoad: true);
					return;
				}

				AdvanceModel = foundAdvance;
			}

			else
			{
				AdvanceModel.ApprovedBy = string.Empty;
				AdvanceModel.Booking = 0;
				AdvanceModel.LocationId = LocationModel.Id;
				AdvanceModel.PersonId = PersonModel.Id;
			}
		}

		else
		{
			AdvanceModel.ApprovedBy = string.Empty;
			AdvanceModel.Booking = 0;
			AdvanceModel.LocationId = LocationModel.Id;
			AdvanceModel.PersonId = 0;
		}
	}

	#endregion

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

		await InsertPerson();
		await UpdateAdvanceMain();

		NavManager.NavigateTo(NavManager.Uri, forceLoad: true);
	}

	private async Task InsertPerson()
	{
		if (PersonModel.Id == 0) PersonModel.Id = await PersonData.InsertPerson(PersonModel);
		await PersonData.UpdatePerson(PersonModel);
	}

	private async Task UpdateAdvanceMain() => await AdvanceData.UpdateAdvance(AdvanceModel);
}