using Microsoft.AspNetCore.Components;

using PubEntryLibrary.Data;
using PubEntryLibrary.DataAccess;
using PubEntryLibrary.Models;

namespace PubEntryMAUI.Components.Pages;

public partial class LoginPage
{
	[Inject] private NavigationManager NavManager { get; set; }

	private int _selectedUserId;
	private string _password = string.Empty;

	private List<UserModel> _users = [];

	protected override async Task OnInitializedAsync()
	{
		SecureStorage.RemoveAll();
		await LoadData();
	}

	private async Task LoadData()
	{
		_users = await CommonData.LoadTableDataByStatus<UserModel>(TableNames.User);
		_selectedUserId = _users.FirstOrDefault()?.Id ?? 0;

		_password = string.Empty;

		StateHasChanged();
	}

	private async Task LogIn()
	{
		if (_selectedUserId == 0 || string.IsNullOrWhiteSpace(_password))
		{
			await Application.Current.MainPage.DisplayAlert("Error", "Please select a user, location and enter a password.", "OK");
			return;
		}

		var user = _users.FirstOrDefault(u => u.Id == _selectedUserId);
		if (user is null || user.Password != _password)
		{
			await Application.Current.MainPage.DisplayAlert("Error", "Invalid user or password.", "OK");
			return;
		}

		await SecureStorage.Default.SetAsync("UserId", user.Id.ToString());
		await SecureStorage.Default.SetAsync("Password", user.Password);
		NavManager.NavigateTo("/", true);
	}
}