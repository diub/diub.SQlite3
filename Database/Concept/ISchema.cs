
namespace diub.Database;

public interface ISchema<TColumnTypes> where TColumnTypes : Enum, IComparable {

	public TableSchema<TColumnTypes> GetTableSchema (string Tablename);

}   // class

//	namespace	2022-10-05 - 14.31.47
