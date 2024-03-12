namespace diub.Database;

public partial class SQLite3 {

	public class SQLiteInsertQuery : SQLiteQuery {
		public SQLiteInsertQuery (SQLite3 CreatorDatabase) : base (CreatorDatabase) {
		}

		public bool Insert (DataRow Row) {
			return Database.Insert (this, Row);
		}
	}   // class

	public class SQLiteInsertQuery<T> : SQLiteQuery {
		public SQLiteInsertQuery (SQLite3 CreatorDatabase) : base (CreatorDatabase) {
		}

		public bool Insert (T Value) {
			return Database.Insert<T> (this, Value);
		}

		public bool Insert (SQLite3 ActiveDatabase, T Value) {
			return ActiveDatabase.Insert<T> (this, Value);
		}
	}   // class


}   // class

//	namespace	2024-03-12 - 13.45.44