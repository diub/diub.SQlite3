namespace diub.Database;

public partial class SQLite3 {

	public partial class NativeMapper {

		/// <summary>
		/// Verknüft den mit <paramref name="Value"/> angegebenen Wert mit der mit <paramref name="Index"/> angegebenen
		/// Spalte für die Abfrage.
		/// </summary>
		/// <param name="Statement"></param>
		/// <param name="Index"></param>
		/// <param name="Value"></param>
		/// <param name="ConnectionInfo"></param>
		/// <returns></returns>
		/// <exception cref="NotSupportedException"></exception>
		internal SQLiteResult BindParameter (Sqlite3Statement Statement, int Index, object Value, ConnectionInfo ConnectionInfo) {
			if (Value == null)
				return (SQLiteResult) SQLite3Native.BindNull (Statement, Index);
			if (Value is string)
				return (SQLiteResult) SQLite3Native.BindText (Statement, Index, (string) Value, -1, neg_ptr);
			if (Value is bool)
				return (SQLiteResult) SQLite3Native.BindInt (Statement, Index, (bool) Value ? 1 : 0);
			if (Value is byte)
				return (SQLiteResult) SQLite3Native.BindInt (Statement, Index, Convert.ToInt32 (Value));
			if (Value is SByte)
				return (SQLiteResult) SQLite3Native.BindInt (Statement, Index, Convert.ToInt32 (Value));
			if (Value is Int16)
				return (SQLiteResult) SQLite3Native.BindInt (Statement, Index, Convert.ToInt32 (Value));
			if (Value is Int32)
				return (SQLiteResult) SQLite3Native.BindInt (Statement, Index, (Int32) Value);
			if (Value is Int64)
				return (SQLiteResult) SQLite3Native.BindInt64 (Statement, Index, (Int64) Value);
			if (Value is float || Value is Double || Value is Decimal)
				return (SQLiteResult) SQLite3Native.BindDouble (Statement, Index, Convert.ToDouble (Value));
			if (Value is byte [])
				return (SQLiteResult) SQLite3Native.BindBlob (Statement, Index, (byte []) Value, ((byte []) Value).Length, neg_ptr);

			if (Value is TimeSpan) {
				if (ConnectionInfo.StoreTimeSpanAsTicks)
					return (SQLiteResult) (SQLiteResult) SQLite3Native.BindInt64 (Statement, Index, ((TimeSpan) Value).Ticks);
				return (SQLiteResult) (SQLiteResult) SQLite3Native.BindText (Statement, Index, ((TimeSpan) Value).ToString (), -1, neg_ptr);
			}
			if (Value is DateTime) {
				if (ConnectionInfo.StoreDateTimeAsTicks)
					return (SQLiteResult) (SQLiteResult) SQLite3Native.BindInt64 (Statement, Index, ((DateTime) Value).Ticks);
				return (SQLiteResult) SQLite3Native.BindText (Statement, Index, ((DateTime) Value).ToString (ConnectionInfo.DateTimeStringFormat, System.Globalization.CultureInfo.InvariantCulture), -1, neg_ptr);
			}

			//else if (value is DateTimeOffset) {
			//	result = (SQLiteResult) SQLite3Native.BindInt64 (stmt, index, ((DateTimeOffset) value).UtcTicks);
			//} else if (value is Guid) {
			//	result = (SQLiteResult) SQLite3Native.BindText (stmt, index, ((Guid) value).ToString (), 72, NegativePointer);
			//} else if (value is Uri) {
			//	result = (SQLiteResult) SQLite3Native.BindText (stmt, index, ((Uri) value).ToString (), -1, NegativePointer);
			//} else if (value is StringBuilder) {
			//	result = (SQLiteResult) SQLite3Native.BindText (stmt, index, ((StringBuilder) value).ToString (), -1, NegativePointer);
			//} else if (value is UriBuilder) {
			//	result = (SQLiteResult) SQLite3Native.BindText (stmt, index, ((UriBuilder) value).ToString (), -1, NegativePointer);
			//}

			throw new NotSupportedException ("Cannot store type: " + Value.GetType ().ToString ());
		}

	}   // class

}   // class

//	namespace	2022-09-09 - 14.47.39