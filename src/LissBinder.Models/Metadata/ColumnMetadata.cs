
namespace Escyug.LissBinder.Models.Metadata
{
    public sealed class ColumnMetadata
    {
        public string Name { get; private set; }

        // ?? object ??
        public string Type { get; private set; }

        public int Length { get; private set; }

        public ColumnMetadata(string columnName, string columnType, int columnLength)
        {
            Name = columnName;
            Type = columnType;
            Length = columnLength;
        }
    }
}
