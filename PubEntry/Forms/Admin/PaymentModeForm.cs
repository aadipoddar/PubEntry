namespace PubEntry.Forms.Admin;

public partial class PaymentModeForm : Form
{
	public PaymentModeForm() => InitializeComponent();

	private void PaymentModeForm_Load(object sender, EventArgs e) => LoadData();

	private async void LoadData()
	{
		paymentModeComboBox.DataSource = await CommonData.LoadTableData<PaymentModeModel>("PaymentModeTable");
		paymentModeComboBox.DisplayMember = nameof(PaymentModeModel.Name);
		paymentModeComboBox.ValueMember = nameof(PaymentModeModel.Id);

		paymentModeComboBox.SelectedIndex = -1;
	}

	private void paymentModeComboBox_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (paymentModeComboBox.SelectedIndex == -1)
		{
			nameTextBox.Clear();
			statusCheckBox.Checked = true;
		}
		else
		{
			var paymentModeModel = (PaymentModeModel)paymentModeComboBox.SelectedItem;
			nameTextBox.Text = paymentModeModel.Name;
			statusCheckBox.Checked = paymentModeModel.Status;
		}
	}

	private bool ValidateForm() => !string.IsNullOrWhiteSpace(nameTextBox.Text);

	private async void saveButton_Click(object sender, EventArgs e)
	{
		if (!ValidateForm())
		{
			MessageBox.Show("Please Enter all the Fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			return;
		}

		if (paymentModeComboBox.SelectedIndex == -1)
			await PaymentModeData.PaymentModeInsert(new PaymentModeModel
			{
				Id = 0,
				Name = nameTextBox.Text,
				Status = statusCheckBox.Checked
			});
		else
			await PaymentModeData.PaymentModeUpdate(new PaymentModeModel
			{
				Id = ((PaymentModeModel)paymentModeComboBox.SelectedItem).Id,
				Name = nameTextBox.Text,
				Status = statusCheckBox.Checked
			});

		ClearForm();
	}

	private void ClearForm()
	{
		paymentModeComboBox.SelectedIndex = -1;
		nameTextBox.Clear();
		statusCheckBox.Checked = true;
		LoadData();
	}
}
