namespace PubWebReport.Components.Pages.Admin.AdminControls;

public partial class ReservationTypes
{
	[Inject] public NavigationManager NavManager { get; set; }
	[Inject] public IJSRuntime JS { get; set; }

	private ReservationTypeModel ReservationTypeModel { get; set; } = new();
	private readonly List<ReservationTypeModel> _reservationTypes = [];

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
		_reservationTypes.Clear();
		foreach (var reservationType in await CommonData.LoadTableData<ReservationTypeModel>(TableNames.ReservationType))
			_reservationTypes.Add(reservationType);

		ReservationTypeModel = new() { Status = true };
	}

	private void OnReservationTypeSelect(ChangeEventArgs e)
	{
		if (int.TryParse(e.Value.ToString(), out int reservationTypeId))
			ReservationTypeModel = _reservationTypes.FirstOrDefault(u => u.Id == reservationTypeId) ?? new ReservationTypeModel();
		else
			ReservationTypeModel = new() { Status = true };
	}

	private bool ValidateForm() =>
		!string.IsNullOrEmpty(ReservationTypeModel.Name);

	private async Task OnSaveButtonClick()
	{
		if (!ValidateForm())
		{
			await JS.InvokeVoidAsync("alert", "Enter all fields");
			return;
		}

		await ReservationTypeData.InsertReservationType(ReservationTypeModel);

		ReservationTypeModel = new() { Status = true };
		NavManager.NavigateTo(NavManager.Uri, forceLoad: true);
	}
}