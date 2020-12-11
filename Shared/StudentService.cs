using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
	/// <summary>
	/// Service provide functions to get and Insert students from/to Database
	/// </summary>
	public class StudentService
	{
		// Constant for Query Insert
		private const string INSERT_STUDENT = "INSERT INTO Students(FirstName, LastName,Phone,Grade) VALUES(?,?,?,?)";

		// Constant fron Get all data Query
		private const string SELECT_ALL = "SELECT * FROM Students";

		/// <summary>
		/// Read Database to get All Students
		/// </summary>
		/// <returns>List of students</returns>
		public static IEnumerable<Student> GetAll()
		{
			// Instance of result
			var result = new List<Student>();

			// Using context, this contains the SQLite connection
			using (IDbConnection ctx = DbContext.GetInstance())
			{
				// Fill Result with dapper
				result = ctx.Query<Student>(SELECT_ALL).ToList();
			}

			// Return Filled Result
			return result;
		}

		/// <summary>
		/// Insert An student to Database, require DbContext
		/// </summary>
		/// <param name="toInsert">Object to insert</param>
		/// <param name="ctx">DbContext</param>
		/// <returns></returns>

		public static Student Insert(Student toInsert, IDbConnection ctx)
		{
			// Execute SQL Insert with parameters 
			var affectedRows = ctx.Execute(INSERT_STUDENT, new { 
				toInsert.FirstName,
				toInsert.LastName,
				toInsert.Phone,
				toInsert.Grade
			});
			// TODO: write code to add the new ID of student
			// Return Value
			return toInsert;
		}

		/// <summary>
		/// Insert An student to Database
		/// </summary>
		/// <param name="toInsert">object to insert</param>
		/// <returns></returns>
		public static Student Insert(Student toInsert)
		{
			// Instantiate DbContext, this contain the database connection
			using (IDbConnection ctx = DbContext.GetInstance())
			{
				// Call Insert method with dbcontext and return value
				return Insert(toInsert, ctx);
			}
		}
	}
}
