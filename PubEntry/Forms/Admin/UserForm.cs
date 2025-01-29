using System.Reflection;

namespace PubEntry.Forms.Admin;

public partial class UserForm : Form
{
	public UserForm() => InitializeComponent();

	private void UserForm_Load(object sender, EventArgs e) => LoadData();

	private async void LoadData()
	{
		userComboBox.DataSource = (await CommonData.LoadTableData<UserModel>(Table.User)).ToList();
		userComboBox.DisplayMember = nameof(UserModel.Name);
		userComboBox.ValueMember = nameof(UserModel.Id);

		userComboBox.SelectedIndex = -1;

		locationComboBox.DataSource = (await CommonData.LoadTableData<LocationModel>(Table.Location)).ToList();
		locationComboBox.DisplayMember = nameof(LocationModel.Name);
		locationComboBox.ValueMember = nameof(LocationModel.Id);

		adminCheckBox.Checked = false;

		richTextBoxFooter.Text = $"Version: {Assembly.GetExecutingAssembly().GetName().Version}";
	}

	private void userComboBox_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (userComboBox?.SelectedItem is UserModel selectedUser)
		{
			nameTextBox.Text = selectedUser.Name;
			passwordTextBox.Text = selectedUser.Password;
			locationComboBox.SelectedValue = selectedUser.LocationId;
			statusCheckBox.Checked = selectedUser.Status;
			adminCheckBox.Checked = selectedUser.Admin;
		}

		else
		{
			nameTextBox.Text = string.Empty;
			passwordTextBox.Text = string.Empty;
			locationComboBox.SelectedIndex = -1;
			statusCheckBox.Checked = true;
			adminCheckBox.Checked = false;
		}
	}

	private bool ValidateForm() =>
		!string.IsNullOrEmpty(nameTextBox.Text) &&
		!string.IsNullOrEmpty(passwordTextBox.Text);

	private async void saveButton_Click(object sender, EventArgs e)
	{
		if (!ValidateForm())
		{
			MessageBox.Show("Please fill in all fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			return;
		}

		UserModel userModel = new()
		{
			Name = nameTextBox.Text,
			Password = passwordTextBox.Text,
			LocationId = (locationComboBox.SelectedItem as LocationModel).Id,
			Admin = adminCheckBox.Checked,
			Status = statusCheckBox.Checked
		};

		if (userComboBox.SelectedIndex == -1) await UserData.InsertUser(userModel);
		else
		{
			userModel.Id = (userComboBox.SelectedItem as UserModel).Id;
			await UserData.UpdateUser(userModel);
		}

		LoadData();
	}
}
