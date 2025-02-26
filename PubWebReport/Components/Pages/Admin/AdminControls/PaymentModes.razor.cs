namespace PubWebReport.Components.Pages.Admin.AdminControls;

public partial class PaymentModes
{
	[Inject] public NavigationManager NavManager { get; set; }
	[Inject] public IJSRuntime JS { get; set; }

	private PaymentModeModel PaymentModeModel { get; set; } = new();
	private readonly List<PaymentModeModel> _paymentModes = [];

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
			   BCrypt.Net.BCrypt.EnhancedVerify((await CommonData.LoadTableDataById<UserModel>(Table.User, int.Parse(userId))).Password, password);
	}

	protected override async Task OnInitializedAsync() => await LoadData();

	private async Task LoadData()
	{
		_paymentModes.Clear();
		foreach (var paymentMode in await CommonData.LoadTableData<PaymentModeModel>(Table.PaymentMode))
			_paymentModes.Add(paymentMode);

		PaymentModeModel = new() { Status = true };
	}

	private void OnPaymentModeSelect(ChangeEventArgs e)
	{
		if (int.TryParse(e.Value.ToString(), out int paymentModeId))
			PaymentModeModel = _paymentModes.FirstOrDefault(u => u.Id == paymentModeId) ?? new PaymentModeModel();
		else
			PaymentModeModel = new() { Status = true };
	}

	private bool ValidateForm() =>
		!string.IsNullOrEmpty(PaymentModeModel.Name);

	private async Task OnSaveButtonClick()
	{
		if (!ValidateForm())
		{
			await JS.InvokeVoidAsync("alert", "Enter all fields");
			return;
		}

		if (PaymentModeModel.Id == 0)
			await PaymentModeData.InsertPaymentMode(PaymentModeModel);
		else
			await PaymentModeData.UpdatePaymentMode(PaymentModeModel);

		PaymentModeModel = new() { Status = true };
		NavManager.NavigateTo(NavManager.Uri, forceLoad: true);
	}
}