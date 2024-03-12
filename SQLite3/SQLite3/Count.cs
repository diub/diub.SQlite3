using System.Collections.Generic;

namespace diub.Database;

public partial class SQLite3 {

	public int Count (string Tablename) {
		SQLiteCountQuery query;

		query = PrepareCountQuery (Tablename);
		return Count (query);
	}

	public SQLiteCountQuery PrepareCountQuery (string Tablename, params QueryItem [] Queries) {
		string [] where, arg_names;
		(where, arg_names) = SQLite3.QuerySetAsWhereStatements (Queries);
		return PrepareCountQuery (Tablename, where, arg_names);
	}

	/// <summary>
	/// Nur zur internen Verwendung.
	/// </summary>
	/// <param name="Tablename"></param>
	/// <param name="WhereStatments"></param>
	/// <param name="ArgNames"></param>
	/// <returns></returns>
	private SQLiteCountQuery PrepareCountQuery (string Tablename, string [] WhereStatments, string [] ArgNames) {
		string [] fixed_arg_names;
		StringBuilder query;

		query = new StringBuilder ();
		query.Append ("SELECT COUNT(*) FROM ");
		query.Append (Tablename);
		fixed_arg_names = null;
		if (ArgNames != null && ArgNames.Length != 0) {
			fixed_arg_names = FixNames (ArgNames);
			AppendWhere (query, WhereStatments, ArgNames, fixed_arg_names);
		}
		return new SQLiteCountQuery (this) { Tablename = Tablename, Query = query.ToString (), FixedArgNames = fixed_arg_names };
	}

	public int Count (SQLiteCountQuery PreparedQuery, params object [] Args) {
		int i;
		List<Dictionary<string, object>> list;

		list = mapper.ExecuteQuery (new Type [] { typeof (int) }, null, PreparedQuery.Query, PreparedQuery.FixedArgNames, Args);
		if (list == null || list.Count == 0)
			return -1;
		i = (int) list [0].First ().Value;
		return i;
	}

}   // class

//	namespace	2022-09-23 - 15.05.39
