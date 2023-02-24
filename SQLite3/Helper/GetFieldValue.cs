namespace diub.Database;

public partial class SQLite3 {

	//private Dictionary<string, FieldInfo> PrepareFieldValues (Type Type) {
	//	Dictionary<string, FieldInfo> fields;

	//	fields = new Dictionary<string, FieldInfo> ();
	//	PrepareFieldValues (Type, fields, "");
	//	return fields;
	//}

	//private void PrepareFieldValues (Type Type, Dictionary<string, FieldInfo> Fields, string Rootname) {
	//	string part_name;
	//	FieldInfo [] fields_infos;

	//	if (Rootname.Length != 0)
	//		Rootname += ".";
	//	fields_infos = Type.GetFields ();
	//	foreach (FieldInfo fi in fields_infos) {
	//		if (fi.FieldType.IsValueType || fi.FieldType.IsSealed) { // Ein Value-Typ hat keine Unterstruktur
	//			part_name = Rootname + fi.Name;
	//			Fields.Add (part_name, fi);
	//			continue;
	//		}
	//		PrepareFieldValues (fi.FieldType, Fields, Rootname + fi.Name);
	//	}
	//}


	/// <summary>
	/// Liefert die Werte von <paramref name="Value"/> als Name/Wert-Paare zurück.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="Value"></param>
	/// <returns></returns>
	internal Dictionary<string, object> ExtractFieldValues<T> (T Value) {
		Dictionary<string, object> fields;

		fields = new Dictionary<string, object> ();
		PrepareFieldValues (Value, fields, "");
		return fields;
	}

	/// <summary>
	/// Durchläuft rekursiv alle Felder und baut eine Name/Wert-Liste auf.
	/// </summary>
	/// <param name="Value"></param>
	/// <param name="Fields"></param>
	/// <param name="Rootname"></param>
	private void PrepareFieldValues (object Value, Dictionary<string, object> Fields, string Rootname) {
		string part_name;
		FieldInfo [] fields_infos;
		object value;

		if (Rootname.Length != 0)
			Rootname += ".";
		fields_infos = Value.GetType ().GetFields ();
		foreach (FieldInfo fi in fields_infos) {
			part_name = Rootname + fi.Name;
			value = fi.GetValue (Value);
			if (fi.FieldType.IsValueType || fi.FieldType.IsSealed) { // Ein Value-Typ hat keine Unterstruktur
				Fields.Add (part_name, value);
				continue;
			}
			PrepareFieldValues (value, Fields, part_name);
		}
	}

	//public object GetFieldValue (object Obj, string FieldName) {
	//	int i;
	//	string [] part_names;
	//	object sub_obj;
	//	Type type;
	//	FieldInfo fi;

	//	if (Obj == null)
	//		return null;

	//	part_names = FieldName.Split (new char [] { '.' }, StringSplitOptions.RemoveEmptyEntries);
	//	sub_obj = Obj;

	//	for (i = 0; i < part_names.Length; i++) {
	//		type = sub_obj.GetType ();
	//		fi = type.GetField (part_names [i]);
	//		if (fi != null)
	//			sub_obj = fi.GetValue (sub_obj);
	//		if (sub_obj == null)
	//			return null;
	//	}
	//	return sub_obj;
	//}

}   // class

//	namespace	2022-09-16 - 11.19.03