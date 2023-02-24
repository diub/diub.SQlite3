namespace diub.Database;

public partial class SQLite3 {

	private void AppendWhere (StringBuilder query, string [] WhereStatments, string [] ArgNames, string [] FixedArgNames) {
		int i;

		query.Append (" WHERE \"");		// minimale Optimierung ;-)
		//query.Append ('\"');
		query.Append (ArgNames [0]);
		query.Append ('\"');
		query.Append (WhereStatments [0]);
		query.Append ("@");
		query.Append (FixedArgNames [0]);
		for (i = 1; i < ArgNames.Length; i++) {
			query.Append (" AND ");
			query.Append ('\"');
			query.Append (ArgNames [i]);
			query.Append ('\"');
			query.Append (WhereStatments [i]);
			query.Append ("@");
			query.Append (FixedArgNames [i]);
		}
	}

}   // class

//	namespace	2022-09-28 - 15.49.20
