namespace diub.Database;

public partial class SQLite3 {

	/// <summary>
	/// 
	/// </summary>
	/// <param name="TableName"></param>
	/// <param name="IndexName">Bei NULL wird ein Name generiert: Tabellenname+"_INDEX_"+Spaltenname(n).</param>
	/// <param name="Unique"></param>
	/// <param name="ColumnNames"></param>
	/// <returns></returns>
	public bool CreateIndex (string TableName, string IndexName = null, bool Unique = false, params string [] ColumnNames) {
		string sql, column_names;

		column_names = string.Join (",", ColumnNames);
		sql = "create ";
		if (Unique)
			sql += " unique ";
		if (IndexName == null || IndexName.Length == 0)
			IndexName = TableName + "_Index_" + string.Join ("_", ColumnNames);
		sql += " index if not exists " + IndexName + " on " + TableName + " (" + column_names + ")";

		return mapper.ExecuteNonQuery (sql);
	}

}   // class

//	namespace	2022-09-23 - 14.27.16