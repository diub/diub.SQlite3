namespace diub.Database;

public partial class SQLite3 {


	//
	// Prepare Update
	//


	public SQLiteUpdateQuery PrepareUpdateQuery<T> (string Tablename, params QueryItem [] Queries) {
		string [] where, arg_names;
		(where, arg_names) = SQLite3.QuerySetAsWhereStatements (Queries);
		return PrepareUpdateQuery<T> (Queries, Tablename, where, arg_names);
	}

	private SQLiteUpdateQuery PrepareUpdateQuery<T> (QueryItem [] Queries, string Tablename, string [] WhereStatments, string [] ArgNames) {
		int i;
		Dictionary<string, ColumnSchema<SQLiteTypes>> table_mapping;
		StringBuilder query_builder;
		string [] argument_names, setfields;
		string [] fixed_value_names, fixed_arg_names;

		table_mapping = GetTableMappings<T> (Tablename);
		argument_names = GetFieldnames<T> (table_mapping);
		setfields = GetFieldnames<T> (table_mapping);

		fixed_value_names = FixNames (setfields);
		query_builder = new StringBuilder ("UPDATE ");
		query_builder.Append (Tablename);
		query_builder.Append (" SET ");
		query_builder.Append ("\"");
		query_builder.Append (setfields [0]);
		query_builder.Append ("\"");
		query_builder.Append ("=$");
		query_builder.Append (fixed_value_names [0]);
		for (i = 1; i < setfields.Count (); i++) {
			query_builder.Append (", ");
			query_builder.Append ("\"");
			query_builder.Append (setfields [i]);
			query_builder.Append ("\"");
			query_builder.Append ("=$");
			query_builder.Append (fixed_value_names [i]);
		}
		fixed_arg_names = FixNames (ArgNames);
		AppendWhere (query_builder, WhereStatments, ArgNames, fixed_arg_names);
		return new SQLiteUpdateQuery () {
			Queries = Queries,
			Tablename = Tablename,
			TableMapping = table_mapping, Query = query_builder.ToString (), ArgumentNames = argument_names,
			FixedArgNames = fixed_arg_names, FixedValueNames = fixed_value_names
		};
	}

	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="PreparedQuery"></param>
	/// <param name="Value"></param>
	/// <param name="Args"></param>
	/// <returns></returns>
	public bool Update<T> (SQLiteUpdateQuery PreparedQuery, T Value, params object [] Args) {
		bool status;
		object [] values;
		SQLiteInsertQuery sq;

		values = GetValues<T> (PreparedQuery.TableMapping, Value);
		status = mapper.ExecuteNonQuery (PreparedQuery.Query, PreparedQuery.FixedValueNames, values, PreparedQuery.FixedArgNames,
			QuerySetAsArguments (PreparedQuery.Queries, Args));
		if (!status)
			return status;
		if (mapper.Changes () != 0)
			return status;
		sq = PrepareInsertQuery<T> (PreparedQuery.Tablename);
		return Insert<T> (sq, Value);
	}

}   // class

//	namespace	2022-09-26 - 12.11.55