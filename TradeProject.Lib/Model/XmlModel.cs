using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace TradeProject.Lib.Model
{
    // REMARQUE : Le code généré peut nécessiter au moins .NET Framework 4.5 ou .NET Core/Standard 2.0.
    /// <remarks />
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class Trade
    {
        /// <remarks />
        [XmlAttribute]
        public string CorrelationId { get; set; }

        /// <remarks />
        [XmlAttribute]
        public int NumberOfTrades { get; set; }

        /// <remarks />
        [XmlAttribute]
        public int Limit { get; set; }

        /// <remarks />
        [XmlAttribute]
        public string TradeID { get; set; }

        /// <remarks />
        [XmlText]
        public int Value { get; set; }


        // Custom code
        public override bool Equals(object obj)
        {
            //Check for null and compare run-time types.
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            return Equals((Trade) obj);
        }

        protected bool Equals(Trade other)
        {
            return CorrelationId == other.CorrelationId && NumberOfTrades == other.NumberOfTrades &&
                   Limit == other.Limit && TradeID == other.TradeID && Value == other.Value;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = CorrelationId != null ? CorrelationId.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ NumberOfTrades;
                hashCode = (hashCode * 397) ^ Limit;
                hashCode = (hashCode * 397) ^ (TradeID != null ? TradeID.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Value;
                return hashCode;
            }
        }
    }
}