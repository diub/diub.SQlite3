namespace diub.Database;

public partial class SQLite3 {

	/// <summary>
	/// Speichert <paramref name="Value"/> in der Tabelle <paramref name="Tablename"/>.
	/// Bei mehrfachen Aufrufe ist <see cref="Insert{T}(SQLiteInsertQuery, T)"/> effizienter.<para></para>
	/// Die notwendige Insert-Abfrage wird automatisch erstellt und in <see cref="insert_queries_cache"/> zur 
	/// späteren Wiederverwendung gelegt.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="Tablename"></param>
	/// <param name="Value"></param>
	/// <returns></returns>
	public bool Insert<T> (string Tablename, T Value) {
		SQLiteInsertQuery<T> query;
		SQLiteQuery cached;

		if (!insert_queries_cache.TryGetValue (Tablename, out cached)) {
			query = PrepareInsertQuery<T> (Tablename);
			insert_queries_cache.Add (Tablename, query);
		} else {
			query = cached as SQLiteInsertQuery<T>;
		}
		return Insert (query, Value);
	}

	//
	// Prepare
	//

	/// <summary>
	/// Liefert eine wiederverwendbare Abfrage zur Verwendung mit <see cref="Insert{T}(SQLiteQuery, T)"/>.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="Tablename"></param>
	/// <returns></returns>
	public SQLiteInsertQuery<T> PrepareInsertQuery<T> (string Tablename) {
		Dictionary<string, ColumnSchema<SQLiteTypes>> table_mapping;
		StringBuilder query_builder;
		string [] argument_names, fixed_argument_names;
		string fixex_query, fixed_arg_line;

		table_mapping = GetTableMappings<T> (Tablename);
		argument_names = GetFieldnames<T> (table_mapping);
		(fixex_query, fixed_arg_line, fixed_argument_names) = FixArguments (argument_names);

		query_builder = new StringBuilder ();
		query_builder.Append ("INSERT INTO ");
		query_builder.Append (Tablename);
		query_builder.Append (" (");
		query_builder.Append (fixex_query);
		query_builder.Append (") VALUES (");
		query_builder.Append (fixed_arg_line);
		query_builder.Append (")");
		return new SQLiteInsertQuery<T> (this) {
			Tablename = Tablename,
			TableMapping = table_mapping, Query = query_builder.ToString (), ArgumentNames = argument_names,
			FixedArgNames = fixed_argument_names
		};
	}

	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="PreparedQuery">Eine vorbereitete Abfrage.</param>
	/// <param name="Value">Das zu speichernde Objekt.</param>
	/// <returns></returns>
	public bool Insert<T> (SQLiteInsertQuery<T> PreparedQuery, T Value) {
		object [] values;

		values = GetValues<T> (PreparedQuery.TableMapping, Value);
		return mapper.ExecuteNonQuery (PreparedQuery.Query, PreparedQuery.FixedArgNames, values);
	}

}   // class

//	namespace	2022-09-16 - 11.39.24