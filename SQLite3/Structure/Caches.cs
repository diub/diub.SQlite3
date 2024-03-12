namespace diub.Database;

public partial class SQLite3 {

	/// <summary>
	/// Zum Zwischenspeichern von <see cref="ColumnSchema{TColumnTypes}"/>, abrufbar nach Klassen-Typ (der C#-Klasse, 
	/// welche zum Erstellen der Tabelle verwendet wurde).
	/// </summary>
	internal Dictionary<Type, Dictionary<string, ColumnSchema<SQLiteTypes>>> class_mappings_cache = new Dictionary<Type, Dictionary<string, ColumnSchema<SQLiteTypes>>> ();

	/// <summary>
	/// Cache für die von einer Tabelle für die Spalten verwendeten SQLite-Typen.
	/// </summary>
	internal Dictionary<string, SQLiteTypes []> sqlite_types_cache = new Dictionary<string, SQLiteTypes []> ();

	/// <summary>
	/// Cache für die bei einer Tabelle den Spalten zugeordneten C#-Typen.
	/// </summary>
	internal Dictionary<string, Type []> target_types_cache = new Dictionary<string, Type []> ();

	/// <summary>
	/// Cache für zu einer Tabelle bereits ermittelte Spaltenchemata (<see cref="ColumnSchema{TColumnTypes}"/>).
	/// </summary>
	internal Dictionary<string, Dictionary<string, ColumnSchema<SQLiteTypes>>> table_mapping_cache = new Dictionary<string, Dictionary<string, ColumnSchema<SQLiteTypes>>> ();

	/// <summary>
	/// Cache für bereits ermittelte Tabellenschemata (<see cref="TableSchema{TColumnTypes}"/>).
	/// </summary>
	internal Dictionary<string, TableSchema<SQLiteTypes>> tableschema_cache = new Dictionary<string, TableSchema<SQLiteTypes>> ();

	/// <summary>
	/// Cache für mit <see cref="Insert{T}(string, T)"/> automatisch vordefinierte Insert-Abfragen.
	/// </summary>
	internal Dictionary<string, SQLiteQuery> insert_queries_cache = new Dictionary<string, SQLiteQuery> ();

	/// <summary>
	/// Nach einer ALTER-Abfrage müssen die Caches geleert werden!
	/// </summary>
	private void ClearCaches () {
		class_mappings_cache.Clear ();
		table_mapping_cache.Clear ();
		target_types_cache.Clear ();
		tableschema_cache.Clear ();
		insert_queries_cache.Clear ();
	}

}   // class

//	namespace	2022-09-15 - 17.40.41