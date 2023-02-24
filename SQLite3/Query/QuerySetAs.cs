namespace diub.Database;

public partial class SQLite3 {

	/// <summary>
	/// Passt auch die Argumente an 'LIKE' an.
	/// </summary>
	/// <param name="Queries"></param>
	/// <returns></returns>
	/// <exception cref="Exception"></exception>
	static public (string [] Where, string [] ArgNames, object [] Values) QuerySetAsSQLiteStatements (params QueryItem [] Queries) {
		int i;
		string [] where, arg_names;
		object [] values;

		if (Queries == null || Queries.Length == 0)
			return (null, null, null);
		where = new string [Queries.Length];
		arg_names = new string [Queries.Length];
		values = new object [Queries.Length];
		for (i = 0; i < Queries.Length; i++) {
			arg_names [i] = Queries [i].ColumnName;
			values [i] = Queries [i].Value;
			switch (Queries [i].CompareType) {
				case QueryCompareType.Equal:
					where [i] = "=";
					break;
				case QueryCompareType.Unequal:
					where [i] = "!=";
					break;
				case QueryCompareType.LessThen:
					where [i] = "<";
					break;
				case QueryCompareType.LessThanOrEqual:
					where [i] = "<=";
					break;
				case QueryCompareType.GreaterThan:
					where [i] = ">";
					break;
				case QueryCompareType.GreaterThanOrEqual:
					where [i] = ">=";
					break;
				case QueryCompareType.Like:
					where [i] = " LIKE ";
					arg_names [i] = "%" + Queries [i].Value + "%";
					break;
				case QueryCompareType.Unlike:
					where [i] = " NOT LIKE ";
					values [i] = "%" + Queries [i].Value + "%";
					break;
				case QueryCompareType.StartsWith:
					where [i] = "  LIKE ";  // Zusätzliches Leerzeichen für eventuelle Fallunterscheidung.
					values [i] = Queries [i].Value + "%";
					break;
				case QueryCompareType.EndsWith:
					where [i] = " LIKE  ";  // Zusätzliches Leerzeichen für eventuelle Fallunterscheidung.
					values [i] = "%" + Queries [i].Value;
					break;
				default:
					throw new Exception ("Unsupported compare type: " + Queries [i].CompareType.ToString ());
			}
		}
		return (where, arg_names, values);
	}

	/// <summary>
	/// Liefert 'WhereStaments' und Argumentnamen für <see cref="SQLiteQuery"/>. 
	/// Wichtig: Die Anpassung der Argumente an 'LIKE' muss separat bei Ausführung der Abfrage erfolgen!
	/// </summary>
	/// <param name="Queries"></param>
	/// <returns></returns>
	/// <exception cref="Exception"></exception>
	static public (string [] Where, string [] ArgNames) QuerySetAsWhereStatements (params QueryItem [] Queries) {
		int i;
		string [] where, arg_names;

		where = new string [Queries.Length];
		arg_names = new string [Queries.Length];
		for (i = 0; i < Queries.Length; i++) {
			arg_names [i] = Queries [i].ColumnName;
			switch (Queries [i].CompareType) {
				case QueryCompareType.Equal:
					where [i] = "=";
					break;
				case QueryCompareType.Unequal:
					where [i] = "!=";
					break;
				case QueryCompareType.LessThen:
					where [i] = "<";
					break;
				case QueryCompareType.LessThanOrEqual:
					where [i] = "<=";
					break;
				case QueryCompareType.GreaterThan:
					where [i] = ">";
					break;
				case QueryCompareType.GreaterThanOrEqual:
					where [i] = ">=";
					break;
				case QueryCompareType.Like:
					where [i] = " LIKE ";
					break;
				case QueryCompareType.Unlike:
					where [i] = " NOT LIKE ";
					break;
				case QueryCompareType.StartsWith:
					where [i] = "  LIKE ";  // Zusätzliches Leerzeichen für eventuelle Fallunterscheidung.
					break;
				case QueryCompareType.EndsWith:
					where [i] = " LIKE  ";  // Zusätzliches Leerzeichen für eventuelle Fallunterscheidung.
					break;
				default:
					throw new Exception ("Unsupported compare type: " + Queries [i].CompareType.ToString ());
			}
		}
		return (where, arg_names);
	}

	/// <summary>
	/// Passt die Argumente an 'LIKE' an.
	/// </summary>
	/// <param name="Queries"></param>
	/// <param name="Args"></param>
	/// <returns></returns>
	static public object [] QuerySetAsArguments (QueryItem [] Queries, object [] Args) {
		int i;
		object [] values;

		values = new object [Args.Length];
		for (i = 0; i < Args.Length; i++) {
			switch (Queries [i].CompareType) {
				default:
					values [i] = Args [i];
					break;
				case QueryCompareType.Like:
					values [i] = "%" + Args [i] + "%";
					break;
				case QueryCompareType.Unequal:
					values [i] = "%" + Args [i] + "%";
					break;
				case QueryCompareType.StartsWith:
					values [i] = Args [i] + "%";
					break;
				case QueryCompareType.EndsWith:
					values [i] = "%" + Args [i];
					break;
			}
		}
		return (values);
	}

}   // class

//	namespace	2022-11-20 - 14.25.28
