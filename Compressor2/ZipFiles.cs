using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
	public class ZipFiles
	{

		public static void Go(string folderName, string outFilename)
		{

			ZipConstants.DefaultCodePage = System.Text.Encoding.Default.CodePage;

			var fsOut = File.Create(outFilename);
			var zipStream = new ZipOutputStream(fsOut);

			zipStream.SetLevel(3);
			zipStream.Password = null;

			var folderOffset = folderName.Length + (folderName.EndsWith("\\") ? 0 : 1);

			CompressFolder(folderName, zipStream, folderOffset);

			zipStream.IsStreamOwner = true; // Makes the Close also Close the underlying stream
			zipStream.Close();

		}

		private static void CompressFolder(string path, ZipOutputStream zipStream, int folderOffset)
		{

			var files = Directory.GetFiles(path);

			foreach (var filename in files)
			{

				var fi = new FileInfo(filename);

				var entryName = filename.Substring(folderOffset); // Makes the name in zip based on the folder
				entryName = ZipEntry.CleanName(entryName); // Removes drive from name and fixes slash direction
				var newEntry = new ZipEntry(entryName);
				newEntry.DateTime = fi.LastWriteTime; // Note the zip format stores 2 second granularity

				// Specifying the AESKeySize triggers AES encryption. Allowable values are 0 (off), 128 or 256.
				// A password on the ZipOutputStream is required if using AES.
				//   newEntry.AESKeySize = 256;

				// To permit the zip to be unpacked by built-in extractor in WinXP and Server2003, WinZip 8, Java, and other older code,
				// you need to do one of the following: Specify UseZip64.Off, or set the Size.
				// If the file may be bigger than 4GB, or you do not need WinXP built-in compatibility, you do not need either,
				// but the zip will be in Zip64 format which not all utilities can understand.
				//   zipStream.UseZip64 = UseZip64.Off;
				newEntry.Size = fi.Length;

				zipStream.PutNextEntry(newEntry);

				// Zip the file in buffered chunks
				// the "using" will close the stream even if an exception occurs
				var buffer = new byte[4096];
				using (var streamReader = File.OpenRead(filename))
				{
					StreamUtils.Copy(streamReader, zipStream, buffer);
				}
				zipStream.CloseEntry();
			}
			var folders = Directory.GetDirectories(path);
			foreach (var folder in folders)
			{
				CompressFolder(folder, zipStream, folderOffset);
			}
		}

	}
}
