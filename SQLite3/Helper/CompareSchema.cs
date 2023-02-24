namespace diub.Database;

public partial class SQLite3 {

	static public bool CompareSchema (SQLiteTableSchema Left, SQLiteTableSchema Right) {
		if (Left.ColumnsCount != Right.ColumnsCount)
			return false;
		return SQLiteTableSchema.Compare (Left, Right);
	}

}   // class

//	namespace	2022-11-18 - 13.02.32