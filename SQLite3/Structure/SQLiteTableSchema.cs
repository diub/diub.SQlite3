namespace diub.Database;

public partial class SQLite3 {

	/// <summary>
	/// 
	/// </summary>
	public class SQLiteTableSchema : TableSchema<SQLiteTypes> {

		static public bool Compare (SQLiteTableSchema Left, SQLiteTableSchema Right) {
			return TableSchema<SQLiteTypes>.Compare (Left, Right);
		}

		//	static public bool Compare (SQLiteTableSchema Left, SQLiteTableSchema Right) {
		//	ColumnSchema<SQLiteTypes> right;

		//	if (Left == null)
		//		return true;
		//	if (Left.ColumnsCount != Right.ColumnsCount)
		//		return false;
		//	foreach (ColumnSchema<SQLiteTypes> item in Left.TColumns.Values) {
		//		if (!Right.TColumns.TryGetValue (item.ColumnName, out right))
		//			return false;
		//		if (!ColumnSchema<SQLiteTypes>.Compare (item, right))
		//			return false;
		//	}
		//	return true;
		//}

	
	}	// class

}   // class

//	namespace	2022-11-18 - 13.12.10