namespace diub.Database;

public partial class SQLite3 {

	/// <summary>
	/// 
	/// </summary>
	public class SQLiteColumnSchema : ColumnSchema<SQLiteTypes> {

	}

	///// <summary>
	///// Vergleicht SQLite-Spalten-Typ und Namen, nicht jedoch Index und c#-Typ
	///// </summary>
	///// <param name="Left"></param>
	///// <param name="Right"></param>
	///// <returns></returns>
	//static public bool Compare (ColumnSchema<SQLiteTypes> Left, ColumnSchema<SQLiteTypes> Right) {
	//	if (Left == null)
	//		return true;
	//	if (Left.ColumnType != Right.ColumnType)
	//		return false;
	//	if (Left.ColumnName != Right.ColumnName)
	//		return false;
	//	return true;
	//}

}   // class

//	namespace	2022-10-05 - 14.04.40