namespace diub.Database;

public partial class SQLite3 {

#if DEBUG
	static public void ConcurrentTest () {
		int i;
		SQLite3 sqlite;
		TESTCLASS test;
		SUPERCLASS super, upd, back;
		SQLiteSelectQuery<SUPERCLASS> select_query;
		SQLiteUpdateQuery<SUPERCLASS> update_query;

		System.IO.File.Delete (PATH_FILENAME);

		sqlite = new SQLite3 (PATH_FILENAME);
		sqlite.Connect ();

		sqlite.CreateTable (TABLENAME, typeof (SUPERCLASS));
		sqlite.BeginTransaction ();

		// Daten schreiben
		for (i = 0; i < 10; i++) {
			super = new SUPERCLASS () { SuperName = "SuperDummy-" + i.ToString (), SuperCount = i + 100 };
			super.testclass = new TESTCLASS () { count = i, name = "TestDummy-" + i.ToString () };
			sqlite.Insert<SUPERCLASS> (TABLENAME, super);
		}

		// Ohne Commit lesen und ändern
		select_query = sqlite.PrepareSelectQuery<SUPERCLASS> (TABLENAME, new QueryItem ("SuperCount", QueryCompareType.Equal));
		upd = select_query.SelectOne (5 + 100);
		upd.SuperName = "Changed!";

		// Update ohne Commit
		update_query = sqlite.PrepareUpdateQuery<SUPERCLASS> (TABLENAME, new QueryItem (nameof (SUPERCLASS.SuperCount), QueryCompareType.Equal));
		update_query.UpdateIfExist (upd, 5 + 100);

		// Ohne Commit prüfen!
		back = select_query.SelectOne (5 + 100);
		if (back.SuperName != "Changed!") {
			// Failure
		}

		sqlite.Commit ();
	}
#endif


}   // class

//	namespace	2024-03-14 - 12.09.03