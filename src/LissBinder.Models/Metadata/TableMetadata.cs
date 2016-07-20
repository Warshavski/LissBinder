using System.Collections.Generic;

namespace Escyug.LissBinder.Models.Metadata
{
    public sealed class TableMetadata
    {
        public string Name { get; private set; }

        public IEnumerable<ColumnMetadata> Columns { get; private set; }

        public long RowsCount { get; private set; }

        public TableMetadata(string tableName, IEnumerable<ColumnMetadata> tableColumns)
        {
            Name = tableName;
            Columns = tableColumns;
        }

        public TableMetadata(string tableName, IEnumerable<ColumnMetadata> tableColumns, long rowsCount)
            : this(tableName, tableColumns)
        {
            RowsCount = rowsCount;
        }
    }
}
