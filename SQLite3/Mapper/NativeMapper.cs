using System.Reflection;

namespace diub.Database;

public partial class SQLite3 {

	/// <summary>
	/// Bildet SQL Abfragen und andere Funktionen auf die native SQLite-Schnittstelle ab.
	/// </summary>
	public partial class NativeMapper {

		readonly static private IntPtr neg_ptr = new IntPtr (-1);

		internal Sqlite3DatabaseHandle db_handle;

		private ConnectionInfo connection_info;

		public NativeMapper (ConnectionInfo ConnectionInfo) {
			connection_info = ConnectionInfo;
		}

		public SQLiteResult GetResult (Sqlite3DatabaseHandle db) {
			return (SQLiteResult) SQLite3Native.GetResult (db);
		}

		public ExtendedResult ExtendedErrCode (Sqlite3DatabaseHandle db) {
			return (ExtendedResult) SQLite3Native.ExtendedErrCode (db);
		}


		public SQLiteResult Open (string filename) {
			return (SQLiteResult) SQLite3Native.Open (filename, out db_handle);
		}

		public SQLiteResult Open (string filename, int flags, string vfsName) {
			return (SQLiteResult) SQLite3Native.Open (filename, out db_handle, flags, vfsName);
		}

		public SQLiteResult Close (Sqlite3DatabaseHandle db) {
			return (SQLiteResult) SQLite3Native.Close (db);
		}

		//
		//
		//

		public string GetLastErrorMessage () {
			//return Marshal.PtrToStringAnsi (SQLite3Native.Errmsg (db_handle));
			return Marshal.PtrToStringUni (SQLite3Native.Errmsg (db_handle));
		}

		/// <summary>
		/// Übergibt die Abfrage/das SQL-Statement zur Prüfung und Vorbereitung an SQLite.
		/// </summary>
		/// <param name="Query"></param>
		/// <returns></returns>
		private Sqlite3Statement Prepare (string Query) {
			Sqlite3Statement stmt;
			SQLiteResult r;

			r = SQLite3Native.Prepare2 (db_handle, Query, System.Text.UTF8Encoding.UTF8.GetByteCount (Query), out stmt, IntPtr.Zero);
			if (r != SQLiteResult.OK) {
				var s = GetLastErrorMessage ();
			}
			return stmt;
		}

		/// <summary>
		/// Ermittelt die Wirkung der letzten Abfrage.<para></para>
		/// Insbesondere für Updates notwendig, da Updates *ohne* eine zutreffende Zeile *keinen* Fehler zurückliefern. 
		/// </summary>
		/// <returns></returns>
		public int Changes () {
			return SQLite3Native.Changes (db_handle);
		}

		/// <summary>
		/// Führt Abfragen ohne weitere Parameter aus.
		/// </summary>
		/// <param name="Query"></param>
		/// <returns></returns>
		public bool ExecuteNonQuery (string Query) {
			Sqlite3Statement stmt;
			SQLiteResult r;

			stmt = Prepare (Query);
			if (stmt == IntPtr.Zero) {
				var s = GetLastErrorMessage ();
				return false;
			}
			r = SQLite3Native.Step (stmt);
			SQLite3Native.Finalize (stmt);
			return (r == SQLiteResult.Done);
		}

		/// <summary>
		/// Führt Abfragen mit Parametern aus.
		/// </summary>
		/// <param name="Query"></param>
		/// <param name="ArgNames"></param>
		/// <param name="Args"></param>
		/// <returns></returns>
		public bool ExecuteNonQuery (string Query, string [] ArgNames, params object [] Args) {
			int i, index;
			Sqlite3Statement stmt;
			SQLiteResult r;

			stmt = Prepare (Query);
			if (stmt == IntPtr.Zero) {
				var s = GetLastErrorMessage ();
				return false;
			}
			if (ArgNames != null) {
				for (i = 0; i < Args.Length; i++) {
					index = SQLite3Native.BindParameterIndex (stmt, "@" + ArgNames [i]);
					if ((r = BindParameter (stmt, index, Args [i], connection_info)) != SQLiteResult.OK) {
						var s = GetLastErrorMessage ();
						return false;
					}
				}
			}
			r = SQLite3Native.Step (stmt);
			SQLite3Native.Finalize (stmt);
			if (r != SQLiteResult.Done) {
				var s = GetLastErrorMessage ();
			}
			return (r == SQLiteResult.Done);
		}

