namespace diub.Database;

public enum QueryCompareType {

	Equal,
	LessThen, LessThanOrEqual, GreaterThan, GreaterThanOrEqual, Unequal,
	/// <summary>
	/// Enspricht hier 'Contains'.
	/// </summary>
	Like,
	/// <summary>
	/// Enspricht hier 'Contains Not'.
	/// </summary>
	Unlike,
	StartsWith,
	EndsWith

}   // class

//	namespace	2018-09-03 - 10.34.13