namespace diub.Database;

public partial class SQLite3 {


	/// <summary>
	/// Generiert die interne Abfrage und liefert die Zeilen. Die Reihenfolge in den Arrays muss stimmig sein!<para></para>
	/// Diese Funktion wird von allen anderen Varianten benutzt. 
	/// </summary>
	/// <param name="GetFields"></param>
	/// <param name="GetTypes"></param>
	/// <param name="Tablename"></param>
	/// <param name="WhereStatments"></param>
	/// <param name="ArgNames"></param>
	/// <param name="Args"></param>
	/// <returns></returns>
	public List<Dictionary<string, object>> SelectListWhere (string [] GetFields, Type [] GetTypes, SQLiteTypes [] SQLiteTypes, string Tablename, string [] WhereStatments, string [] ArgNames, object [] Args) {
		int i;
		StringBuilder query_builder;
		List<Dictionary<string, object>> rows;
		string [] fixed_arg_names;

		fixed_arg_names = null;
		query_builder = new StringBuilder ("SELECT ");
		if (GetFields != null) {
			query_builder.Append ("\"");
			query_builder.Append (GetFields [0]);
			query_builder.Append ("\"");
			for (i = 1; i < GetFields.Count (); i++) {
				query_builder.Append (",");
				query_builder.Append ("\"");
				query_builder.Append (GetFields [i]);
				query_builder.Append ("\"");
			}
		} else {
			query_builder.Append ('*');
		}
		query_builder.Append (" FROM ");
		query_builder.Append (Tablename);
		if (ArgNames != null && ArgNames.Length != 0) {
			fixed_arg_names = FixNames (ArgNames);
			AppendWhere (query_builder, WhereStatments, ArgNames, fixed_arg_names);
		}
		rows = mapper.ExecuteQuery (GetTypes, SQLiteTypes, query_builder.ToString (), fixed_arg_names, Args);
		return rows;
	}


	/// <summary>
	/// Vereinfachte Abfrage mit Rückgabe aller Spalten.
	/// </summary>
	/// <param name="Tablename"></param>
	/// <param name="WhereStatments"></param>
	/// <param name="ArgNames"></param>
	/// <param name="Args"></param>
	/// <returns></returns>
	private List<Dictionary<string, object>> SelectAllRowsWhere (string Tablename, string [] WhereStatments, string [] ArgNames, object [] Args) {
		int i;
		string [] get_fields;
		Type [] get_types;
		SQLiteTypes [] sqlite_types;
		TableSchema<SQLiteTypes> table_schema;

		if (!tableschema_cache.TryGetValue (Tablename, out table_schema)) {
			table_schema = GetTableSchema (Tablename);
			tableschema_cache.Add (Tablename, table_schema);
		}
		i = 0;
		get_fields = new string [table_schema.ColumnsCount];
		get_types = new Type [table_schema.ColumnsCount];
		sqlite_types = new SQLiteTypes [table_schema.ColumnsCount];
		foreach (ColumnSchema<SQLiteTypes> item in table_schema.TColumns.Values) {
			get_fields [i] = item.ColumnName;
			get_types [i] = item.MappingType;
			sqlite_types [i] = item.ColumnType;
			i++;
		}
		return SelectListWhere (get_fields, get_types, sqlite_types, Tablename, WhereStatments, ArgNames, Args);
	}

	
}   // class

//	namespace	2022-09-16 - 11.12.19