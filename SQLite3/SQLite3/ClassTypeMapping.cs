namespace diub.Database;

public partial class SQLite3 {

	/// <summary>
	/// Erstellt aus einem Klassen-Typ ein Feld-Mapping zum Anlegen einer SQLite-Tabelle.
	/// </summary>
	/// <param name="ClassType"></param>
	internal Dictionary<string, ColumnSchema<SQLiteTypes>> GetClassTypeMapping (Type ClassType) {
		int index;
		Dictionary<string, ColumnSchema<SQLiteTypes>> mappings;

		index = 0;
		if (class_mappings_cache.ContainsKey (ClassType))
			return class_mappings_cache [ClassType];
		mappings = new Dictionary<string, ColumnSchema<SQLiteTypes>> ();

		foreach (FieldInfo fi in ClassType.GetFields ()) {
			GetClassTypeMapping (mappings, fi.Name, fi.Name, fi.FieldType, ref index);
			//mappings [fi.Name].Index = index;
			//index++;
		}
		class_mappings_cache.Add (ClassType, mappings);
		return mappings;
	}
	
	//private void EnumerateMapping () {
	//	int i;

	//	foreach (FieldInfo fi in ClassType.GetFields ()) {
	//		GetClassTypeMapping (mappings, fi.Name, fi.Name, fi.FieldType);
	//		if (mappings.ContainsKey (fi.Name))
	//			mappings [fi.Name].Index = i;
	//		i++;
	//	}
	//}

	private void GetClassTypeMapping (Dictionary<string, ColumnSchema<SQLiteTypes>> Mappings, string Root, string Fieldname, Type BaseType, ref int Index) {
		ColumnSchema<SQLiteTypes> mapping;

		if (BaseType.IsClass && !BaseType.IsSealed) {
			foreach (FieldInfo fi in BaseType.GetFields ())
				GetClassTypeMapping (Mappings, Root + "." + fi.Name, fi.Name, fi.FieldType, ref Index);
			return;
		}
		mapping = new ColumnSchema<SQLiteTypes> () { ColumnName = Root, MappingType = BaseType, ColumnType = GetSQLiteType (BaseType) };
		mapping.Index = Index;
		Mappings.Add (mapping.ColumnName, mapping);
		Index++;
	}

}   // class


//	namespace	2022-09-15 - 16.51.56
