namespace PubEntry.Forms.Admin;

public partial class PaymentMode : Form
{
	public PaymentMode() => InitializeComponent();

	private void PaymentMode_Load(object sender, EventArgs e) => LoadData();

	private async void LoadData()
	{
		paymentComboBox.DataSource = await CommonData.LoadTableData<PaymentModeModel>("PaymentModeTable");
		paymentComboBox.DisplayMember = nameof(PaymentModeModel.Name);
		paymentComboBox.ValueMember = nameof(PaymentModeModel.Id);

		paymentComboBox.SelectedIndex = -1;
	}

	private void paymentComboBox_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (paymentComboBox.SelectedItem is PaymentModeModel selectedPayment)
		{
			nameTextBox.Text = selectedPayment.Name;
			statusCheckBox.Checked = selectedPayment.Status;
		}
		else
		{
			nameTextBox.Text = string.Empty;
			statusCheckBox.Checked = true;
		}
	}

	private bool ValidateForm()
	{
		if (nameTextBox.Text == string.Empty) return false;
		return true;
	}

	private async void saveButton_Click(object sender, EventArgs e)
	{
		if (!ValidateForm())
		{
			MessageBox.Show("Please enter all fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			return;
		}

		PaymentModeModel paymentModeModel = new()
		{
			Name = nameTextBox.Text,
			Status = statusCheckBox.Checked
		};

		if (paymentComboBox.SelectedIndex == -1) await PaymentModeData.PaymentModeInsert(paymentModeModel);
		else
		{
			paymentModeModel.Id = (paymentComboBox.SelectedItem as PaymentModeModel).Id;
			await PaymentModeData.PaymentModeUpdate(paymentModeModel);
		}

		LoadData();
	}
}
