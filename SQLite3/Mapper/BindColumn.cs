using System.ComponentModel;
using System.Windows.Forms;

namespace diub.Database;

public partial class SQLite3 {

	public partial class NativeMapper {

		/// <summary>
		/// Liefert zur der mit <paramref name="Index"/> angegebenen Spalte den C#-typsierten Wert zurück.
		/// </summary>
		/// <param name="Statement"></param>
		/// <param name="Index"></param>
		/// <param name="SQLites"></param>
		/// <param name="TargetType"></param>
		/// <param name="ConnectionInfo"></param>
		/// <returns></returns>
		/// <exception cref="NotSupportedException"></exception>
		internal object BindColumn (Sqlite3Statement Statement, int Index, Type TargetType, SQLiteTypes [] SQLites, ConnectionInfo ConnectionInfo) {
			int count;
			string text;
			byte [] ba;
			TypeInfo type_info;
			TypeConverter tc;

			if (TargetType == typeof (byte [])) {
				count = SQLite3Native.ColumnBytes (Statement, Index);
				ba = new byte [count];
				Marshal.Copy (SQLite3Native.ColumnBlob (Statement, Index), ba, 0, count);
				return ba;
			}
			if (TargetType == typeof (Ansistring))
				return Marshal.PtrToStringAnsi (SQLite3Native.ColumnText (Statement, Index));
			if (TargetType == typeof (string))
				return Marshal.PtrToStringUni (SQLite3Native.ColumnText16 (Statement, Index));
			if (TargetType == typeof (Int32))
				return (int) SQLite3Native.ColumnInt (Statement, Index);
			if (TargetType == typeof (UInt32))
				return (uint) SQLite3Native.ColumnInt64 (Statement, Index);
			if (TargetType == typeof (double))
				return SQLite3Native.ColumnDouble (Statement, Index);
			if (TargetType == typeof (float))
				return (float) SQLite3Native.ColumnDouble (Statement, Index);
			if (TargetType == typeof (Int64))
				return SQLite3Native.ColumnInt64 (Statement, Index);
			if (TargetType == typeof (UInt64))
				return Convert.ToUInt64 (SQLite3Native.ColumnInt64 (Statement, Index));
			if (TargetType == typeof (decimal))
				return (decimal) SQLite3Native.ColumnDouble (Statement, Index);
			if (TargetType == typeof (Boolean))
				return SQLite3Native.ColumnInt (Statement, Index) == 1;
			if (TargetType == typeof (Byte))
				return (byte) SQLite3Native.ColumnInt (Statement, Index);
			if (TargetType == typeof (UInt16))
				return (ushort) SQLite3Native.ColumnInt (Statement, Index);
			if (TargetType == typeof (Int16))
				return (short) SQLite3Native.ColumnInt (Statement, Index);
			if (TargetType == typeof (sbyte))
				return (sbyte) SQLite3Native.ColumnInt (Statement, Index);

			type_info = TargetType.GetTypeInfo ();
			if (type_info.IsGenericType && type_info.GetGenericTypeDefinition () == typeof (Nullable<>)) {
				TargetType = type_info.GenericTypeArguments [0];
				type_info = TargetType.GetTypeInfo ();
			}

			if (TargetType == typeof (TimeSpan)) {
				if (ConnectionInfo.StoreTimeSpanAsTicks) {
					return new TimeSpan (SQLite3Native.ColumnInt64 (Statement, Index));
				} else {
					text = Marshal.PtrToStringUni (SQLite3Native.ColumnText (Statement, Index));
					TimeSpan resultTime;
					if (!TimeSpan.TryParseExact (text, "c", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.TimeSpanStyles.None, out resultTime)) {
						resultTime = TimeSpan.Parse (text);
					}
					return resultTime;
				}
			}
			if (TargetType == typeof (DateTime)) {
				if (ConnectionInfo.StoreDateTimeAsTicks) {
					return new DateTime (SQLite3Native.ColumnInt64 (Statement, Index));
				} else {
					text = Marshal.PtrToStringUni (SQLite3Native.ColumnText (Statement, Index));
					DateTime resultDate;
					if (!DateTime.TryParseExact (text, ConnectionInfo.DateTimeStringFormat, System.Globalization.CultureInfo.InvariantCulture, ConnectionInfo.DateTimeStyle, out resultDate)) {
						resultDate = DateTime.Parse (text);
					}
					return resultDate;
				}
			}
			// Parsen / Typeconverter
			tc = TypeDescriptor.GetConverter (TargetType);
			try {
				tc = TypeDescriptor.GetConverter (TargetType);
				if (SQLites [Index] == SQLiteTypes.VARCHAR) {
					text = Marshal.PtrToStringAnsi (SQLite3Native.ColumnText (Statement, Index));
					return tc.ConvertFrom (text);
				}
				text = Marshal.PtrToStringUni (SQLite3Native.ColumnText (Statement, Index));
				return tc.ConvertFrom (text);
			} catch (Exception) { }


			if (TargetType == typeof (Keys)) {
				try {
					tc = TypeDescriptor.GetConverter (TargetType);
					if (SQLites [Index] == SQLiteTypes.VARCHAR) {
						text = Marshal.PtrToStringAnsi (SQLite3Native.ColumnText (Statement, Index));
						return tc.ConvertFrom (text);
					}
				} catch (Exception) {
					return Keys.None;
				}
			}

			//if (TargetType == typeof (DateTimeOffset)) {
			//	return new DateTimeOffset (SQLite3Native.ColumnInt64 (Statement, Index), TimeSpan.Zero);
			//} else if (type_info.IsEnum) {
			//	if (SQLiteType == SQLiteTypes.TEXT) {
			//		var value = Marshal.PtrToStringUni (SQLite3Native.ColumnText (Statement, Index));
			//		return Enum.Parse (TargetType, value.ToString (), true);
			//	} else
			//		return SQLite3Native.ColumnInt (Statement, Index);
			//}
			//if (TargetType == typeof (Guid)) {
			//	var text = Marshal.PtrToStringUni (SQLite3Native.ColumnText (Statement, Index));
			//	return new Guid (text);
			//}
			//if (TargetType == typeof (Uri)) {
			//	var text = Marshal.PtrToStringUni (SQLite3Native.ColumnText (Statement, Index));
			//	return new Uri (text);
			//}
			//if (TargetType == typeof (StringBuilder)) {
			//	var text = Marshal.PtrToStringUni (SQLite3Native.ColumnText (Statement, Index));
			//	return new StringBuilder (text);
			//}
			//if (TargetType == typeof (UriBuilder)) {
			//	var text = Marshal.PtrToStringUni (SQLite3Native.ColumnText (Statement, Index));
			//	return new UriBuilder (text);
			//}


			throw new NotSupportedException ("Don't know how to read " + TargetType);
		}

	}   // class

}   // class

//	namespace	2022-09-11 - 13.19.16