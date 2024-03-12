namespace diub.Database;

public partial class SQLite3 {

	/// <summary>
	/// Das Schema muss der realen Tabelle in der Datenbank entsprechen. 
	/// </summary>
	/// <param name="Tablename"></param>
	/// <param name="Row"></param>
	/// <returns></returns>
	public bool InsertDataRow (string Tablename, DataRow Row) {
		SQLiteInsertQuery query;

		query = PrepareInsertQuery (Tablename, Row.Table);
		return Insert (query, Row);
	}

	/// <summary>
	/// Das Schema muss der realen Tabelle in der Datenbank entsprechen. 
	/// </summary>
	/// <param name="Tablename"></param>
	/// <param name="Table"></param>
	/// <returns></returns>
	public bool InsertDataTable (string Tablename, DataTable Table) {
		bool status;
		SQLiteInsertQuery query;

		status = true;
		query = PrepareInsertQuery (Tablename, Table);
		BeginTransaction ();
		foreach (DataRow row in Table.Rows)
			status = status && Insert (query, row);
		Commit ();
		return status;
	}

	/// <summary>
	/// Das Schema muss der realen Tabelle in der Datenbank entsprechen. 
	/// </summary>
	/// <param name="Tablename"></param>
	/// <param name="Row"></param>
	/// <returns></returns>
	public SQLiteInsertQuery PrepareInsertQuery (string Tablename, DataTable Table) {
		int i;
		string fixed_query, fixed_arg_line;
		StringBuilder query_builder;
		string [] argument_names;
		string [] fixed_argument_names;
		TableSchema<SQLiteTypes> table_schema;
		Dictionary<string, ColumnSchema<SQLiteTypes>> table_columns;

		if (!tableschema_cache.TryGetValue (Tablename, out table_schema)) {
			table_schema = GetTableSchema (Tablename);
			tableschema_cache.Add (Tablename, table_schema);
		}
		i = 0;
		table_columns = table_schema.TColumns;
		argument_names = new string [table_columns.Count];
		foreach (ColumnSchema<SQLiteTypes> row_column in table_columns.Values)
			argument_names [i++] = row_column.ColumnName;

		(fixed_query, fixed_arg_line, fixed_argument_names) = FixArguments (argument_names);

		query_builder = new StringBuilder ();
		query_builder.Append ("INSERT INTO ");
		query_builder.Append (Tablename);
		query_builder.Append (" (");
		query_builder.Append (fixed_query);
		query_builder.Append (") VALUES (");
		query_builder.Append (fixed_arg_line);
		query_builder.Append (")");

		return new SQLiteInsertQuery (this) {
			Tablename = Tablename,
			TableMapping = table_schema.TColumns,
			Query = query_builder.ToString (), ArgumentNames = argument_names,
			FixedArgNames = fixed_argument_names
		};
	}

	public bool Insert (SQLiteInsertQuery PreparedQuery, DataRow Row) {
		int i;
		object [] values;

		i = 0;
		values = new object [PreparedQuery.FixedArgNames.Length];
		foreach (ColumnSchema<SQLiteTypes> column in PreparedQuery.TableMapping.Values)
			values [i++] = Row [column.ColumnName];
		return mapper.ExecuteNonQuery (PreparedQuery.Query, PreparedQuery.FixedArgNames, values);
	}


}   // class

//	namespace	2022-11-21 - 14.24.23