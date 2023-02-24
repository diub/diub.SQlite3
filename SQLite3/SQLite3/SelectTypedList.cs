namespace diub.Database;

public partial class SQLite3 {

	/// <summary>
	/// Nur zur internen Verwendung: Die Basis-Funktion für Typ-gebundene Abfragen.<para></para> 
	/// Liefert eine Liste mit Objekten vom Typ <typeparamref name="T"/> welche den Suchkriterien entsprechen.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="GetTypes">Die C#-Typen der einzelnen Felder der Ziel-Klasse in der Reihenfolge ihres Auftretens!</param>
	/// <param name="Tablename"></param>
	/// <param name="WhereStatment">SQLite3 Vergleichsoperator: =, !=, <, <=, >=, >, like</param>
	/// <param name="ArgNames">Spaltennamen oder NULL für Abfrage ohne WHERE Statement.</param>
	/// <param name="Args">Vergleichswerte oder NULL, falls <paramref name="ArgName"/> == NULL.</param>
	/// <returns></returns>
	protected List<T> SelectListWhere<T> (string [] GetFields, Type [] GetTypes, SQLiteTypes [] SQLiteTypes, string Tablename, string [] WhereStatments, string [] ArgNames, object [] Args) {
		List<Dictionary<string, object>> rows;
		T value;
		List<T> list;
		Type value_type;

		value_type = typeof (T);
		list = new List<T> ();
		rows = SelectListWhere (GetFields, GetTypes, SQLiteTypes, Tablename, WhereStatments, ArgNames, Args);
		if (value_type.IsClass && !value_type.IsSealed) {
			foreach (Dictionary<string, object> row in rows) {
				value = Create (value_type);
				foreach (string key in row.Keys)
					SetFieldValue (value, key, row [key]);
				list.Add (value);
			}
		} else {
			foreach (Dictionary<string, object> row in rows) {
				value = (T) row.Values.First ();
				list.Add (value);
			}
		}
		return list;
	}


	public SQLiteSelectQuery PrepareSelectQuery<T> (string Tablename, params QueryItem [] Queries) {
		string [] where, arg_names;
		(where, arg_names) = SQLite3.QuerySetAsWhereStatements (Queries);
		return PrepareSelectQuery<T> (Queries, null, Tablename, where, arg_names);
	}

	/// <summary>
	/// Nur zur internen Verwendung. Generiert eine mehrfach verwendbare Abfrage.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="GetFields">NULL für (*).</param>
	/// <param name="Tablename"></param>
	/// <param name="WhereStatments"></param>
	/// <param name="ArgNames"></param>
	/// <returns></returns>
	private SQLiteSelectQuery PrepareSelectQuery<T> (QueryItem [] Queries, string [] GetFields, string Tablename, string [] WhereStatments = null, string [] ArgNames = null) {
		int i;
		string query;
		StringBuilder query_builder;
		string [] fixed_arg_names;
		Type [] target_types;
		SQLiteTypes [] sqlite_types;
		SQLiteSelectQuery cache_item;

		(target_types, sqlite_types) = GetTypesFromMapping<T> (Tablename);
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
		query = query_builder.ToString ();
		cache_item = new SQLiteSelectQuery () {
			Queries = Queries,
			Tablename = Tablename,
			Query = query, TargetTypes = target_types, SQLiteTypes = sqlite_types,
			FixedArgNames = fixed_arg_names
		};
		return cache_item;
	}


	/// <summary>
	/// Schnellste Variante, wenn <paramref name="PreparedQuery"/> nur einmal erzeugt wird.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="PreparedQuery"></param>
	/// <param name="Args"></param>
	/// <returns></returns>
	/// <exception cref="ArgumentException"></exception>
	public List<T> SelectList<T> (SQLiteSelectQuery PreparedQuery, params object [] Args) {
		List<Dictionary<string, object>> rows;
		T value;
		List<T> list;
		Type value_type;

		rows = mapper.ExecuteQuery (PreparedQuery.TargetTypes, PreparedQuery.SQLiteTypes, PreparedQuery.Query,
			PreparedQuery.FixedArgNames, QuerySetAsArguments (PreparedQuery.Queries, Args));
		value_type = typeof (T);
		list = new List<T> ();
		if (value_type.IsClass && !value_type.IsSealed) {
			foreach (Dictionary<string, object> row in rows) {
				value = Create (value_type);
				foreach (string key in row.Keys)
					SetFieldValue (value, key, row [key]);
				list.Add (value);
			}
		} else {
			foreach (Dictionary<string, object> row in rows) {
				value = (T) row.Values.First ();
				list.Add (value);
			}
		}
		return list;
	}

	public T SelectOne<T> (SQLiteSelectQuery PreparedQuery, params object [] Args) {
		List<T> list;

		list = SelectList<T> (PreparedQuery, Args);
		if (list.Count == 0)
			return default (T);
		return list [0];
	}


	//
	// Ohne Prepare...
	//


	/// <summary>
	/// Variante mit der einfachsten Nutzung.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="Tablename"></param>
	/// <param name="Queries"></param>
	/// <returns></returns>
	public List<T> QueryList<T> (string Tablename, params QueryItem [] Queries) {
		string [] where, arg_names;
		List<Dictionary<string, object>> rows;
		object [] args;
		List<T> list;
		TableSchema<SQLiteTypes> table_schema;
		Type value_type;
		T value;

		(where, arg_names, args) = QuerySetAsSQLiteStatements (Queries);
		rows = SelectAllRowsWhere (Tablename, where, arg_names, args);
		table_schema = tableschema_cache [Tablename];
		value_type = typeof (T);
		list = new List<T> ();
		foreach (Dictionary<string, object> row in rows) {
			value = Create (value_type);
			foreach (string key in row.Keys) 
				SetFieldValue (value, key, row [key]);
			list.Add (value);
		}
		return list;
	}


	public T QueryOne<T> (string Tablename, params QueryItem [] Queries) {
		List<T> list;

		list = QueryList<T> (Tablename, Queries);
		if (list.Count == 0)
			return default (T);
		return list [0];
	}

}   // class

//	namespace	2022-11-30 - 12.29.01