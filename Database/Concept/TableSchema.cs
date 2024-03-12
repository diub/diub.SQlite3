namespace diub.Database;

/// <summary>
/// Ist das noch notwendig??? Abstrakte Klasse als universeller Rückgabetyp.
/// </summary>
public interface ITableSchema<TColumnTypes> where TColumnTypes : Enum, IComparable {

	abstract public int ColumnsCount {
		get;
	}

	//abstract public AColumnSchema this [string ColumnName] { get; }
	abstract public ColumnSchema<TColumnTypes> this [string ColumnName] { get; }

	//abstract public AColumnSchema this [int Index] { get; }
	abstract public ColumnSchema<TColumnTypes> this [int Index] { get; }

	//abstract public Dictionary<string, AColumnSchema> Columns {
	//	get;
	//}
	abstract public Dictionary<string, ColumnSchema<TColumnTypes>> Columns {
		get;
	}
}

/// <summary>
/// Das Tabellenschema enthält Tabellennamen und die Spaltenschemata (<see cref="ColumnSchema{TColumnTypes}"/>).
/// </summary>
/// <typeparam name="TColumnTypes">Der Typ, der die Datenbank eigenen Typen beschreibt.</typeparam>
public class TableSchema<TColumnTypes> : ITableSchema<TColumnTypes> where TColumnTypes : Enum, IComparable {

	public string TableName;

	/// <summary>
	/// Spaltenname → Spalten-Schema der Datenbank
	/// </summary>
	public Dictionary<string, ColumnSchema<TColumnTypes>> TColumns = new Dictionary<string, ColumnSchema<TColumnTypes>> ();

	public int ColumnsCount {
		get {
			return TColumns.Count;
		}
	}

	/// <summary>
	/// <see cref="AColumnSchema"/> entspricht dabei <see cref="ColumnSchema{TColumnTypes}"/> .
	/// </summary>
	/// <param name="ColumnName"></param>
	/// <returns></returns>
	//public AColumnSchema this [string ColumnName] {
	//	get {
	//		return TColumns [ColumnName];
	//	}
	//}
	public ColumnSchema<TColumnTypes> this [string ColumnName] {
		get {
			return TColumns [ColumnName];
		}
	}

	/// <summary>
	/// <see cref="AColumnSchema"/> entspricht dabei <see cref="ColumnSchema{TColumnTypes}"/> .
	/// </summary>
	/// <param name="Index"></param>
	/// <returns></returns>
	//public AColumnSchema this [int Index] {
	//	get {
	//		foreach (ColumnSchema<TColumnTypes> item in TColumns.Values)
	//			if (item.Index == Index)
	//				return item;
	//		return null;
	//	}
	//}
	public ColumnSchema<TColumnTypes> this [int Index] {
		get {
			foreach (ColumnSchema<TColumnTypes> item in TColumns.Values)
				if (item.Index == Index)
					return item;
			return null;
		}
	}


	//public Dictionary<string, AColumnSchema> Columns {
	//	get {
	//		Dictionary<string, AColumnSchema> column_schemas;

	//		column_schemas = new Dictionary<string, AColumnSchema> ();
	//		foreach (ColumnSchema<TColumnTypes> item in TColumns.Values)
	//			column_schemas.Add (item.ColumnName, item);
	//		return column_schemas;
	//	}
	//}

	public Dictionary<string, ColumnSchema<TColumnTypes>> Columns {
		get {
			Dictionary<string, ColumnSchema<TColumnTypes>> column_schemas;

			column_schemas = new Dictionary<string, ColumnSchema<TColumnTypes>> ();
			foreach (ColumnSchema<TColumnTypes> item in TColumns.Values)
				column_schemas.Add (item.ColumnName, item);
			return column_schemas;
		}
	}

	/// <summary>
	/// Prüft, ob die Spalten identisch sind. Die Reihenfolge im Schema ist dabei egal.
	/// </summary>
	/// <param name="Left"></param>
	/// <param name="Right"></param>
	/// <returns></returns>
	static public bool Compare (TableSchema<TColumnTypes> Left, TableSchema<TColumnTypes> Right) {
		ColumnSchema<TColumnTypes> right;

		if (Left == null)
			return true;
		if (Left.ColumnsCount != Right.ColumnsCount)
			return false;
		foreach (ColumnSchema<TColumnTypes> item in Left.TColumns.Values) {
			// Existiert die Spalte überhaupt?
			if (!Right.TColumns.TryGetValue (item.ColumnName, out right))
				return false;
			// Stimmen die Spaltenschemata überein ?
			if (!ColumnSchema<TColumnTypes>.Compare (item, right))
				return false;
		}
		return true;
	}

	/// <summary>
	/// Prüft, ob die Spalten identisch sind. Die Reihenfolge im Schema wird dabei beachtet.
	/// </summary>
	/// <param name="Left"></param>
	/// <param name="Right"></param>
	/// <returns></returns>
	static public bool CompareWithColumnOrder (TableSchema<TColumnTypes> Left, TableSchema<TColumnTypes> Right) {
		int i;
		ColumnSchema<TColumnTypes> left, right;

		if (Left == null)
			return true;
		if (Left.ColumnsCount != Right.ColumnsCount)
			return false;
		for (i = 0; i < Left.ColumnsCount; i++) {
			try {
				left = Left [i];
				right = Right [i];
				// Stimmen die Spaltenschemata überein ?
				if (!ColumnSchema<TColumnTypes>.Compare (left, right))
					return false;
			} catch (Exception) { }
		}
		return true;
	}

}   // class

//	namespace	2022-10-05 - 14.24.35
