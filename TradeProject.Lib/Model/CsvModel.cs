using LINQtoCSV;

namespace TradeProject.Lib.Model
{
    public class CsvModel
    {
        [CsvColumn(FieldIndex = 1)] public string CorrelationID { get; set; }

        [CsvColumn(FieldIndex = 2)] public int NumberOfTrades { get; set; }

        [CsvColumn(FieldIndex = 3)] public string State { get; set; }

        public int Count { get; set; }
        public int Value { get; set; }
        public int Limit { get; set; }

        // Custom code
        public override bool Equals(object obj)
        {
            //Check for null and compare run-time types.
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            return Equals((CsvModel) obj);
        }

        protected bool Equals(CsvModel other)
        {
            return CorrelationID == other.CorrelationID && NumberOfTrades == other.NumberOfTrades &&
                   State == other.State;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = CorrelationID != null ? CorrelationID.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ NumberOfTrades;
                hashCode = (hashCode * 397) ^ (State != null ? State.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}