namespace diub.Database;

public partial class SQLite3 {

	public bool CreateTable<T> (string Tablename) {
		return CreateTable (Tablename, typeof (T));
	}

	public bool CreateTable (string Tablename, Type CSharpTypes) {
		int i, count;
		Dictionary<string, ColumnSchema<SQLiteTypes>> mappings;
		string [] names, sqlite_types;

		mappings = GetClassTypeMapping (CSharpTypes);
		count = mappings.Count;
		names = new string [count];
		sqlite_types = new string [count];
		i = 0;
		foreach (ColumnSchema<SQLiteTypes> mapping in mappings.Values) {
			names [i] = mapping.ColumnName;
			sqlite_types [i] = mapping.ColumnType.ToString ();
			i++;
		}
		return CreateTable (Tablename, names, sqlite_types);
	}

	public bool CreateTable (string TableName, string [] ColumnNames, params Type [] CSharpTypes) {
		int i;
		string [] types;

		types = new string [CSharpTypes.Length];
		for (i = 0; i < CSharpTypes.Length; i++)
			types [i] = GetSQLiteType (CSharpTypes [i]).ToString ();
		return CreateTable (TableName, ColumnNames, types);
	}

	public bool CreateTable (string TableName, string [] ColumnNames, params string [] SQLiteColumnTypes) {
		int i;
		string query;

		query = "create table if not exists '" + TableName + "' (";
		for (i = 0; i < ColumnNames.Length; i++) {
			query += " '" + ColumnNames [i] + "' " + SQLiteColumnTypes [i];
			if (i < ColumnNames.Length - 1)
				query += ",";
		}
		query += ")";
		return mapper.ExecuteNonQuery (query);
	}


	public bool CreateTable (string Tablename, TableSchema<SQLiteTypes> TableSchema) {
		int i;
		string [] column_names, column_types;

		i = 0;
		column_names = new string [TableSchema.ColumnsCount];
		column_types = new string [TableSchema.ColumnsCount];
		foreach (ColumnSchema<SQLiteTypes> item in TableSchema.TColumns.Values) {
			column_names [i] = item.ColumnName;
			column_types [i] = item.ColumnType.ToString ();
			i++;
		}
		return CreateTable (Tablename, column_names, column_types);
	}

	public bool DropTable (string TableName) {
		string query;

		query = "DROP TABLE IF EXISTS '" + TableName + "'";
		return mapper.ExecuteNonQuery (query);
	}

	public List<string> ListTables () {
		string query;
		Type [] types;
		List<Dictionary<string, object>> rows;
		List<string> tables;

		// NULL als Type für ANSI-string Binding!
		types = new Type [] { typeof (Ansistring) };
		tables = new List<string> ();
		query = "SELECT name FROM sqlite_master WHERE type = 'table'";
		rows = mapper.ExecuteQuery (types, null, query, new string [0], null);
		foreach (Dictionary<string, object> row in rows)
			tables.Add (row ["name"] as string);
		return tables;
	}

	/// <summary>
	/// Löscht allen Daten in der Tabelle.
	/// </summary>
	/// <param name="Tablename"></param>
	/// <returns></returns>
	public bool ClearTable (string Tablename) {
		StringBuilder query;
		
		query = new StringBuilder ("DELETE FROM ");
		query.Append (Tablename);
		return mapper.ExecuteNonQuery (query.ToString (), null, null);
	}

}   // class

// //	namespace	2022-09-09 - 13.35.11}