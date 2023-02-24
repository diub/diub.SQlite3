namespace diub.Database;

public partial class SQLite3 {

	/// <summary>
	/// Modelt die C#-Punktnotation für die Spaltennanme und die Namen der Argumente um ("Address.Street" → "'Address_Street'").
	/// </summary>
	/// <param name="Args"></param>
	/// <returns></returns>
	private (string Query, string Arguments, string [] FixedArguments) FixArguments (string [] Args) {
		int i;
		StringBuilder query, arguments;
		string [] stra;

		stra = new string [Args.Length];
		query = new StringBuilder ();
		arguments = new StringBuilder ();
		for (i = 0; i < Args.Length; i++) {
			query.Append ("'");
			query.Append (Args [i]);
			query.Append ("'");
			//
			stra [i] = Args [i].Replace ('.', '_');
			arguments.Append ("@");
			arguments.Append (stra [i]);

			if (i < Args.Length - 1) {
				query.Append (",");
				arguments.Append (",");
			}
		}
		return (query.ToString (), arguments.ToString (), stra);
	}

	/// <summary>
	/// Modelt die C#-Punktnotation für die Namen der Argumente um ("Address.Street" → "Address_Street").
	/// </summary>
	/// <param name="Args"></param>
	/// <returns></returns>
	private string [] FixNames (string [] Args) {
		int i;
		string [] stra;

		stra = new string [Args.Length];
		for (i = 0; i < Args.Length; i++)
			stra [i] = Args [i].Replace ('.', '_');
		return stra;
	}


}   // class

//	namespace	2022-09-16 - 16.40.58