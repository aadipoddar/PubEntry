using Microsoft.AspNetCore.Components;

using PubEntryLibrary.Data;
using PubEntryLibrary.DataAccess;
using PubEntryLibrary.Models;

using Syncfusion.Blazor.DropDowns;

namespace PubEntryMAUI.Components.Pages;

public partial class LoginPage
{
	[Inject] private NavigationManager NavManager { get; set; }

	private int _selectedLocationId;
	private int _selectedUserId;
	private string _password = string.Empty;

	private List<LocationModel> _locations = [];
	private List<UserModel> _users = [];

	protected override async Task OnInitializedAsync()
	{
		SecureStorage.RemoveAll();
		await LoadData();
	}

	private async Task LoadData()
	{
		_locations = await CommonData.LoadTableDataByStatus<LocationModel>(TableNames.Location);
		_selectedLocationId = _locations.FirstOrDefault()?.Id ?? 0;

		_users = await UserData.LoadUsersByLocationId(_selectedLocationId);
		_selectedUserId = _users.FirstOrDefault()?.Id ?? 0;

		_password = string.Empty;

		StateHasChanged();
	}

	private async Task OnLocationChanged(ChangeEventArgs<int, LocationModel> e)
	{
		if (e?.Value is null || e?.Value == 0)
			_selectedLocationId = _locations.FirstOrDefault()?.Id ?? 0;

		else
			_selectedLocationId = e.Value;

		List<UserModel> admins = [];

		foreach (var location in _locations)
			admins.AddRange((await UserData.LoadUsersByLocationId((location as LocationModel).Id)).ToList().Where(_ => _.Admin));

		_users = [.. admins
				.Concat((await UserData.LoadUsersByLocationId(_selectedLocationId))
				.ToList()
				.Where(_ => !_.Admin))
				.OrderBy(_ => _.Admin)];
		_selectedUserId = _users.FirstOrDefault()?.Id ?? 0;

		_password = string.Empty;

		StateHasChanged();
	}

	private async Task LogIn()
	{
		if (_selectedUserId == 0 || _selectedLocationId == 0 || string.IsNullOrWhiteSpace(_password))
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