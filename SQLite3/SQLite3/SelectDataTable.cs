namespace diub.Database;

public partial class SQLite3 {
	
	/// <summary>
	/// Zur Abfrage in eine <see cref="DataTable"/>.
	/// </summary>
	/// <param name="TableSchema"></param>
	/// <param name="Tablename"></param>
	/// <param name="Queries"></param>
	/// <returns></returns>
	public DataTable SelectTableByQueries (string Tablename, params QueryItem [] Queries) {
		string [] where, arg_names;
		List<Dictionary<string, object>> rows;
		object [] args;
		DataTable dt;
		DataRow row;
		TableSchema<SQLiteTypes> table_schema;

		(where, arg_names, args) = QuerySetAsSQLiteStatements (Queries);
		rows = SelectAllRowsWhere (Tablename, where, arg_names, args);
		table_schema = tableschema_cache [Tablename];
		dt = DataTableFromSchema (table_schema);
		foreach (Dictionary<string, object> item in rows) {
			row = dt.NewRow ();
			foreach (DataColumn column in dt.Columns)
				row [column.ColumnName] = item [column.ColumnName];
			dt.Rows.Add (row);
		}
		return dt;
	}
}   // class

//	namespace	2022-11-30 - 12.26.06