namespace diub.Database;

public partial class SQLite3 {

	public dynamic Create (Type CreateType) {
		try {
			if (CreateType == typeof (string))
				return "";
			return Activator.CreateInstance (CreateType);
		} catch (Exception) {
			return null;    // nur Nicht-Primitiven kann eine Konstruktor ohne Parameter fehlen!
		}
	}

}   // class

//	namespace	2022-09-16 - 11.17.01