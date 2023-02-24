namespace diub.Database;

/// <summary>
/// Abfrage ohne Parameter.
/// </summary>
abstract public class QueryBase {

	public string ColumnName;

	public QueryCompareType CompareType;

	public QueryBase (string QueryColumnName, QueryCompareType QueryCompareType = QueryCompareType.Equal) {
		ColumnName = QueryColumnName;
		CompareType = QueryCompareType;
	}
}

/// <summary>
/// Abfrage inkl. Parameter
/// </summary>
public class QueryItem : QueryBase {

	public object Value;

	public QueryItem (string QueryColumnName, QueryCompareType QueryCompareType = QueryCompareType.Equal)
			: base (QueryColumnName, QueryCompareType) {
	}

	public QueryItem (string QueryColumnName, object Value, QueryCompareType QueryCompareType)
		: base (QueryColumnName, QueryCompareType) {
		this.Value = Value;
	}

	//public QueryItem (QueryBase Base) :
	//	base (Base.ColumnName, Base.CompareType) {
	//}

	//static public List<QueryItem> ToQueries (List<QueryBase> Bases) {
	//	List<QueryItem> queries;

	//	queries = new List<QueryItem> ();
	//	foreach (QueryBase item in Bases)
	//		queries.Add (new QueryItem (item));
	//	return queries;
	//}

}   // class



//	namespace	2018-08-26 - 14.13.02
