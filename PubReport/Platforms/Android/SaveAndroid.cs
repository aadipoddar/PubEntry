﻿using Android.Content;
using Android.OS;

using Java.IO;

namespace PubReport.Services;

public partial class SaveService
{
	public static partial void SaveAndView(string filename, string contentType, MemoryStream stream)
	{
		string root;

		if (Android.OS.Environment.IsExternalStorageEmulated)
			root = Android.App.Application.Context!.GetExternalFilesDir(Android.OS.Environment.DirectoryDownloads)!.AbsolutePath;
		else
			root = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);

		Java.IO.File myDir = new(root + "/Syncfusion");
		myDir.Mkdir();

		Java.IO.File file = new(myDir, filename);

		if (file.Exists())
			file.Delete();

		try
		{
			FileOutputStream outs = new(file);
			outs.Write(stream.ToArray());

			outs.Flush();
			outs.Close();
		}
		catch (Exception) { }

		if (file.Exists())
		{
			if (Build.VERSION.SdkInt >= BuildVersionCodes.N)
			{
				var fileUri = AndroidX.Core.Content.FileProvider.GetUriForFile(Android.App.Application.Context, Android.App.Application.Context.PackageName + ".provider", file);
				var intent = new Intent(Intent.ActionView);
				intent.SetData(fileUri);
				intent.AddFlags(ActivityFlags.NewTask);
				intent.AddFlags(ActivityFlags.GrantReadUriPermission);
				Android.App.Application.Context.StartActivity(intent);
			}
			else
			{
				var fileUri = Android.Net.Uri.Parse(file.AbsolutePath);
				var intent = new Intent(Intent.ActionView);
				intent.SetDataAndType(fileUri, contentType);
				intent = Intent.CreateChooser(intent, "Open File");
				intent!.AddFlags(ActivityFlags.NewTask);
				Android.App.Application.Context.StartActivity(intent);
			}
		}
	}
}
