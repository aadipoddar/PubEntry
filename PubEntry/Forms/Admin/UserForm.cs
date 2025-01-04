﻿namespace PubEntry.Forms.Admin;

public partial class UserForm : Form
{
	public UserForm() => InitializeComponent();

	private void UserForm_Load(object sender, EventArgs e) => LoadComboBox();

	private async void LoadComboBox()
	{
		userComboBox.DataSource = (await CommonData.LoadTableData<UserModel>("UserTable")).ToList();
		userComboBox.DisplayMember = nameof(UserModel.Name);
		userComboBox.ValueMember = nameof(UserModel.Id);

		userComboBox.SelectedIndex = -1;

		locationComboBox.DataSource = (await CommonData.LoadTableData<LocationModel>("LocationTable")).ToList();
		locationComboBox.DisplayMember = nameof(LocationModel.Name);
		locationComboBox.ValueMember = nameof(LocationModel.Id);
	}

	private void userComboBox_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (userComboBox?.SelectedItem is UserModel selectedUser)
		{
			nameTextBox.Text = selectedUser.Name;
			passwordTextBox.Text = selectedUser.Password;
			locationComboBox.SelectedValue = selectedUser.LocationId;
			statusCheckBox.Checked = selectedUser.Status;
		}

		else
		{
			nameTextBox.Text = string.Empty;
			passwordTextBox.Text = string.Empty;
			locationComboBox.SelectedIndex = -1;
			statusCheckBox.Checked = true;
		}
	}

	private bool ValidateForm()
	{
		if (nameTextBox.Text == string.Empty) return false;
		if (passwordTextBox.Text == string.Empty) return false;
		return true;
	}

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
			Status = statusCheckBox.Checked
		};

		if (userComboBox.SelectedIndex == -1) await UserData.UserInsert(userModel);
		else
		{
			userModel.Id = (userComboBox.SelectedItem as UserModel).Id;
			await UserData.UserUpdate(userModel);
		}

		LoadComboBox();
	}
}
