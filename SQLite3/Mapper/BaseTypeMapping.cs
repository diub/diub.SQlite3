namespace diub.Database;

public partial class SQLite3 {


	static private Dictionary<Type, SQLiteTypes> CSharpToSQliteTypes = new Dictionary<Type, SQLiteTypes> () {
			{typeof (byte []), SQLiteTypes.BLOB},
			{typeof (bool), SQLiteTypes.TINYINT},
			{typeof (byte), SQLiteTypes.TINYINT},
			{typeof (Int16 ), SQLiteTypes.SMALLINT},
			{typeof (Int32 ), SQLiteTypes.INT},
			{typeof (Int64 ), SQLiteTypes.BIGINT},
			{typeof (float), SQLiteTypes.FLOAT},
			{typeof (double), SQLiteTypes.DOUBLE},
			{typeof (string), SQLiteTypes.TEXT},
			{typeof (StringBuilder), SQLiteTypes.TEXT},
			{typeof (Ansistring), SQLiteTypes.TEXT},
			{typeof (DateTime), SQLiteTypes.BIGINT},
			{typeof (TimeSpan), SQLiteTypes.BIGINT},
		};

	/// <summary>
	/// Liefert den <see cref="SQLite3.SQLiteTypes"/> zu einem C#-Typ. 
	/// Default ist <see cref="SQLiteTypes.TEXT"/>.
	/// </summary>
	/// <param name="Basetype"></param>
	/// <returns></returns>
	static public SQLiteTypes GetSQLiteType (Type Basetype) {
		if (CSharpToSQliteTypes.ContainsKey (Basetype))
			return CSharpToSQliteTypes [Basetype];
		return SQLiteTypes.TEXT;
	}

	static public Type GetCSharpType (SQLiteTypes SQLiteType) {
		switch (SQLiteType) {
			case SQLiteTypes.BOOLEAN:
				return typeof (bool);
			case SQLiteTypes.TINYINT:
				return typeof (byte);
			case SQLiteTypes.SMALLINT:
				return typeof (Int16);
			case SQLiteTypes.INT:
				return typeof (Int32);
			case SQLiteTypes.BIGINT:
				return typeof (Int64);
			case SQLiteTypes.TEXT:
			default:
				return typeof (string);
		}
	}

}   // class

//	namespace	2022-09-16 - 10.24.24