// Copyright (c) 2015, Ben Baker
// All rights reserved.
// 
// This source code is licensed under the BSD-style license found in the
// LICENSE file in the root directory of this source tree. 

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using ICSharpCode.SharpZipLib.Zip;

namespace CPWizard
{
	class Zip
	{
		public static byte[] UnZipMemory(byte[] buffer)
		{
			MemoryStream outputStream = new MemoryStream();

			try
			{
				using (MemoryStream inputStream = new MemoryStream(buffer))
				{
					using (ZipInputStream zipStream = new ZipInputStream(inputStream))
					{
						ZipEntry theEntry = null;

						while ((theEntry = zipStream.GetNextEntry()) != null)
						{
							if (!String.IsNullOrEmpty(theEntry.Name))
							{
								int size = 2048;
								byte[] data = new byte[2048];
								while (true)
								{
									size = zipStream.Read(data, 0, data.Length);
									if (size > 0)
										outputStream.Write(data, 0, size);
									else
										break;
								}
							}
						}
					}
				}
			}
			catch
			{
				return null;
			}

			outputStream.Flush();
			outputStream.Close();

			return outputStream.ToArray();
		}

		public static byte[] ZipMemory(byte[] buffer)
		{
			MemoryStream outputStream = new MemoryStream();

			try
			{
				using (ZipOutputStream zipStream = new ZipOutputStream(outputStream))
				{
					zipStream.SetLevel(9); // 0 - store only to 9 - means best compression

					using (MemoryStream inputStream = new MemoryStream(buffer))
					{
						ZipEntry entry = new ZipEntry("Zip");

						entry.Size = inputStream.Length;

						entry.DateTime = DateTime.Now;
						zipStream.PutNextEntry(entry);

						int sourceBytes;
						do
						{
							sourceBytes = inputStream.Read(buffer, 0, buffer.Length);
							zipStream.Write(buffer, 0, sourceBytes);
						} while (sourceBytes > 0);
					}

					zipStream.Finish();
					zipStream.Close();
				}
			}
			catch
			{
				return null;
			}

			outputStream.Flush();
			outputStream.Close();

			return outputStream.ToArray();
		}

		public static List<string> UnZipFile(string s_ZipFile, string d_Folder)
		{
			List<string> fileList = new List<string>();

			try
			{
				if (!File.Exists(s_ZipFile))
					return null;

				using (ZipInputStream zipStream = new ZipInputStream(File.OpenRead(s_ZipFile)))
				{
					ZipEntry theEntry;

					while ((theEntry = zipStream.GetNextEntry()) != null)
					{
						Console.WriteLine(theEntry.Name);

						string directoryName = Path.Combine(d_Folder, Path.GetDirectoryName(theEntry.Name));
						string fileName = Path.Combine(directoryName, Path.GetFileName(theEntry.Name));

						// create directory
						if (directoryName.Length > 0)
							Directory.CreateDirectory(directoryName);

						if (fileName != String.Empty)
						{
							fileList.Add(fileName);

							using (FileStream streamWriter = File.Create(fileName))
							{
								int size = 2048;
								byte[] data = new byte[2048];
								while (true)
								{
									size = zipStream.Read(data, 0, data.Length);
									if (size > 0)
										streamWriter.Write(data, 0, size);
									else
										break;
								}
							}
						}
					}
				}
			}
			catch
			{
			}

			return fileList;
		}

		public static void ZipFile(string s_Folder, string s_ZipFile)
		{
			try
			{
				string[] filenames = Directory.GetFiles(s_Folder);

				using (ZipOutputStream zipStream = new ZipOutputStream(File.Create(s_ZipFile)))
				{
					zipStream.SetLevel(9); // 0 - store only to 9 - means best compression

					byte[] buffer = new byte[4096];

					foreach (string file in filenames)
					{
						using (FileStream fs = File.OpenRead(file))
						{
							ZipEntry entry = new ZipEntry(Path.GetFileName(file));

							entry.Size = fs.Length;

							entry.DateTime = DateTime.Now;
							zipStream.PutNextEntry(entry);

							int sourceBytes;
							do
							{
								sourceBytes = fs.Read(buffer, 0, buffer.Length);
								zipStream.Write(buffer, 0, sourceBytes);
							} while (sourceBytes > 0);
						}
					}

					zipStream.Finish();
					zipStream.Close();
				}
			}
			catch
			{
			}
		}
	}
}
