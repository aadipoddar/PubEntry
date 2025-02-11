namespace PubWebReport.Components.Pages.Admin.AdminControls;

public partial class PaymentModes
{
	[Inject] public NavigationManager NavManager { get; set; }
	[Inject] public IJSRuntime JS { get; set; }

	[Parameter][SupplyParameterFromQuery] public int UserId { get; set; }
	[Parameter][SupplyParameterFromQuery] public string Password { get; set; }

	private PaymentModeModel PaymentModeModel { get; set; } = new();
	private readonly List<PaymentModeModel> paymentModes = [];

	protected override async Task OnInitializedAsync()
	{
		PaymentModeModel = new();
		await LoadData();
	}

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if (!await ValidatePassword()) NavManager.NavigateTo("/");
	}

	private async Task<bool> ValidatePassword() =>
		!string.IsNullOrEmpty(Password) &&
		UserId != 0 &&
		BCrypt.Net.BCrypt.EnhancedVerify((await CommonData.LoadTableDataById<UserModel>(Table.User, UserId)).Password, Password);

	private async Task LoadData()
	{
		paymentModes.Clear();
		foreach (var paymentMode in await CommonData.LoadTableData<PaymentModeModel>(Table.PaymentMode))
			paymentModes.Add(paymentMode);

		PaymentModeModel = new() { Status = true };
	}

	private void OnPaymentModeSelect(ChangeEventArgs e)
	{
		if (int.TryParse(e.Value.ToString(), out int paymentModeId))
			PaymentModeModel = paymentModes.FirstOrDefault(u => u.Id == paymentModeId) ?? new PaymentModeModel();
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