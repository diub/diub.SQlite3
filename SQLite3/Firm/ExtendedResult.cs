namespace diub.Database;

public partial class SQLite3 {

	public enum ExtendedResult : int {
		IOErrorRead = (SQLiteResult.IOError | (1 << 8)),
		IOErrorShortRead = (SQLiteResult.IOError | (2 << 8)),
		IOErrorWrite = (SQLiteResult.IOError | (3 << 8)),
		IOErrorFsync = (SQLiteResult.IOError | (4 << 8)),
		IOErrorDirFSync = (SQLiteResult.IOError | (5 << 8)),
		IOErrorTruncate = (SQLiteResult.IOError | (6 << 8)),
		IOErrorFStat = (SQLiteResult.IOError | (7 << 8)),
		IOErrorUnlock = (SQLiteResult.IOError | (8 << 8)),
		IOErrorRdlock = (SQLiteResult.IOError | (9 << 8)),
		IOErrorDelete = (SQLiteResult.IOError | (10 << 8)),
		IOErrorBlocked = (SQLiteResult.IOError | (11 << 8)),
		IOErrorNoMem = (SQLiteResult.IOError | (12 << 8)),
		IOErrorAccess = (SQLiteResult.IOError | (13 << 8)),
		IOErrorCheckReservedLock = (SQLiteResult.IOError | (14 << 8)),
		IOErrorLock = (SQLiteResult.IOError | (15 << 8)),
		IOErrorClose = (SQLiteResult.IOError | (16 << 8)),
		IOErrorDirClose = (SQLiteResult.IOError | (17 << 8)),
		IOErrorSHMOpen = (SQLiteResult.IOError | (18 << 8)),
		IOErrorSHMSize = (SQLiteResult.IOError | (19 << 8)),
		IOErrorSHMLock = (SQLiteResult.IOError | (20 << 8)),
		IOErrorSHMMap = (SQLiteResult.IOError | (21 << 8)),
		IOErrorSeek = (SQLiteResult.IOError | (22 << 8)),
		IOErrorDeleteNoEnt = (SQLiteResult.IOError | (23 << 8)),
		IOErrorMMap = (SQLiteResult.IOError | (24 << 8)),
		LockedSharedcache = (SQLiteResult.Locked | (1 << 8)),
		BusyRecovery = (SQLiteResult.Busy | (1 << 8)),
		CannottOpenNoTempDir = (SQLiteResult.CannotOpen | (1 << 8)),
		CannotOpenIsDir = (SQLiteResult.CannotOpen | (2 << 8)),
		CannotOpenFullPath = (SQLiteResult.CannotOpen | (3 << 8)),
		CorruptVTab = (SQLiteResult.Corrupt | (1 << 8)),
		ReadonlyRecovery = (SQLiteResult.ReadOnly | (1 << 8)),
		ReadonlyCannotLock = (SQLiteResult.ReadOnly | (2 << 8)),
		ReadonlyRollback = (SQLiteResult.ReadOnly | (3 << 8)),
		AbortRollback = (SQLiteResult.Abort | (2 << 8)),
		ConstraintCheck = (SQLiteResult.Constraint | (1 << 8)),
		ConstraintCommitHook = (SQLiteResult.Constraint | (2 << 8)),
		ConstraintForeignKey = (SQLiteResult.Constraint | (3 << 8)),
		ConstraintFunction = (SQLiteResult.Constraint | (4 << 8)),
		ConstraintNotNull = (SQLiteResult.Constraint | (5 << 8)),
		ConstraintPrimaryKey = (SQLiteResult.Constraint | (6 << 8)),
		ConstraintTrigger = (SQLiteResult.Constraint | (7 << 8)),
		ConstraintUnique = (SQLiteResult.Constraint | (8 << 8)),
		ConstraintVTab = (SQLiteResult.Constraint | (9 << 8)),
		NoticeRecoverWAL = (SQLiteResult.Notice | (1 << 8)),
		NoticeRecoverRollback = (SQLiteResult.Notice | (2 << 8))

	}   // class

}   // class

//	namespace	2022-09-09 - 11.44.54