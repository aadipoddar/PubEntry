using Syncfusion.Blazor.Calendars;

using Syncfusion.Blazor.Grids;

namespace PubWebReport.Components.Pages.Admin.AdminControls;

public partial class Advance
{
	[Inject] public NavigationManager NavManager { get; set; }
	[Inject] public IJSRuntime JS { get; set; }

	[Parameter][SupplyParameterFromQuery] public int UserId { get; set; }
	[Parameter][SupplyParameterFromQuery] public string Password { get; set; }

	private LocationModel LocationModel { get; set; } = new();
	private PaymentModeModel PaymentModeModel { get; set; } = new();
	private PersonModel PersonModel { get; set; } = new();
	private AdvanceModel AdvanceModel { get; set; } = new() { AdvanceDate = DateTime.Now.AddHours(5).AddMinutes(30).Date };

	private readonly List<LocationModel> locations = [];
	private readonly List<PaymentModeModel> paymentModes = [];
	private readonly List<AdvancePaymentModel> advancePaymentModels = [];

	private SfGrid<AdvancePaymentModel> AdvanceGrid;

	private int amount = 0;
	private int totalAmount = 0;

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

		LocationModel = locations.FirstOrDefault();

		paymentModes.Clear();
		foreach (var paymentMode in await CommonData.LoadTableDataByStatus<PaymentModeModel>(Table.PaymentMode))
			paymentModes.Add(paymentMode);

		PaymentModeModel = paymentModes.FirstOrDefault();
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
				AdvanceModel = foundAdvance;

				advancePaymentModels.Clear();
				foreach (var advanceDetail in await AdvanceData.LoadAdvanceDetailByAdvanceId(foundAdvance.Id))
					advancePaymentModels.Add(new AdvancePaymentModel
					{
						PaymentModeId = advanceDetail.PaymentModeId,
						PaymentModeName = paymentModes.FirstOrDefault(x => x.Id == advanceDetail.PaymentModeId)?.Name,
						Amount = advanceDetail.Amount
					});
				totalAmount = advancePaymentModels.Sum(x => x.Amount);
			}

			else
			{
				AdvanceModel.Id = 0;
				AdvanceModel.ApprovedBy = string.Empty;
				AdvanceModel.Booking = 0;
				AdvanceModel.LocationId = LocationModel.Id;
				AdvanceModel.PersonId = PersonModel.Id;

				advancePaymentModels.Clear();
				totalAmount = 0;
			}
		}

		else
		{
			AdvanceModel.Id = 0;
			AdvanceModel.ApprovedBy = string.Empty;
			AdvanceModel.Booking = 0;
			AdvanceModel.LocationId = LocationModel.Id;
			AdvanceModel.PersonId = 0;

			advancePaymentModels.Clear();
			totalAmount = 0;
		}

		await AdvanceGrid.Refresh();
	}

	#endregion

	#region DataGrid

	private void OnPaymentModeSelect(ChangeEventArgs e)
	{
		if (int.TryParse(e.Value.ToString(), out int paymentModeId))
			PaymentModeModel = paymentModes.FirstOrDefault(u => u.Id == paymentModeId) ?? new PaymentModeModel();
		else PaymentModeModel = new() { Status = true };
	}

	private async Task OnAddButtonClick()
	{
		if (advancePaymentModels.Any(x => x.PaymentModeId == PaymentModeModel.Id))
			advancePaymentModels.FirstOrDefault(x => x.PaymentModeId == PaymentModeModel.Id).Amount += amount;

		else advancePaymentModels.Add(new AdvancePaymentModel
		{
			PaymentModeId = PaymentModeModel.Id,
			PaymentModeName = PaymentModeModel.Name,
			Amount = amount
		});

		amount = 0;
		totalAmount = advancePaymentModels.Sum(x => x.Amount);
		await AdvanceGrid.Refresh();
	}

	public void RecordClickHandler(RecordClickEventArgs<AdvancePaymentModel> args)
	{
		advancePaymentModels.RemoveAt(args.RowIndex);
		totalAmount = advancePaymentModels.Sum(x => x.Amount);
		AdvanceGrid.Refresh();
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

		if (PersonModel.Id == 0)
		{
			await InsertPerson();
			await InsertAdvance();
		}

		else
		{
			await UpdateAdvance();
			await DeleteAdvanceDetails();
		}

		await InsertAdvanceDetail();

		NavManager.NavigateTo(NavManager.Uri, forceLoad: true);
	}

	private async Task InsertPerson()
	{
		if (PersonModel.Id == 0) PersonModel.Id = await PersonData.InsertPerson(PersonModel);
		PersonModel.Id = await PersonData.UpdatePerson(PersonModel);
	}

	private async Task InsertAdvance()
	{
		AdvanceModel.PersonId = PersonModel.Id;
		AdvanceModel.LocationId = LocationModel.Id;
		AdvanceModel.DateTime = DateTime.Now;
		AdvanceModel.Id = await AdvanceData.InsertAdvance(AdvanceModel);
	}

	private async Task UpdateAdvance() => await AdvanceData.UpdateAdvance(AdvanceModel);

	private async Task DeleteAdvanceDetails() => await AdvanceData.DeleteAdvanceDetails(AdvanceModel.Id);

	private async Task InsertAdvanceDetail()
	{
		foreach (var advance in advancePaymentModels)
			await AdvanceData.InsertAdvanceDetail(new AdvanceDetailModel
			{
				Id = 0,
				AdvanceId = AdvanceModel.Id,
				Amount = advance.Amount,
				PaymentModeId = advance.PaymentModeId
			});
	}

	#endregion
}