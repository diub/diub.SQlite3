namespace diub.Database;

public partial class SQLite3 {

	/// <summary>
	/// Erstellt aus einem Klassen-Typ ein Feld-Mapping zum Anlegen einer SQLite-Tabelle.
	/// </summary>
	/// <param name="ClassType"></param>
	internal Dictionary<string, ColumnSchema<SQLiteTypes>> GetClassTypeMapping (Type ClassType) {
		int i;
		Dictionary<string, ColumnSchema<SQLiteTypes>> mappings;

		i = 0;
		if (class_mappings_cache.ContainsKey (ClassType))
			return class_mappings_cache [ClassType];
		mappings = new Dictionary<string, ColumnSchema<SQLiteTypes>> ();

		foreach (FieldInfo fi in ClassType.GetFields ()) {
			GetClassTypeMapping (mappings, fi.Name, fi.Name, fi.FieldType);
			mappings [fi.Name].Index = i;
			i++;
		}
		class_mappings_cache.Add (ClassType, mappings);
		return mappings;
	}

	private void GetClassTypeMapping (Dictionary<string, ColumnSchema<SQLiteTypes>> Mappings, string Root, string Fieldname, Type BaseType) {
		ColumnSchema<SQLiteTypes> mapping;

		if (BaseType.IsClass && !BaseType.IsSealed) {
			foreach (FieldInfo fi in BaseType.GetFields ())
				GetClassTypeMapping (Mappings, Root + "." + fi.Name, fi.Name, fi.FieldType);
			return;
		}
		mapping = new ColumnSchema<SQLiteTypes> () { ColumnName = Root, MappingType = BaseType, ColumnType = GetSQLiteType (BaseType) };
		Mappings.Add (mapping.ColumnName, mapping);
	}

}   // class


//	namespace	2022-09-15 - 16.51.56
