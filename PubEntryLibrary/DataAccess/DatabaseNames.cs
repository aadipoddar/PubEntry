namespace PubEntryLibrary.DataAccess;

public static class TableNames
{
	public static string Advance => "Advance";
	public static string AdvanceDetail => "AdvanceDetail";
	public static string Location => "Location";
	public static string PaymentMode => "PaymentMode";
	public static string Person => "Person";
	public static string ReservationType => "ReservationType";
	public static string Transaction => "Transaction";
	public static string User => "User";
	public static string Settings => "Settings";
}

public static class ViewNames
{
	public static string Advances => "View_Advances";
	public static string Transactions => "View_Transactions";
	public static string UserLocation => "View_User_Location";
}

public static class StoredProcedureNames
{
	public static string ClearAdvance => "Clear_Advance";
	public static string DeleteAdvanceDetails => "Delete_AdvanceDetails";
	public static string InsertAdvance => "Insert_Advance";
	public static string InsertAdvanceDetail => "Insert_AdvanceDetail";
	public static string UpdateAdvance => "Update_Advance";
	public static string LoadAdvanceByDateLocationPerson => "Load_Advance_By_Date_Location_Person";
	public static string LoadAdvanceByTransactionId => "Load_Advance_By_TransactionId";
	public static string LoadAdvanceDetailByAdvanceId => "Load_AdvanceDetail_By_AdvanceId";
	public static string LoadAdvancesByForDateLocation => "Load_Advances_By_ForDate_Location";
	public static string LoadAdvancesByTakenOnLocation => "Load_Advances_By_TakenOn_Location";
	public static string LoadAdvancePaymentModeTotalsByTakenOn => "Load_AdvancePaymentModeTotals_By_TakenOn";
	public static string LoadAdvanceTotalsByForDateLocation => "Load_AdvanceTotals_By_ForDate_Location";

	public static string InsertTransaction => "Insert_Transaction";
	public static string UpdateTransaction => "Update_Transaction";
	public static string LoadTransactionsByDateLocation => "Load_Transactions_By_Date_Location";
	public static string LoadTransactionTotalsByDateLocation => "Load_TransactionTotals_By_Date_Location";

	public static string InsertLocation => "Insert_Location";
	public static string InsertPaymentMode => "Insert_PaymentMode";
	public static string InsertPerson => "Insert_Person";
	public static string InsertReservationType => "Insert_ReservationType";
	public static string InsertUser => "Insert_User";

	public static string UpdateLocation => "Update_Location";
	public static string UpdatePaymentMode => "Update_PaymentMode";
	public static string UpdatePerson => "Update_Person";
	public static string UpdateReservationType => "Update_ReservationType";
	public static string UpdateUser => "Update_User";

	public static string LoadTableData => "Load_TableData";
	public static string LoadTableDataById => "Load_TableData_By_Id";
	public static string LoadTableDataByStatus => "Load_TableData_By_Status";
	public static string LoadPersonByNumber => "Load_Person_By_Number";
	public static string LoadPersonByName => "Load_Person_By_Name";
	public static string LoadUsersByLocationId => "Load_Users_By_LocationId";

	public static string LoadSettingsByKey => "Load_Settings_By_Key";
	public static string UpdateSettings => "Update_Settings";
	public static string ResetSettings => "Reset_Settings";
}