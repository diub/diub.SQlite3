namespace diub.Database;

public partial class SQLite3 {

	/// <summary>
	/// Prüft, ob die Spalten identisch sind. Die Reihenfolge im Schema ist dabei egal.
	/// </summary>
	/// <param name="Left"></param>
	/// <param name="Right"></param>
	/// <returns></returns>
	static public bool CompareSchema (SQLiteTableSchema Left, SQLiteTableSchema Right) {
		if (Left.ColumnsCount != Right.ColumnsCount)
			return false;
		return SQLiteTableSchema.Compare (Left, Right);
	}

	/// <summary>
	/// Prüft, ob die Spalten identisch sind. Die Reihenfolge im Schema wird dabei beachtet.
	/// </summary>
	/// <param name="Left"></param>
	/// <param name="Right"></param>
	/// <returns></returns>
	static public bool CompareSchemaWithColumnOrder (SQLiteTableSchema Left, SQLiteTableSchema Right) {
		if (Left.ColumnsCount != Right.ColumnsCount)
			return false;
		return SQLiteTableSchema.CompareWithColumnOrder (Left, Right);
	}

}   // class

//	namespace	2022-11-18 - 13.02.32