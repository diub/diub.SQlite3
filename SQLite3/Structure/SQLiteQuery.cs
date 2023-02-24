namespace diub.Database;

public partial class SQLite3 {

	/// <summary>
	/// Zur Speicherung von Abfragen, die direkt mit einer der Varianten 
	/// von <see cref="NativeMapper.ExecuteNonQuery(string, string[], object[], string[], object[])"/> 
	/// oder <see cref="NativeMapper.ExecuteQuery(Type[], string, string[], object[])"/>
	/// verwendet werden können.
	/// </summary>
	abstract public class SQLiteQuery {

		/// <summary>
		/// Die originalen Abfragen.
		/// </summary>
		public QueryItem [] Queries;

		public string Tablename;
		internal Dictionary<string, ColumnSchema<SQLiteTypes>> TableMapping;
		public string Query;
		public Type [] TargetTypes;
		public SQLiteTypes [] SQLiteTypes;
		public string [] FixedArgNames;
		public string [] ArgumentNames;
		public string [] FixedValueNames;

	}

	public class SQLiteSelectQuery : SQLiteQuery {

	}   // class

	public class SQLiteInsertQuery : SQLiteQuery {

	}   // class

	public class SQLiteUpdateQuery : SQLiteQuery {

	}   // class

	public class SQLiteDeleteQuery : SQLiteQuery {

	}   // class
	
	public class SQLiteCountQuery : SQLiteQuery {

	}   // class

}   // class

//	namespace	2022-09-30 - 11.12.40