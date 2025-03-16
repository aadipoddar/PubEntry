using System.Reflection;

namespace PubEntry.Forms.Admin;

public partial class PaymentMode : Form
{
	public PaymentMode() => InitializeComponent();

	private void PaymentMode_Load(object sender, EventArgs e) => LoadData();

	private async void LoadData()
	{
		paymentComboBox.DataSource = await CommonData.LoadTableData<PaymentModeModel>(TableNames.PaymentMode);
		paymentComboBox.DisplayMember = nameof(PaymentModeModel.Name);
		paymentComboBox.ValueMember = nameof(PaymentModeModel.Id);

		paymentComboBox.SelectedIndex = -1;

		richTextBoxFooter.Text = $"Version: {Assembly.GetExecutingAssembly().GetName().Version}";
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

	private bool ValidateForm() => !string.IsNullOrEmpty(nameTextBox.Text);

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

		if (paymentComboBox.SelectedIndex == -1) await PaymentModeData.InsertPaymentMode(paymentModeModel);
		else
		{
			paymentModeModel.Id = (paymentComboBox.SelectedItem as PaymentModeModel).Id;
			await PaymentModeData.UpdatePaymentMode(paymentModeModel);
		}

		LoadData();
	}
}
