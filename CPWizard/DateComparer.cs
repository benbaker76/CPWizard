// Copyright (c) 2015, Ben Baker
// All rights reserved.
// 
// This source code is licensed under the BSD-style license found in the
// LICENSE file in the root directory of this source tree. 

using System;

namespace CPWizard
{
	/// <summary>
	/// Summary description for DateComparer.
	/// </summary>
	sealed class DateComparer : System.Collections.IComparer
	{
		public int Compare(object info1, object info2)
		{
			System.IO.FileInfo fileInfo1 = info1 as System.IO.FileInfo;
			System.IO.FileInfo fileInfo2 = info2 as System.IO.FileInfo;

			DateTime Date1 = (fileInfo1 == null) ? System.Convert.ToDateTime(System.Data.SqlTypes.SqlDateTime.Null) : fileInfo1.LastWriteTime;
			DateTime Date2 = (fileInfo2 == null) ? System.Convert.ToDateTime(System.Data.SqlTypes.SqlDateTime.Null) : fileInfo2.LastWriteTime;

			if (Date1 > Date2) return 1;
			if (Date1 < Date2) return -1;
			return 0;
		}
	}
}
