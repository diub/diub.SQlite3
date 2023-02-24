namespace diub.Database;

public partial class SQLite3 {

	public static class Raw {

		public const int SQLITE_UTF8 = 1;

		public const int SQLITE_UTF16LE = 2;

		public const int SQLITE_UTF16BE = 3;

		public const int SQLITE_UTF16 = 4;

		public const int SQLITE_ANY = 5;

		public const int SQLITE_UTF16_ALIGNED = 8;

		public const int SQLITE_DETERMINISTIC = 2048;

		public const int SQLITE_CONFIG_SINGLETHREAD = 1;

		public const int SQLITE_CONFIG_MULTITHREAD = 2;

		public const int SQLITE_CONFIG_SERIALIZED = 3;

		public const int SQLITE_CONFIG_MALLOC = 4;

		public const int SQLITE_CONFIG_GETMALLOC = 5;

		public const int SQLITE_CONFIG_SCRATCH = 6;

		public const int SQLITE_CONFIG_PAGECACHE = 7;

		public const int SQLITE_CONFIG_HEAP = 8;

		public const int SQLITE_CONFIG_MEMSTATUS = 9;

		public const int SQLITE_CONFIG_MUTEX = 10;

		public const int SQLITE_CONFIG_GETMUTEX = 11;

		public const int SQLITE_CONFIG_LOOKASIDE = 13;

		public const int SQLITE_CONFIG_PCACHE = 14;

		public const int SQLITE_CONFIG_GETPCACHE = 15;

		public const int SQLITE_CONFIG_LOG = 16;

		public const int SQLITE_CONFIG_URI = 17;

		public const int SQLITE_CONFIG_PCACHE2 = 18;

		public const int SQLITE_CONFIG_GETPCACHE2 = 19;

		public const int SQLITE_CONFIG_COVERING_INDEX_SCAN = 20;

		public const int SQLITE_CONFIG_SQLLOG = 21;

		public const int SQLITE_OPEN_READONLY = 1;

		public const int SQLITE_OPEN_READWRITE = 2;

		public const int SQLITE_OPEN_CREATE = 4;

		public const int SQLITE_OPEN_DELETEONCLOSE = 8;

		public const int SQLITE_OPEN_EXCLUSIVE = 16;

		public const int SQLITE_OPEN_AUTOPROXY = 32;

		public const int SQLITE_OPEN_URI = 64;

		public const int SQLITE_OPEN_MEMORY = 128;

		public const int SQLITE_OPEN_MAIN_DB = 256;

		public const int SQLITE_OPEN_TEMP_DB = 512;

		public const int SQLITE_OPEN_TRANSIENT_DB = 1024;

		public const int SQLITE_OPEN_MAIN_JOURNAL = 2048;

		public const int SQLITE_OPEN_TEMP_JOURNAL = 4096;

		public const int SQLITE_OPEN_SUBJOURNAL = 8192;

		public const int SQLITE_OPEN_MASTER_JOURNAL = 16384;

		public const int SQLITE_OPEN_NOMUTEX = 32768;

		public const int SQLITE_OPEN_FULLMUTEX = 65536;

		public const int SQLITE_OPEN_SHAREDCACHE = 131072;

		public const int SQLITE_OPEN_PRIVATECACHE = 262144;

		public const int SQLITE_OPEN_WAL = 524288;

		public const int SQLITE_PREPARE_PERSISTENT = 1;

		public const int SQLITE_PREPARE_NORMALIZE = 2;

		public const int SQLITE_PREPARE_NO_VTAB = 4;

		public const int SQLITE_INTEGER = 1;

		public const int SQLITE_FLOAT = 2;

		public const int SQLITE_TEXT = 3;

		public const int SQLITE_BLOB = 4;

		public const int SQLITE_NULL = 5;

		public const int SQLITE_OK = 0;

		public const int SQLITE_ERROR = 1;

		public const int SQLITE_INTERNAL = 2;

		public const int SQLITE_PERM = 3;

		public const int SQLITE_ABORT = 4;

		public const int SQLITE_BUSY = 5;

		public const int SQLITE_LOCKED = 6;

		public const int SQLITE_NOMEM = 7;

		public const int SQLITE_READONLY = 8;

		public const int SQLITE_INTERRUPT = 9;

		public const int SQLITE_IOERR = 10;

		public const int SQLITE_CORRUPT = 11;

		public const int SQLITE_NOTFOUND = 12;

		public const int SQLITE_FULL = 13;

		public const int SQLITE_CANTOPEN = 14;

		public const int SQLITE_PROTOCOL = 15;

