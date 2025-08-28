using OfficeOpenXml;

using PubEntryLibrary.Data;

FileInfo fileInfo = new(@"C:\Others\Loyalty.xlsx");

ExcelPackage.License.SetNonCommercialPersonal("AadiSoft");

using var package = new ExcelPackage(fileInfo);

await package.LoadAsync(fileInfo);

var worksheet = package.Workbook.Worksheets[0];

int row = 1;

while (worksheet.Cells[row, 1].Value != null)
{
	var name = worksheet.Cells[row, 1].Value.ToString();
	var phone = worksheet.Cells[row, 2].Value.ToString();

	if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(phone))
	{
		Console.WriteLine("Not Inserted Row = " + row);
		continue;
	}

	phone = phone.Replace(" ", "");

	var foundPerson = await PersonData.LoadPersonByNumber(phone);
	if (foundPerson != null)
	{
		Console.WriteLine($"Found Person: {foundPerson.Name} with Phone: {foundPerson.Number}");

		await PersonData.InsertPerson(new()
		{
			Id = foundPerson.Id,
			Name = name,
			Number = phone,
			Loyalty = true
		});
	}

	else
	{
		Console.WriteLine("Inserting New Person: " + name);
		await PersonData.InsertPerson(new()
		{
			Name = name,
			Number = phone,
			Loyalty = true
		});
	}

	row++;
}

Console.ReadLine();