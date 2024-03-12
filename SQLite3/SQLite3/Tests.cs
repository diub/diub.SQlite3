namespace diub.Database;

public partial class SQLite3 {

#if DEBUG

	static string PATH_FILENAME = @"Y:\TEMP\SQLite3-Test.sql";

	static string TABLENAME = "DUMMY";

	public class TESTCLASS {
		public Int32 count;
		public string name;
	}

	public class REVERSECLASS {
		public string name;
		public Int32 count;
	}

	public class SUPERCLASS {
		public Int32 SuperCount;
		public string SuperName;
		public TESTCLASS testclass;
	}

	public class BRACKENCLASS {
		public Int32 BrackenCount;
		public string BrackenName;
		public TESTCLASS testclass;
	}

	static public void Tests () {
		int i;
		SQLite3 sqlite;
		TESTCLASS test;
		SUPERCLASS super;
		BRACKENCLASS bracken;
		SQLiteSelectQuery<SUPERCLASS> select_query;
		SQLiteInsertQuery<BRACKENCLASS> insert_query;
		SQLiteUpdateQuery<SUPERCLASS> update_query;

		System.IO.File.Delete (PATH_FILENAME);

		sqlite = new SQLite3 (PATH_FILENAME);
		sqlite.Connect ();

		sqlite.CreateTable (TABLENAME, typeof (SUPERCLASS));

		for (i = 0; i < 10; i++) {
			super = new SUPERCLASS () { SuperName = "SuperDummy-" + i.ToString (), SuperCount = i + 100 };
			super.testclass = new TESTCLASS () { count = i, name = "TestDummy-" + i.ToString () };
			sqlite.Insert<SUPERCLASS> (TABLENAME, super);
		}

		insert_query = sqlite.PrepareInsertQuery<BRACKENCLASS> (TABLENAME);
		bracken = null;
		for (i = 100; i < 110; i++) {
			bracken = new BRACKENCLASS () { BrackenName = "BrackenDummy-" + i.ToString (), BrackenCount = i + 100 };
			bracken.testclass = new TESTCLASS () { count = i, name = "TestDummy-" + i.ToString () };
			//sqlite.Store<BRACKENCLASS> (TABLENAME, bracken);
			sqlite.Insert<BRACKENCLASS> (insert_query, bracken);
		}
		bracken.testclass.count = 1000;
		bracken.testclass.name = "BrackenDummy-1000";
		//sqlite.Update<BRACKENCLASS> (TABLENAME, bracken, "=", "testclass.count", 100);

		select_query = sqlite.PrepareSelectQuery<SUPERCLASS> (TABLENAME, new QueryItem ("SuperCount", QueryCompareType.Equal));
		//var l1 = sqlite.SelectListWhere<SUPERCLASS> (TABLENAME, "=", "SuperCount", 100);
		var l3 = sqlite.SelectList<SUPERCLASS> (select_query, 100);

		//var l4 = sqlite.SelectListWhere<BRACKENCLASS> (TABLENAME);

		//sqlite.Remove (TABLENAME , "=", "SuperCount", 100);
		//sqlite.Remove (TABLENAME , "=", "testclass.count", 1);

		//var v = sqlite.Count (TABLENAME + "S");

		super = new SUPERCLASS () { SuperName = "SuperDummy-X", SuperCount = 1000 };
		super.testclass = new TESTCLASS () { count = 1000, name = "TestDummy-X" };
		//sqlite.Update<SUPERCLASS> (TABLENAME, super, "=", "SuperCount", 100);

		//update_query = sqlite.PrepareUpdateQuery<SUPERCLASS> (TABLENAME, "=", "SuperCount");
		super = new SUPERCLASS () { SuperName = "SuperDummy-XY", SuperCount = 2000 };
		super.testclass = new TESTCLASS () { count = 2000, name = "TestDummy-XY" };
		//sqlite.Update<SUPERCLASS> (update_query, super, 101);

		sqlite.Disconnect ();


		sqlite = new SQLite3 (PATH_FILENAME);
		sqlite.Connect ();
		sqlite.Compact ();

		//sqlite.Update<SUPERCLASS> (TABLENAME , super, "=", "SuperName", "SuperDummy-0");

		sqlite.Disconnect ();
	}

#endif

}   // class

//	namespace	2022-09-16 - 12.11.52