		/// <summary>
		/// Für Updates.
		/// </summary>
		/// <param name="Query"></param>
		/// <param name="ValueNames"></param>
		/// <param name="Values"></param>
		/// <param name="ArgNames"></param>
		/// <param name="Args"></param>
		/// <returns></returns>
		public bool ExecuteNonQuery (string Query, string [] ValueNames, object [] Values, string [] ArgNames, params object [] Args) {
			int i, index;
			Sqlite3Statement stmt;
			SQLiteResult r;

			stmt = Prepare (Query);
			if (stmt == IntPtr.Zero)
				return false;
			for (i = 0; i < Values.Length; i++) {
				index = SQLite3Native.BindParameterIndex (stmt, "$" + ValueNames [i]);
				if ((r = BindParameter (stmt, index, Values [i], connection_info)) != SQLiteResult.OK)
					return false;
			}
			for (i = 0; i < Args.Length; i++) {
				index = SQLite3Native.BindParameterIndex (stmt, "@" + ArgNames [i]);
				if ((r = BindParameter (stmt, index, Args [i], connection_info)) != SQLiteResult.OK)
					return false;
			}
			r = SQLite3Native.Step (stmt);
			SQLite3Native.Finalize (stmt);
			return (r == SQLiteResult.Done);
		}

		/// <summary>
		/// Liefert die gefundenen SQLite-Zeilen als Liste von <see cref="Dictionary{string, object}"/>.
		/// </summary>
		/// <param name="TargetTypes">Die C#-Typen, in welche die SQLite-Werte konvertiert werden sollen.</param>
		/// <param name="Query">Die vollständige Abfrage mit "@"-Argumenten!</param>
		/// <param name="ArgNames">Die Argument-Namen der Art "@ArgName".</param>
		/// <param name="Args">Die Argument-Werte.</param>
		/// <returns></returns>
		public List<Dictionary<string, object>> ExecuteQuery (Type [] TargetTypes, SQLiteTypes [] SQLites, string Query, string [] ArgNames, params object [] Args) {
			int i, index;
			string column_name;
			Sqlite3Statement stmt;
			object value;
			SQLiteTypes column_type;
			Dictionary<string, object> row;
			List<Dictionary<string, object>> rows;
			SQLiteResult r;

			rows = new List<Dictionary<string, object>> ();
			stmt = Prepare (Query);
			if (stmt == IntPtr.Zero) {
				var s = GetLastErrorMessage ();
				return null;
			}
			if (ArgNames != null) {
				for (i = 0; i < ArgNames.Length; i++) {
					index = SQLite3Native.BindParameterIndex (stmt, "@" + ArgNames [i]);
					if ((r = BindParameter (stmt, index, Args [i], connection_info)) != SQLiteResult.OK)
						return null;
				}
			}
			while (SQLite3Native.Step (stmt) == SQLiteResult.Row) {
				row = new Dictionary<string, object> ();
				for (i = 0; i < SQLite3Native.ColumnCount (stmt); i++) {
					if (TargetTypes [i] == null) // Falls die Klasse kein Feld besitzt, das sich der Spalte zuordnen lässt!
						continue;
					column_type = SQLite3Native.ColumnType (stmt, i);
					if (column_type == SQLiteTypes.NULL)
						continue;
					column_name = Marshal.PtrToStringAnsi (SQLite3Native.ColumnName (stmt, i));
					value = BindColumn (stmt, i, TargetTypes [i], SQLites, connection_info);
					row.Add (column_name, value);
				}
				rows.Add (row);
			}
			SQLite3Native.Finalize (stmt);
			return rows;
		}

	}   // class

}   // class

//  namespace   2022-09-09 - 11.11.31
