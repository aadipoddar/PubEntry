﻿using PubEntryLibrary.DataAccess;
using PubEntryLibrary.Models;

namespace PubEntryLibrary.Data;

public static class DataAccess
{
	public static async Task<IEnumerable<T>> LoadTableData<T>(string tableName) where T : new() =>
			await SqlDataAccess.LoadDataSQL<T>($"SELECT * FROM {tableName}", "PubEntry");

	public static async Task InsertPersonTableData(string personName, string personNumber) =>
			await SqlDataAccess.RunSQL($"INSERT INTO PersonTable VALUES ('{personName}', {personNumber})", "PubEntry");

	public static async Task InsertTransactionTableData(TransactionModel transaction) =>
			await SqlDataAccess.RunSQL($"INSERT INTO TransactionTable VALUES ({transaction.PersonId}, {transaction.Male}, {transaction.Female}, {transaction.Cash}, {transaction.Card}, {transaction.UPI}, {transaction.ReservationType}, '{transaction.DateTime}', '{transaction.ApprovedBy}', {transaction.LocationId}, {transaction.EmployeeId})", "PubEntry");

	public static async Task<IEnumerable<T>> GetById<T>(string tableName, int id) where T : new() =>
			await SqlDataAccess.LoadDataSQL<T>($"SELECT * FROM {tableName} WHERE Id = {id}", "PubEntry");

	public static async Task<string> GetLocationNameById(int locationId) =>
		(await GetById<LocationModel>("LocationTable", locationId) as List<LocationModel>).FirstOrDefault().Name;

	public static async Task<List<PersonModel>> GetPersonByNumber(string number) =>
			await SqlDataAccess.LoadDataSQL<PersonModel>($"SELECT * FROM PersonTable WHERE Number = '{number}'", "PubEntry") as List<PersonModel>;

	public static async Task<string> GetEmployeePasswordById(int employeeId) =>
		(await GetById<EmployeeModel>("EmployeeTable", employeeId) as List<EmployeeModel>).FirstOrDefault().Password;

	public static async Task<string> GetReservationTypeById(int reservationTypeId) =>
		(await GetById<ReservationTypeModel>("ReservationTypeTable", reservationTypeId) as List<ReservationTypeModel>).FirstOrDefault().Name;

	public static async Task<int> GetTransactionIdbyDate(string dateTime) =>
		(int)Convert.ToInt64((await SqlDataAccess.LoadDataSQL<TransactionModel>($"SELECT Id FROM TransactionTable WHERE DateTime = '{dateTime}'", "PubEntry")).FirstOrDefault().Id);

	public static async Task<List<TransactionModel>> GetTransactionsByDateRangeAndLocation(string fromDateTime, string toDateTime, int locationId) =>
		await SqlDataAccess.LoadDataSQL<TransactionModel>($"SELECT * FROM TransactionTable WHERE DateTime >= '{fromDateTime}' AND DateTime <= '{toDateTime}' AND LocationId = {locationId}", "PubEntry") as List<TransactionModel>;
}
