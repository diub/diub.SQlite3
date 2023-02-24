namespace diub.Database;

// https://doc.ispirer.com/sqlways/Output/SQLWays-1-196.html

public partial class SQLite3 {

	public partial class NativeMapper {

		public enum SQLiteTypes : int {

			NULL = 0,

			OBJECT,

			TINYINT,            // 8 Bit
			SMALLINT,           // 16 Bit
			INT,                // 32 Bit
			MEDIUMINT,          // 24 Bit
			INTEGER,            // 64 Bit
			BIGINT = INTEGER,   // 64 Bit

			//CHARACTER(20),
			VARCHAR,	// (255)	ASCII
			//VARYING CHARACTER (255),
			//NCHAR (55),
			//NATIVE CHARACTER (70),
			NVARCHAR,	// (100),	Unicode
			TEXT,
			//CLOB,
			BLOB,

			//REAL,
			DOUBLE,
			FLOAT,

			//NUMERIC,
			//DECIMAL (10,5),
			BOOLEAN,
			DATE,
			DATETIME,
		}

	}   // class

}   // class

//	namespace	2022-09-09 - 11.44.25
