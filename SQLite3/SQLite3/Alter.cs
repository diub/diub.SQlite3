namespace diub.Database;

public partial class SQLite3 {

	//public bool AddColumn (string Tablename, Type CSType) {
	//	return AddColumn (Tablename, nameof (CSType), CSType);
	//}

	public bool AddColumn (string Tablename, string Columnname, Type CSType) {
		StringBuilder sb;
		string type;

		type = GetSQLiteType (CSType).ToString ();
		sb = new StringBuilder ();
		sb.Append ("ALTER TABLE ");
		sb.Append (Tablename);
		sb.Append (" ADD ");
		sb.Append (Columnname);
		sb.Append (" ");
		sb.Append (type);
		return mapper.ExecuteNonQuery (sb.ToString ());
	}

	public bool DropColumn (string Tablename, string Columnname) {
		StringBuilder sb;

		sb = new StringBuilder ();
		sb.Append ("ALTER TABLE ");
		sb.Append (Tablename);
		sb.Append (" DROP ");
		sb.Append (Columnname);
		return mapper.ExecuteNonQuery (sb.ToString ());
	}

	public bool RenameColumn (string Tablename, string OldColumnname, string NewColumnname) {
		StringBuilder sb;

		sb = new StringBuilder ();
		sb.Append ("ALTER TABLE ");
		sb.Append (Tablename);
		sb.Append (" RENAME COLUMN ");
		sb.Append (OldColumnname);
		sb.Append (" TO ");
		sb.Append (NewColumnname);
		return mapper.ExecuteNonQuery (sb.ToString ());
	}

	public bool RenameTable (string OldTablename, string NewTablename) {
		StringBuilder sb;

		sb = new StringBuilder ();
		sb.Append ("ALTER TABLE ");
		sb.Append (OldTablename);
		sb.Append (" RENAME TO ");
		sb.Append (NewTablename);
		return mapper.ExecuteNonQuery (sb.ToString ());
	}

}   // class

//	namespace	2022-10-27 - 15.29.10