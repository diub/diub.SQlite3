namespace diub.Database;

public partial class SQLite3 {

	/// <summary>
	/// Enthält Informationen zur Handhabung von Besonderheiten.
	/// </summary>
	public class ConnectionInfo {

		const string DATETIMESQLITEDEFAULTFORMAT = "yyyy-MM-dd HH:mm:ss.fff";

		//public string UniqueKey {
		//	get; private set;
		//}

		//public string DatabasePath {
		//	get; private set;
		//}

		public bool StoreDateTimeAsTicks {
			get; private set;
		} = true;

		public bool StoreTimeSpanAsTicks {
			get; private set;
		} = true;

		public string DateTimeStringFormat {
			get; private set;
		} = DATETIMESQLITEDEFAULTFORMAT;

		public DateTimeStyles DateTimeStyle {
			get; private set;
		}

		//public object Key {
		//	get; private set;
		//} = null;

		public SQLiteOpenFlags OpenFlags {
			get; private set;
		}

		public string VfsName {
			get; private set;
		} = null;

		public ConnectionInfo (string databasePath)
				: this (databasePath, SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite) {
		}

		public ConnectionInfo (string PathFilename, SQLiteOpenFlags openFlags) {
			//UniqueKey = string.Format ("{0}_{1:X8}", PathFilename, (uint) openFlags);
			DateTimeStyle = "o".Equals (DateTimeStringFormat, StringComparison.OrdinalIgnoreCase) || "r".Equals (DateTimeStringFormat, StringComparison.OrdinalIgnoreCase) ? DateTimeStyles.RoundtripKind : DateTimeStyles.None;
			OpenFlags = openFlags;
			//DatabasePath = PathFilename;
		}

		//internal byte [] GetNullTerminatedUtf8 (string Source) {
		//	var utf8Length = System.Text.Encoding.UTF8.GetByteCount (Source);
		//	var bytes = new byte [utf8Length + 1];
		//	utf8Length = System.Text.Encoding.UTF8.GetBytes (Source, 0, Source.Length, bytes, 0);
		//	return bytes;
		//}

	}   // class

}   // class

//	namespace	2022-09-09 - 12.19.47