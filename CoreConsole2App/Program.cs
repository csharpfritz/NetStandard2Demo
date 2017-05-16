using ConsoleApp1;
using System;

namespace CoreConsole2App
{
	class Program
	{
		static void Main(string[] args)
		{
			var folderName = args.Length > 0 ? args[0] : @"C:\dev\SampleZip\filesToZip";
			var targetFile = args.Length > 0 ? args[1] : @"C:\dev\SampleZip\zippedFiles.zip";

			ZipFiles.Go(folderName, targetFile);

			Console.WriteLine("Completed zipping");
			Console.ReadLine();
		}
	}
}
