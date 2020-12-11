using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

/*
 * For this example use packages System.Data.SQLite.Core and Dapper
 * enoksaju@gmail.com
 * */

namespace Shared
{
	public class DbContext
	{
		// Constant with Database Name
		private const string DBNAME = "database.db";

		// Constant with create Table students Query
		private const string CREATE_TABLE = @"
CREATE TABLE IF NOT EXISTS Students (
	id INTEGER PRIMARY KEY AUTOINCREMENT,
	FirstName VARCHAR(100) NOT NULL,
	LastName VARCHAR(100) NOT NULL,
	Phone VARCHAR(10),
	Grade INT NOT NULL
);
";

		// bol variable indicate if the database as create recently
		private static bool IsDBRecentlyCreated = false;


		/// <summary>
		/// Initialize the database, call on Program.cs before calling mainForm
		/// </summary>
		public static void up()
		{
			// check if database file exist
			if (!File.Exists(Path.GetFullPath(DBNAME)))
			{
				// if not exist create new
				SQLiteConnection.CreateFile(DBNAME);

				// set true 
				IsDBRecentlyCreated = true;
			}

			// If is recently create
			if (IsDBRecentlyCreated)
			{
				// instantiate new DbContext
				using (var ctx = GetInstance())
				{
					// Create command and execute create table first time
					using (var cmd = new SQLiteCommand(CREATE_TABLE, ctx))
					{
						cmd.ExecuteNonQuery();
					}

					// For testinf propource create 5 elemets
					for (var i = 1; i <= 5; i++)
					{
						// Call studentService insert function
						StudentService.Insert(
							// Add new Student
							new Student() { FirstName = $"FirstName {i}", LastName = $"LastName {i}", Phone = $"123456789{i}", Grade = 1 },
							// Pass current DBContext to function parameter
							ctx);
					}
				}
			}
		}

		/// <summary>
		/// Create a new instance of DbContext
		/// </summary>
		/// <returns>an istance of DBContext</returns>
		public static SQLiteConnection GetInstance()
		{
			// Create new SQLiteConnection with StringConnection
			var db = new SQLiteConnection($"Data Source={DBNAME};Version=3;");

			// Open DbConnection
			db.Open();

			// return the connection
			return db;
		}
	}


}
