﻿namespace diub.Database;

public partial class SQLite3 {

	public enum SQLiteResult : int {
		OK = 0,
		Error = 1,
		Internal = 2,
		Perm = 3,
		Abort = 4,
		Busy = 5,
		Locked = 6,
		NoMem = 7,
		ReadOnly = 8,
		Interrupt = 9,
		IOError = 10,
		Corrupt = 11,
		NotFound = 12,
		Full = 13,
		CannotOpen = 14,
		LockErr = 15,
		Empty = 16,
		SchemaChngd = 17,
		TooBig = 18,
		Constraint = 19,
		Mismatch = 20,
		Misuse = 21,
		NotImplementedLFS = 22,
		AccessDenied = 23,
		Format = 24,
		RangeError = 25,
		NonDBFile = 26,
		Notice = 27,
		Warning = 28,
		Row = 100,
		Done = 101
	}   // class

}   // class

//	namespace	2022-09-09 - 11.46.24
