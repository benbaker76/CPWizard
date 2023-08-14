// Copyright (c) 2015, Ben Baker
// All rights reserved.
// 
// This source code is licensed under the BSD-style license found in the
// LICENSE file in the root directory of this source tree. 

using System;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using System.IO;

namespace CPWizard
{
	class GameInfoNode
	{
		public string GoodName = null;
		//public string Name = null;
		public string Category = null;
		public string Developer = null;
		public int NumPlayers = 0;
		public string Date = null;
		public string Description = null;

		public GameInfoNode()
		{
		}
	}

	class GameInfoDatabase
	{
		public static GameInfoNode GetGameInfo(string databasePath, string GoodName)
		{
			OleDbConnection databaseConnection = null;
			OleDbDataAdapter dataAdapter = null;
			GameInfoNode gameInfo = null;
			DataSet dataSet = null;
			DataTable dataTable = null;

			if (!File.Exists(databasePath))
				return null;

			string connString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + databasePath;

			try
			{
				databaseConnection = new OleDbConnection(connString);

				string sqlCommand = String.Format("SELECT * FROM GameData WHERE GoodName = '{0}'", GoodName.Replace("'", "''"));

				dataAdapter = new OleDbDataAdapter(sqlCommand, connString);
				databaseConnection.Open();

				dataSet = new DataSet();

				dataAdapter.Fill(dataSet);

				dataTable = dataSet.Tables[0];

				if (dataTable.Rows.Count > 0)
				{
					gameInfo = new GameInfoNode();

					gameInfo.GoodName = dataTable.Rows[0]["GoodName"].ToString();
					gameInfo.Category = dataTable.Rows[0]["Category"].ToString();
					gameInfo.Developer = dataTable.Rows[0]["Developer"].ToString();
					gameInfo.NumPlayers = StringTools.FromString<int>(dataTable.Rows[0]["NumPlayers"].ToString());
					gameInfo.Date = dataTable.Rows[0]["Date"].ToString();
					gameInfo.Description = dataTable.Rows[0]["Description"].ToString();
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("GetGameInfo", "GameInfoDatabase", ex.Message, ex.StackTrace);
			}
			finally
			{
				if (dataTable != null)
					dataTable.Dispose();
				if (dataSet != null)
					dataSet.Dispose();
				if (dataAdapter != null)
					dataAdapter.Dispose();
				if (databaseConnection != null)
				{
					databaseConnection.Close();
					databaseConnection.Dispose();
				}
			}

			return gameInfo;
		}
	}
}
