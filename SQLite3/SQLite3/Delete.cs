namespace diub.Database;

public partial class SQLite3 {

	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="Tablename"></param>
	/// <param name="Queries"></param>
	/// <returns></returns>
	public SQLiteDeleteQuery PrepareDeleteQuery<T> (string Tablename, params QueryItem [] Queries) {
		string [] where, arg_names;
		(where, arg_names) = SQLite3.QuerySetAsWhereStatements (Queries);
		return PrepareDeleteQuery<T> (Queries, Tablename, where, arg_names);
	}

	private SQLiteDeleteQuery PrepareDeleteQuery<T> (QueryItem [] Queries, string Tablename, string [] WhereStatments, string [] ArgNames) {
		StringBuilder query;
		string [] fixed_arg_names;

		fixed_arg_names = FixNames (ArgNames);
		query = new StringBuilder ("DELETE FROM ");
		query.Append (Tablename);
		AppendWhere (query, WhereStatments, ArgNames, fixed_arg_names);
		return new SQLiteDeleteQuery (this) {
			Queries = Queries,
			Tablename = Tablename, Query = query.ToString (), FixedArgNames = fixed_arg_names
		};
	}

	public bool Delete (SQLiteDeleteQuery PreparedQuery, params object [] Args) {
		return mapper.ExecuteNonQuery (PreparedQuery.Query, PreparedQuery.FixedArgNames, 
			QuerySetAsArguments (PreparedQuery.Queries, Args));
	}

}   // class

//	namespace	2022-09-28 - 12.56.54