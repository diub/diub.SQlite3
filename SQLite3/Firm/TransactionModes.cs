namespace diub.Database;

public partial class SQLite3 {

	public enum TransactionModes {

		/// <summary>
		/// Nur eine Transaktion zur Zeit ist gestattet.
		/// </summary>
		Exclusive,
		
		Default = Exclusive,

		/// <summary>
		/// Alle Transaktionen werden zusammengefasst, bis die letzte beendt wird.
		/// Achtung: das ist völlig ungeeignet bei möglichen "Rollback".
		/// </summary>
		SpeedCombine,

	}

}   // class

//	namespace	2024-03-14 - 12.45.08
