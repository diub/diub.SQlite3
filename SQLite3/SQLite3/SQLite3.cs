namespace diub.Database;

public partial class SQLite3 : IDisposable {

	private string path_filename;

	protected NativeMapper mapper;

	protected ConnectionInfo connection_info;

	protected bool connected;

	protected bool is_disposing = false;

	/// <summary>
	/// Zähler für weitere Verbindungsversuche
	/// </summary>
	protected int connect_counter = 0;

	/// <summary>
	/// Zugriffsregler für die einzelnen Threads
	/// </summary>
	protected Semaphore semaphore = new Semaphore (1, 1);

	public SQLite3 (string PathFilename) {
		path_filename = PathFilename;
		//connection_info = new ConnectionInfo (path_filename);
	}

	virtual public void Dispose () {
		if (is_disposing)
			return;
		is_disposing = true;
		Disconnect ();
	}

	/// <summary>
	/// Opens/Connect to the database. Calls <see cref="Connect"/>.
	/// </summary>
	/// <param name="OpenFlags">Für nur Lesen: <see cref="SQLiteOpenFlags.ReadOnly"/></param>
	/// <returns></returns>
	virtual public bool Open (SQLiteOpenFlags OpenFlags = SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite) {
		return Connect (OpenFlags);
	}

	/// <summary>
	/// Opens/Connect to the database. Called by <see cref="Open"/>.
	/// </summary>
	/// <param name="OpenFlags">Für nur Lesen: <see cref="SQLiteOpenFlags.ReadOnly"/></param>
	/// <returns></returns>
	virtual public bool Connect (SQLiteOpenFlags OpenFlags = SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite) {
		SQLiteResult r;

		lock (semaphore) {
			if (connect_counter == 0) {
				mapper = new NativeMapper (connection_info);
				connection_info = new ConnectionInfo (path_filename, OpenFlags);
				r = mapper.Open (path_filename, (int) connection_info.OpenFlags, connection_info.VfsName);
				connected = r == SQLiteResult.OK;
			}
			if (connected)
				connect_counter++;
		}
		semaphore.WaitOne ();
		return connected;
	}

	/// <summary>
	/// Dissconects / close the database. Calls <see cref="Disconnect"/>.
	/// </summary>
	/// <returns></returns>
	virtual public bool Close () {
		return Disconnect ();
	}

	/// <summary>
	/// Dissconects / close the database. Called by <see cref="Close"/>.
	/// </summary>
	/// <returns></returns>
	virtual public bool Disconnect () {
		bool status;

		if (!connected)
			return true;
		lock (semaphore) {
			connect_counter--;
			if (connect_counter == 0) {
				status = (SQLite3Native.Close (mapper.db_handle) == SQLiteResult.OK);
				connected = !status;
			} else {
				status = true;
			}
		}
		semaphore.Release ();
		return status;
	}

	public bool Compact () {
		bool was_connected;
		string query;

		was_connected = connected;
		if (!was_connected)
			if (!Connect ())
				return false;
		query = "VACUUM";
		try {
			return mapper.ExecuteNonQuery (query);
		} finally {
			if (!was_connected)
				Disconnect ();
		}
	}


	public bool BeginTransaction () {
		string query;

		query = "BEGIN DEFERRED";
		query += " TRANSACTION";
		return mapper.ExecuteNonQuery (query);
	}

	public bool Commit () {
		string query;

		query = "COMMIT";
		query += " TRANSACTION";
		return mapper.ExecuteNonQuery (query);
	}

	public bool Rollback () {
		string query;

		query = "ROLLBACK";
		query += " TRANSACTION";
		return mapper.ExecuteNonQuery (query);
	}


	//
	// Properties
	//

	public bool Connected {
		get {
			return connected;
		}
	}

}   // class

//	namespace	2022-09-16 - 11.00.14