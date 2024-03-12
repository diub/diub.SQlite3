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

		/// <summary>
		/// Für Aufrufe der Art:  SqliteQuery.Insert(...);
		/// </summary>
		public SQLite3 Database;

		public SQLiteQuery (SQLite3 CreatorDatabase) {
			Database = CreatorDatabase;
		}

	}

	public class SQLiteSelectQuery<T> : SQLiteQuery {
		public SQLiteSelectQuery (SQLite3 CreatorDatabase) : base (CreatorDatabase) {
		}

		public List<T> SelectList (params object [] Args) {
			return Database.SelectList<T> (this, Args);
		}

		public T SelectOne (params object [] Args) {
			return Database.SelectOne<T> (this, Args);
		}

		public T SelectOne (SQLite3 ActiveDatabase, params object [] Args) {
			return ActiveDatabase.SelectOne<T> (this, Args);
		}
	}   // class

	public class SQLiteUpdateQuery<T> : SQLiteQuery {
		public SQLiteUpdateQuery (SQLite3 CreatorDatabase) : base (CreatorDatabase) {
		}

		public bool UpdateOrInsert (T Value, params object [] Args) {
			return Database.UpdateOrInsert<T> (this, Value, Args);
		}

		public bool UpdateOrInsert (SQLite3 ActiveDatabase, T Value, params object [] Args) {
			return ActiveDatabase.UpdateOrInsert<T> (this, Value, Args);
		}

		public bool UpdateIfExist (T Value, params object [] Args) {
			return Database.UpdateIfExist<T> (this, Value, Args);
		}
		
		public bool UpdateIfExist (SQLite3 ActiveDatabase, T Value, params object [] Args) {
			return ActiveDatabase.UpdateIfExist<T> (this, Value, Args);
		}
	}   // class

	public class SQLiteDeleteQuery : SQLiteQuery {
		public SQLiteDeleteQuery (SQLite3 CreatorDatabase) : base (CreatorDatabase) {
		}

		public bool Delete (params object [] Args) {
			return Database.Delete (this, Args);
		}

		public bool Delete (SQLite3 ActiveDatabase, params object [] Args) {
			return ActiveDatabase.Delete (this, Args);
		}
	}   // class

	public class SQLiteCountQuery : SQLiteQuery {
		public SQLiteCountQuery (SQLite3 CreatorDatabase) : base (CreatorDatabase) {
		}

		public int Count (params object [] Args) {
			return Database.Count (this, Args);
		}

		public int Count (SQLite3 ActiveDatabase, params object [] Args) {
			return ActiveDatabase.Count (this, Args);
		}
	}   // class

}   // class

//	namespace	2022-09-30 - 11.12.40