		public const int SQLITE_EMPTY = 16;

		public const int SQLITE_SCHEMA = 17;

		public const int SQLITE_TOOBIG = 18;

		public const int SQLITE_CONSTRAINT = 19;

		public const int SQLITE_MISMATCH = 20;

		public const int SQLITE_MISUSE = 21;

		public const int SQLITE_NOLFS = 22;

		public const int SQLITE_AUTH = 23;

		public const int SQLITE_FORMAT = 24;

		public const int SQLITE_RANGE = 25;

		public const int SQLITE_NOTADB = 26;

		public const int SQLITE_NOTICE = 27;

		public const int SQLITE_WARNING = 28;

		public const int SQLITE_ROW = 100;

		public const int SQLITE_DONE = 101;

		public const int SQLITE_IOERR_READ = 266;

		public const int SQLITE_IOERR_SHORT_READ = 522;

		public const int SQLITE_IOERR_WRITE = 778;

		public const int SQLITE_IOERR_FSYNC = 1034;

		public const int SQLITE_IOERR_DIR_FSYNC = 1290;

		public const int SQLITE_IOERR_TRUNCATE = 1546;

		public const int SQLITE_IOERR_FSTAT = 1802;

		public const int SQLITE_IOERR_UNLOCK = 2058;

		public const int SQLITE_IOERR_RDLOCK = 2314;

		public const int SQLITE_IOERR_DELETE = 2570;

		public const int SQLITE_IOERR_BLOCKED = 2826;

		public const int SQLITE_IOERR_NOMEM = 3082;

		public const int SQLITE_IOERR_ACCESS = 3338;

		public const int SQLITE_IOERR_CHECKRESERVEDLOCK = 3594;

		public const int SQLITE_IOERR_LOCK = 3850;

		public const int SQLITE_IOERR_CLOSE = 4106;

		public const int SQLITE_IOERR_DIR_CLOSE = 4362;

		public const int SQLITE_IOERR_SHMOPEN = 4618;

		public const int SQLITE_IOERR_SHMSIZE = 4874;

		public const int SQLITE_IOERR_SHMLOCK = 5130;

		public const int SQLITE_IOERR_SHMMAP = 5386;

		public const int SQLITE_IOERR_SEEK = 5642;

		public const int SQLITE_IOERR_DELETE_NOENT = 5898;

		public const int SQLITE_IOERR_MMAP = 6154;

		public const int SQLITE_IOERR_GETTEMPPATH = 6410;

		public const int SQLITE_IOERR_CONVPATH = 6666;

		public const int SQLITE_LOCKED_SHAREDCACHE = 262;

		public const int SQLITE_BUSY_RECOVERY = 261;

		public const int SQLITE_BUSY_SNAPSHOT = 517;

		public const int SQLITE_CANTOPEN_NOTEMPDIR = 270;

		public const int SQLITE_CANTOPEN_ISDIR = 526;

		public const int SQLITE_CANTOPEN_FULLPATH = 782;

		public const int SQLITE_CANTOPEN_CONVPATH = 1038;

		public const int SQLITE_CORRUPT_VTAB = 267;

		public const int SQLITE_READONLY_RECOVERY = 264;

		public const int SQLITE_READONLY_CANTLOCK = 520;

		public const int SQLITE_READONLY_ROLLBACK = 776;

		public const int SQLITE_READONLY_DBMOVED = 1032;

		public const int SQLITE_ABORT_ROLLBACK = 516;

		public const int SQLITE_CONSTRAINT_CHECK = 275;

		public const int SQLITE_CONSTRAINT_COMMITHOOK = 531;

		public const int SQLITE_CONSTRAINT_FOREIGNKEY = 787;

		public const int SQLITE_CONSTRAINT_FUNCTION = 1043;

		public const int SQLITE_CONSTRAINT_NOTNULL = 1299;

		public const int SQLITE_CONSTRAINT_PRIMARYKEY = 1555;

		public const int SQLITE_CONSTRAINT_TRIGGER = 1811;

		public const int SQLITE_CONSTRAINT_UNIQUE = 2067;

		public const int SQLITE_CONSTRAINT_VTAB = 2323;

		public const int SQLITE_CONSTRAINT_ROWID = 2579;

		public const int SQLITE_NOTICE_RECOVER_WAL = 283;

		public const int SQLITE_NOTICE_RECOVER_ROLLBACK = 539;

		public const int SQLITE_WARNING_AUTOINDEX = 284;

		public const int SQLITE_CREATE_INDEX = 1;

		public const int SQLITE_CREATE_TABLE = 2;

