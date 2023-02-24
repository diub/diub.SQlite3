using System.ComponentModel;

namespace diub.Database;

public partial class SQLite3 {

	private void SetFieldValue (object Obj, string Fieldname, object Value) {
		int i;
		string part;
		string [] part_names;
		object sub_obj, created;
		Type type;
		FieldInfo fi;
		TypeConverter tc;

		fi = null;
		part_names = Fieldname.Split ('.');
		// Das muss so sein für Felder direkt/oben im Objekt.
		sub_obj = Obj;
		type = Obj.GetType ();
		// Tieferliegendes Feld ?
		for (i = 0; i < part_names.Length - 1; i++) {
			fi = type.GetField (part_names [i]);
			if (fi.FieldType.IsValueType)   // Ein Value-Typ hat keine Unterstruktur,
				break;                      // daher muss dies schon der letzte Teil-Name sein!
			created = fi.GetValue (sub_obj);
			if (created == null) {
				created = Create (fi.FieldType);
				fi.SetValue (sub_obj, created);
			}
			sub_obj = created;
			type = sub_obj.GetType ();
		}
		// Das muss so sein für Felder direkt/oben im Objekt.
		fi = type.GetField (part_names [part_names.Length - 1]);
		if (fi != null) {
			try {
				fi.SetValue (sub_obj, Value);
			} catch (Exception) {
				try {
					tc = System.ComponentModel.TypeDescriptor.GetConverter (fi.FieldType);
					try {
						fi.SetValue (sub_obj, tc.ConvertFrom (Value));
					} catch (Exception) {
						// Für die Darstellung von Enumerations-Werten inkl. Typ als "string":
						// diub.biz.Accountancy.LedgerTypes:Cash
						// Ignoriert die mitgespeicherte Typ-Angabe!
						// For displaying enumeration values incl. type as "string":
						// diub.biz.Accountancy.LedgerTypes:Cash
						// Ignores the stored type specification!
						part = Value.ToString ();
						i = part.LastIndexOf (':');
						if (i > 0) {
							part = part.Substring (i + 1);
							fi.SetValue (sub_obj, tc.ConvertFrom (part));
						}
					}
				} catch (Exception) { }
			}
		}
	}

}   // class

//	namespace	2022-09-16 - 11.19.03