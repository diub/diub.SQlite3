namespace diub.Database;

abstract public class AColumnSchema {

	/// <summary>
	/// 
	/// </summary>
	public int Index;

	public string ColumnName;

	/// <summary>
	/// Der C#-Typ, der für diese Spalte benutzt werden soll.
	/// </summary>
	public Type MappingType;

}

/// <summary>
/// Spaltenschema einer (einzelnen!) Spalte mit deren Eigenschaften.
/// </summary>
/// <typeparam name="TColumnTypes">Der Typ, der die Datenbank eigenen Typen beschreibt.</typeparam>
public class ColumnSchema<TColumnTypes> : AColumnSchema where TColumnTypes : IComparable {

	/// <summary>
	/// Spaltentyp der Datenbank.
	/// </summary>
	public TColumnTypes ColumnType;

	static public bool Compare (ColumnSchema<TColumnTypes> Left, ColumnSchema<TColumnTypes> Right) {
		if (Left == null)
			return true;
		if (Left.ColumnType.CompareTo (Right.ColumnType) != 0)
			return false;
		if (Left.ColumnName != Right.ColumnName)
			return false;
		return true;
	}

}   // class

//	namespace	2022-10-05 - 14.24.35