		public const int SQLITE_CREATE_TEMP_INDEX = 3;

		public const int SQLITE_CREATE_TEMP_TABLE = 4;

		public const int SQLITE_CREATE_TEMP_TRIGGER = 5;

		public const int SQLITE_CREATE_TEMP_VIEW = 6;

		public const int SQLITE_CREATE_TRIGGER = 7;

		public const int SQLITE_CREATE_VIEW = 8;

		public const int SQLITE_DELETE = 9;

		public const int SQLITE_DROP_INDEX = 10;

		public const int SQLITE_DROP_TABLE = 11;

		public const int SQLITE_DROP_TEMP_INDEX = 12;

		public const int SQLITE_DROP_TEMP_TABLE = 13;

		public const int SQLITE_DROP_TEMP_TRIGGER = 14;

		public const int SQLITE_DROP_TEMP_VIEW = 15;

		public const int SQLITE_DROP_TRIGGER = 16;

		public const int SQLITE_DROP_VIEW = 17;

		public const int SQLITE_INSERT = 18;

		public const int SQLITE_PRAGMA = 19;

		public const int SQLITE_READ = 20;

		public const int SQLITE_SELECT = 21;

		public const int SQLITE_TRANSACTION = 22;

		public const int SQLITE_UPDATE = 23;

		public const int SQLITE_ATTACH = 24;

		public const int SQLITE_DETACH = 25;

		public const int SQLITE_ALTER_TABLE = 26;

		public const int SQLITE_REINDEX = 27;

		public const int SQLITE_ANALYZE = 28;

		public const int SQLITE_CREATE_VTABLE = 29;

		public const int SQLITE_DROP_VTABLE = 30;

		public const int SQLITE_FUNCTION = 31;

		public const int SQLITE_SAVEPOINT = 32;

		public const int SQLITE_COPY = 0;

		public const int SQLITE_RECURSIVE = 33;

		public const int SQLITE_CHECKPOINT_PASSIVE = 0;

		public const int SQLITE_CHECKPOINT_FULL = 1;

		public const int SQLITE_CHECKPOINT_RESTART = 2;

		public const int SQLITE_CHECKPOINT_TRUNCATE = 3;

		public const int SQLITE_DBSTATUS_LOOKASIDE_USED = 0;

		public const int SQLITE_DBSTATUS_CACHE_USED = 1;

		public const int SQLITE_DBSTATUS_SCHEMA_USED = 2;

		public const int SQLITE_DBSTATUS_STMT_USED = 3;

		public const int SQLITE_DBSTATUS_LOOKASIDE_HIT = 4;

		public const int SQLITE_DBSTATUS_LOOKASIDE_MISS_SIZE = 5;

		public const int SQLITE_DBSTATUS_LOOKASIDE_MISS_FULL = 6;

		public const int SQLITE_DBSTATUS_CACHE_HIT = 7;

		public const int SQLITE_DBSTATUS_CACHE_MISS = 8;

		public const int SQLITE_DBSTATUS_CACHE_WRITE = 9;

		public const int SQLITE_DBSTATUS_DEFERRED_FKS = 10;

		public const int SQLITE_STATUS_MEMORY_USED = 0;

		public const int SQLITE_STATUS_PAGECACHE_USED = 1;

		public const int SQLITE_STATUS_PAGECACHE_OVERFLOW = 2;

		public const int SQLITE_STATUS_SCRATCH_USED = 3;

		public const int SQLITE_STATUS_SCRATCH_OVERFLOW = 4;

		public const int SQLITE_STATUS_MALLOC_SIZE = 5;

		public const int SQLITE_STATUS_PARSER_STACK = 6;

		public const int SQLITE_STATUS_PAGECACHE_SIZE = 7;

		public const int SQLITE_STATUS_SCRATCH_SIZE = 8;

		public const int SQLITE_STATUS_MALLOC_COUNT = 9;

		public const int SQLITE_STMTSTATUS_FULLSCAN_STEP = 1;

		public const int SQLITE_STMTSTATUS_SORT = 2;

		public const int SQLITE_STMTSTATUS_AUTOINDEX = 3;

		public const int SQLITE_STMTSTATUS_VM_STEP = 4;

		public const int SQLITE_DENY = 1;

		public const int SQLITE_IGNORE = 2;

		public const int SQLITE_TRACE_STMT = 1;

		public const int SQLITE_TRACE_PROFILE = 2;

		public const int SQLITE_TRACE_ROW = 4;

		public const int SQLITE_TRACE_CLOSE = 8;

	}   // class

}   // class

//	namespace	2022-09-09 - 11.56.23