namespace diub.Database;

public partial class SQLite3 {

	/// <summary>
	/// Erstellt aus einem Klassen-Typ und dem Tabellen-Schema ein Feld-Mapping zum Auslesen einer SQLite-Datenzeile .
	/// </summary>
	/// <param name="ClassType"></param>
	internal Dictionary<string, ColumnSchema<SQLiteTypes>> GetTableMappings<T> (string Tablename) {
		string query, mapping_cache_keyname;
		Dictionary<string, ColumnSchema<SQLiteTypes>> mapping_list;
		Dictionary<string, ColumnSchema<SQLiteTypes>> type_mappings;
		Type [] types;
		List<Dictionary<string, object>> rows;
		ColumnSchema<SQLiteTypes> mapping, type_mapping;

		mapping_cache_keyname = Tablename + typeof (T).Name;
		if (table_mapping_cache.TryGetValue (mapping_cache_keyname, out mapping_list))
			return mapping_list;

		types = new Type [] { typeof (Ansistring  ), typeof (Ansistring  ), typeof (Ansistring  ), typeof (Ansistring  ), typeof (int), typeof (int) };
		query = "PRAGMA table_info (" + Tablename + ")";
		rows = mapper.ExecuteQuery (types, null, query, null, null);
		if (rows == null || rows.Count == 0)
			return null;
		type_mappings = GetClassTypeMapping (typeof (T));
		mapping_list = new Dictionary<string, ColumnSchema<SQLiteTypes>> ();
		foreach (Dictionary<string, object> row in rows) {
			mapping = new  ColumnSchema<SQLiteTypes> () { ColumnName = row ["name"] as string };
			mapping.ColumnType = (NativeMapper.SQLiteTypes) Enum.Parse (typeof (NativeMapper.SQLiteTypes), row ["type"] as string);
			if (type_mappings.TryGetValue (mapping.ColumnName, out type_mapping))
				mapping.MappingType = type_mapping.MappingType;
			else
				mapping.MappingType = null;  // markieren für: Spalte überspringen da nicht zuordbar.
			mapping_list.Add (mapping.ColumnName, mapping);

		}
		table_mapping_cache.Add (mapping_cache_keyname, mapping_list);
		return mapping_list;
	}

	internal Type [] GetTypesFromMapping (Dictionary<string, ColumnSchema<SQLiteTypes>> Mappings) {
		int i;
		Type [] types;

		i = 0;
		types = new Type [Mappings.Count];
		foreach (ColumnSchema<SQLiteTypes> mapping in Mappings.Values)
			types [i++] = mapping.MappingType;
		return types;
	}

	internal (Type [], SQLiteTypes []) GetTypesFromMapping<T> (string Tablename) {
		Dictionary<string, ColumnSchema<SQLiteTypes>> table_mappings;
		int i;
		Type [] types;
		SQLiteTypes [] sqlites;

		if (target_types_cache.TryGetValue (Tablename, out types))
			return (types, sqlite_types_cache [Tablename]);
		//if (target_types_cache.ContainsKey (Tablename))
		//	return (target_types_cache [Tablename], sqlite_types_cache [Tablename]);
		table_mappings = GetTableMappings<T> (Tablename);
		i = 0;
		types = new Type [table_mappings.Count];
		sqlites = new SQLiteTypes [table_mappings.Count];
		foreach (ColumnSchema<SQLiteTypes> mapping in table_mappings.Values) {
			sqlites [i] = mapping.ColumnType;
			types [i++] = mapping.MappingType;
		}
		return (types, sqlites);
	}

	/// <summary>
	/// Holt die einzelnen Name/Wert-Paare in getrennten Arrays für Name und Wert.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="Tablename"></param>
	/// <param name="Value"></param>
	/// <returns></returns>
	internal (string [], object []) GetFieldnamesValues<T> (string Tablename, T Value) {
		int i;
		string [] arg_names;
		object [] values;
		Dictionary<string, ColumnSchema<SQLiteTypes>> table_mapping;
		Dictionary<string, object> value_map;

		table_mapping = GetTableMappings<T> (Tablename);
		arg_names = new string [table_mapping.Count];
		values = new object [table_mapping.Count];
		i = 0;
		value_map = ExtractFieldValues<T> (Value);
		foreach (ColumnSchema<SQLiteTypes> mapping in table_mapping.Values) {
			arg_names [i] = mapping.ColumnName;
			value_map.TryGetValue (arg_names [i], out values [i]);   // Falls die Klasse kein Feld besitzt, das sich der Spalte zuordnen lässt!
			i++;
		}
		return (arg_names, values);
	}

	internal string [] GetFieldnames<T> (Dictionary<string, ColumnSchema<SQLiteTypes>> TableMapping) {
		int i;
		string [] arg_names;

		arg_names = new string [TableMapping.Count];
		i = 0;
		foreach (ColumnSchema<SQLiteTypes> mapping in TableMapping.Values) {
			arg_names [i] = mapping.ColumnName;
			i++;
		}
		return arg_names;
	}

	internal object [] GetValues<T> (Dictionary<string, ColumnSchema<SQLiteTypes>> TableMapping, T Value) {
		int i;
		object [] values;
		Dictionary<string, object> value_map;

		values = new object [TableMapping.Count];
		i = 0;
		value_map = ExtractFieldValues<T> (Value);
		foreach (ColumnSchema<SQLiteTypes> mapping in TableMapping.Values) {
			value_map.TryGetValue (mapping.ColumnName, out values [i]);   // Falls die Klasse kein Feld besitzt, das sich der Spalte zuordnen lässt!
			i++;
		}
		return values;
	}


}   // class


//	namespace	2022-09-15 - 16.51.56
