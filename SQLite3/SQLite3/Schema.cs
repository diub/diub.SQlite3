namespace diub.Database;

public partial class SQLite3 : ISchema<SQLiteTypes> {

	public SQLiteTableSchema GetSQLite3TableSchema (string Tablename) {
		return GetTableSchema (Tablename) as SQLiteTableSchema;
	}

	/// <summary>
	/// Ermittelt das Tabellenschema jedesmal neu direkt aus der Datenbank!
	/// </summary>
	/// <param name="Tablename"></param>
	/// <returns></returns>
	public TableSchema<SQLiteTypes> GetTableSchema (string Tablename) {
		string query;
		Type [] types;
		List<Dictionary<string, object>> rows;
		SQLiteTableSchema table_schema;
		SQLiteColumnSchema column_schema;

		types = new Type [] { typeof (Ansistring), typeof (Ansistring), typeof (Ansistring), typeof (Ansistring), typeof (int), typeof (int) };
		query = "PRAGMA table_info (" + Tablename + ")";
		rows = mapper.ExecuteQuery (types, null, query, null, null);
		if (rows == null || rows.Count == 0)
			return null;
		table_schema = new SQLiteTableSchema () { TableName = Tablename };

		foreach (Dictionary<string, object> row in rows) {
			column_schema = new SQLiteColumnSchema () {
				Index = int.Parse (row ["cid"].ToString ()),
				ColumnName = row ["name"] as string,
				ColumnType = (NativeMapper.SQLiteTypes) Enum.Parse (typeof (NativeMapper.SQLiteTypes), row ["type"] as string),
			};
			//try {
			//	column_schema.Index = (int) row ["cid"];
			//} catch (Exception) {
			//	column_schema.Index = int.Parse (row ["cid"].ToString());
			//}
			column_schema.MappingType = GetCSharpType (column_schema.ColumnType);
			table_schema.TColumns.Add (column_schema.ColumnName, column_schema);
		}
		return table_schema;
	}

}   // class

//	namespace	2022-10-05 - 14.01.49