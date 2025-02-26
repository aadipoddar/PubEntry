using Syncfusion.Blazor.Calendars;

using Syncfusion.Blazor.Grids;

namespace PubWebReport.Components.Pages;

public partial class Advance
{
	[Inject] public NavigationManager NavManager { get; set; }
	[Inject] public IJSRuntime JS { get; set; }

	private PaymentModeModel PaymentModeModel { get; set; } = new();
	private PersonModel PersonModel { get; set; } = new();
	private AdvanceModel AdvanceModel { get; set; } = new() { AdvanceDate = DateTime.Now.AddHours(5).AddMinutes(30).Date };

	private readonly List<PaymentModeModel> _paymentModes = [];
	private readonly List<AdvancePaymentModel> _advancePaymentModels = [];

	private SfGrid<AdvancePaymentModel> _advanceGrid;

	private int _userId;
	private string _password;
	private int _locationId;
	private int _amount = 0;
	private int _totalAmount = 0;

	#region LoadData

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if (firstRender && !await ValidatePassword()) NavManager.NavigateTo("/");
	}

	private async Task<bool> ValidatePassword()
	{
		var user = await JS.InvokeAsync<string>("getCookie", "AdvanceUserId");
		if (user == "") return false;

		_userId = int.Parse(user);
		_password = await JS.InvokeAsync<string>("getCookie", "AdvancePassword");
		_locationId = int.Parse(await JS.InvokeAsync<string>("getCookie", "AdvanceLocationId"));

		return !string.IsNullOrEmpty(_userId.ToString()) &&
			   !string.IsNullOrEmpty(_password) &&
			   BCrypt.Net.BCrypt.EnhancedVerify((await CommonData.LoadTableDataById<UserModel>(Table.User, _userId)).Password, _password);
	}

	protected override async Task OnInitializedAsync() => await LoadData();

	private async Task LoadData()
	{
		_paymentModes.Clear();
		foreach (var paymentMode in await CommonData.LoadTableDataByStatus<PaymentModeModel>(Table.PaymentMode))
			_paymentModes.Add(paymentMode);

		PaymentModeModel = _paymentModes.FirstOrDefault();
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
				_locationId,
				PersonModel.Id,
				AdvanceModel.AdvanceDate.Date);

			if (foundAdvance is not null)
			{
				AdvanceModel = foundAdvance;

				_advancePaymentModels.Clear();
				foreach (var advanceDetail in await AdvanceData.LoadAdvanceDetailByAdvanceId(foundAdvance.Id))
					_advancePaymentModels.Add(new AdvancePaymentModel
					{
						PaymentModeId = advanceDetail.PaymentModeId,
						PaymentModeName = _paymentModes.FirstOrDefault(x => x.Id == advanceDetail.PaymentModeId)?.Name,
						Amount = advanceDetail.Amount
					});
				_totalAmount = _advancePaymentModels.Sum(x => x.Amount);
			}

			else
			{
				AdvanceModel.Id = 0;
				AdvanceModel.ApprovedBy = string.Empty;
				AdvanceModel.Booking = 0;
				AdvanceModel.LocationId = _locationId;
				AdvanceModel.PersonId = PersonModel.Id;

				_advancePaymentModels.Clear();
				_totalAmount = 0;
			}
		}

		else
		{
			AdvanceModel.Id = 0;
			AdvanceModel.ApprovedBy = string.Empty;
			AdvanceModel.Booking = 0;
			AdvanceModel.LocationId = _locationId;
			AdvanceModel.PersonId = 0;

			_advancePaymentModels.Clear();
			_totalAmount = 0;
		}

		await _advanceGrid.Refresh();
	}

	#endregion

	#region DataGrid

	private void OnPaymentModeSelect(ChangeEventArgs e)
	{
		if (int.TryParse(e.Value.ToString(), out int paymentModeId))
			PaymentModeModel = _paymentModes.FirstOrDefault(u => u.Id == paymentModeId) ?? new PaymentModeModel();
		else PaymentModeModel = new() { Status = true };
	}

	private async Task OnAddButtonClick()
	{
		if (_advancePaymentModels.Any(x => x.PaymentModeId == PaymentModeModel.Id))
			_advancePaymentModels.FirstOrDefault(x => x.PaymentModeId == PaymentModeModel.Id).Amount += _amount;

		else _advancePaymentModels.Add(new AdvancePaymentModel
		{
			PaymentModeId = PaymentModeModel.Id,
			PaymentModeName = PaymentModeModel.Name,
			Amount = _amount
		});

		_amount = 0;
		_totalAmount = _advancePaymentModels.Sum(x => x.Amount);
		await _advanceGrid.Refresh();
	}

	public void RecordClickHandler(RecordClickEventArgs<AdvancePaymentModel> args)
	{
		_advancePaymentModels.RemoveAt(args.RowIndex);
		_totalAmount = _advancePaymentModels.Sum(x => x.Amount);
		_advanceGrid.Refresh();
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

		await InsertPerson();

		if (AdvanceModel.Id == 0) await InsertAdvance();

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
		AdvanceModel.LocationId = _locationId;
		AdvanceModel.DateTime = DateTime.Now;
		AdvanceModel.UserId = _userId;
		AdvanceModel.Id = await AdvanceData.InsertAdvance(AdvanceModel);
	}

	private async Task UpdateAdvance() => await AdvanceData.UpdateAdvance(AdvanceModel);

	private async Task DeleteAdvanceDetails() => await AdvanceData.DeleteAdvanceDetails(AdvanceModel.Id);

	private async Task InsertAdvanceDetail()
	{
		foreach (var advance in _advancePaymentModels)
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