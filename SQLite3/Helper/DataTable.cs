namespace diub.Database;

public partial class SQLite3 {

	static public DataTable DataTableFromSchema (TableSchema<SQLiteTypes> Schema) {
		DataTable dt;

		dt = new DataTable ();
		foreach (ColumnSchema<SQLiteTypes> col in Schema.Columns.Values)
			dt.Columns.Add (new DataColumn (col.ColumnName, col.MappingType));
		return dt;
	}

}   // class

//	namespace	2022-11-20 - 15.09.39