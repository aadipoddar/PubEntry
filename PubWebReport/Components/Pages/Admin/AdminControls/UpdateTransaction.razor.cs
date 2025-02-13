namespace PubWebReport.Components.Pages.Admin.AdminControls;

public partial class UpdateTransaction
{
	[Inject] public NavigationManager NavManager { get; set; }
	[Inject] public IJSRuntime JS { get; set; }

	[Parameter][SupplyParameterFromQuery] public int UserId { get; set; }
	[Parameter][SupplyParameterFromQuery] public string Password { get; set; }

	private TransactionModel TransactionModel { get; set; } = new();
	private LocationModel LocationModel { get; set; } = new();
	private PersonModel PersonModel { get; set; } = new();
	private AdvanceModel AdvanceModel { get; set; } = new() { AdvanceDate = DateTime.Now.AddHours(5).AddMinutes(30).Date };

	private readonly List<LocationModel> locations = [];
	private readonly List<ReservationTypeModel> reservationTypes = [];

	private int totalAdvance = 0;

	#region LoadData

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if (!await ValidatePassword()) NavManager.NavigateTo("/");
	}

	private async Task<bool> ValidatePassword() =>
		!string.IsNullOrEmpty(Password) &&
		UserId != 0 &&
		BCrypt.Net.BCrypt.EnhancedVerify((await CommonData.LoadTableDataById<UserModel>(Table.User, UserId)).Password, Password);

	protected override async Task OnInitializedAsync() => await LoadData();

	private async Task LoadData()
	{
		locations.Clear();
		foreach (var location in await CommonData.LoadTableDataByStatus<LocationModel>(Table.Location))
			locations.Add(location);

		reservationTypes.Clear();
		foreach (var reservationType in await CommonData.LoadTableDataByStatus<ReservationTypeModel>(Table.ReservationType))
			reservationTypes.Add(reservationType);

		LocationModel = locations.FirstOrDefault();
	}

	private async Task OnLoadTransactionButtonClicked()
	{
		var tarnsaction = await CommonData.LoadTableDataById<TransactionModel>(Table.Transaction, TransactionModel.Id);
		if (tarnsaction is null)
		{
			await JS.InvokeVoidAsync("alert", "Invalid Transaction Id");
			return;
		}

		TransactionModel = tarnsaction;
		PersonModel = await CommonData.LoadTableDataById<PersonModel>(Table.Person, TransactionModel.PersonId);
		LocationModel = await CommonData.LoadTableDataById<LocationModel>(Table.Location, TransactionModel.LocationId);

		await LoadPersonAdvance();
	}

	private async Task OnLocationSelect(ChangeEventArgs e)
	{
		if (int.TryParse(e.Value.ToString(), out int locationId))
			LocationModel = locations.FirstOrDefault(u => u.Id == locationId) ?? new LocationModel();
		else LocationModel = new() { Status = true };

		TransactionModel.LocationId = LocationModel.Id;
		await LoadPersonAdvance();
	}

	private async Task OnPersonNumberChanged()
	{
		var foundPerson = await PersonData.LoadPersonByNumber(PersonModel.Number);
		if (foundPerson is not null) PersonModel = foundPerson;
		else PersonModel = new() { Number = PersonModel.Number };

		await LoadPersonAdvance();
	}

	private async Task LoadPersonAdvance()
	{
		if (PersonModel.Id != 0)
		{
			AdvanceModel foundAdvance;

			if (PersonModel.Id == TransactionModel.PersonId && LocationModel.Id == TransactionModel.LocationId)
			{
				foundAdvance = await AdvanceData.LoadAdvanceByTransactionId(TransactionModel.Id);
				if (foundAdvance is not null)
				{
					AdvanceModel = foundAdvance;
					totalAdvance = (await AdvanceData.LoadAdvanceDetailByAdvanceId(foundAdvance.Id)).Sum(x => x.Amount);
					return;
				}
			}

			else
			{
				foundAdvance = TransactionModel.DateTime.TimeOfDay < TimeSpan.FromHours(17) ?
					await AdvanceData.LoadAdvanceByDateLocationPerson(LocationModel.Id, PersonModel.Id, TransactionModel.DateTime.AddDays(-1))
					: await AdvanceData.LoadAdvanceByDateLocationPerson(LocationModel.Id, PersonModel.Id, TransactionModel.DateTime);
				if (foundAdvance is not null)
				{
					AdvanceModel = foundAdvance;
					totalAdvance = (await AdvanceData.LoadAdvanceDetailByAdvanceId(foundAdvance.Id)).Sum(x => x.Amount);
					return;
				}
			}
		}

		AdvanceModel = new();
		totalAdvance = 0;
	}

	#endregion

	#region Saving

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

		await UpdateTransactions();
		await InsertPerson();
		await UpdateAdvances();

		NavManager.NavigateTo(NavManager.Uri, forceLoad: true);
	}

	private async Task InsertPerson()
	{
		if (PersonModel.Id == 0) PersonModel.Id = await PersonData.InsertPerson(PersonModel);
		PersonModel.Id = await PersonData.UpdatePerson(PersonModel);
	}

	private async Task UpdateTransactions() => await TransactionData.UpdateTransaction(TransactionModel);

	private async Task UpdateAdvances()
	{
		var existingAdvance = await AdvanceData.LoadAdvanceByTransactionId(TransactionModel.Id);
		if (existingAdvance is not null) await AdvanceData.ClearAdvance(existingAdvance.Id, 0);
		if (AdvanceModel.Id is not 0) await AdvanceData.ClearAdvance(AdvanceModel.Id, TransactionModel.Id);
	}

	#endregion
}