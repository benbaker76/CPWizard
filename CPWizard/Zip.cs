// Copyright (c) 2015, Ben Baker
// All rights reserved.
// 
// This source code is licensed under the BSD-style license found in the
// LICENSE file in the root directory of this source tree. 

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace CPWizard
{
    public static class Zip
    {
        /// <summary>
        /// Unzips all entries in the byte[] buffer and concatenates them into a single byte[].
        /// Returns null on error.
        /// </summary>
        public static byte[] UnZipMemory(byte[] buffer)
        {
            try
            {
                using var input = new MemoryStream(buffer);
                using var archive = new ZipArchive(input, ZipArchiveMode.Read);
                using var output = new MemoryStream();

                foreach (var entry in archive.Entries)
                {
                    if (string.IsNullOrEmpty(entry.Name))
                        continue; // skip directory entries

                    using var entryStream = entry.Open();
                    entryStream.CopyTo(output);
                }

                return output.ToArray();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Zips the given buffer into a single-entry archive named "Zip".
        /// Returns null on error.
        /// </summary>
        public static byte[] ZipMemory(byte[] buffer)
        {
            try
            {
                using var output = new MemoryStream();
                // leaveOpen:true so we can read the MemoryStream after disposing the archive
                using (var archive = new ZipArchive(output, ZipArchiveMode.Create, leaveOpen: true))
                {
                    var entry = archive.CreateEntry("Zip", CompressionLevel.Optimal);
                    using var entryStream = entry.Open();
                    using var input = new MemoryStream(buffer);
                    input.CopyTo(entryStream);
                }
                return output.ToArray();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Extracts all files from the given zip on disk into d_Folder.
        /// Returns a list of extracted file paths (or null if zip not found).
        /// </summary>
        public static List<string> UnZipFile(string s_ZipFile, string d_Folder)
        {
            if (!File.Exists(s_ZipFile))
                return null;

            var fileList = new List<string>();

            try
            {
                using var archive = System.IO.Compression.ZipFile.OpenRead(s_ZipFile);
                foreach (var entry in archive.Entries)
                {
                    // build the destination path
                    var fullPath = Path.Combine(d_Folder, entry.FullName);
                    var dir = Path.GetDirectoryName(fullPath);
                    if (!string.IsNullOrEmpty(dir))
                        Directory.CreateDirectory(dir);

                    if (string.IsNullOrEmpty(entry.Name))
                        continue; // skip directories

                    entry.ExtractToFile(fullPath, overwrite: true);
                    fileList.Add(fullPath);
                }
            }
            catch
            {
                // you might log or rethrow
            }

            return fileList;
        }

        /// <summary>
        /// Zips all files (not subfolders) in s_Folder into s_ZipFile.
        /// </summary>
        public static void ZipFile(string s_Folder, string s_ZipFile)
        {
            try
            {
                if (File.Exists(s_ZipFile))
                    File.Delete(s_ZipFile);

                using var archive = System.IO.Compression.ZipFile.Open(s_ZipFile, ZipArchiveMode.Create);
                foreach (var file in Directory.GetFiles(s_Folder))
                {
                    var entry = archive.CreateEntry(Path.GetFileName(file), CompressionLevel.Optimal);
                    using var entryStream = entry.Open();
                    using var fs = File.OpenRead(file);
                    fs.CopyTo(entryStream);
                }
            }
            catch
            {
                // you might log or rethrow
            }
        }
    }
}
