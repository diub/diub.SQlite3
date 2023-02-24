global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Text;
global using System.Threading.Tasks;
global using System.Runtime.InteropServices;
global using System.Globalization;
global using System.Reflection;
global using diub.Database;
global using System.Data;
global using System.Threading;

global using Sqlite3DatabaseHandle = System.IntPtr;
global using Sqlite3BackupHandle = System.IntPtr;
global using Sqlite3Statement = System.IntPtr;

global using static diub.Database.SQLite3.NativeMapper;

global using Id = System.Int64;

namespace diub.Database;

public partial class SQLite3 {
}   // class

//	namespace	2022-09-15 - 16.23.42
