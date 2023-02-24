namespace diub.Database;

public partial class SQLite3 {

	static protected class Firm {

		readonly static public string [] ALL_FIELDS = null; // das hier funktioniert so nicht: new string [] { "*" };

		readonly static public string [] COUNT_FIELDS = new string [] { "COUNT(*)" };

	}

}   // class

//	namespace	2022-09-26 - 10.26.12