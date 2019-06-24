using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmfValor.AmfMoney.PortalApi.Model
{
    public class Trade
    {
        public int Id { get; set; }

        [Required]
        [RegularExpression("B|S", ErrorMessage = "The only values allowed are B or S B = Buy ans S = Sell")]
        public char OperationType { get; set; }

        [Required]
        [StringLength(50)]
        public string Asset { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        [Range(typeof(decimal), "0", Decimal18_2)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [Required]
        [Range(typeof(decimal), "0", Decimal18_2)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal StopLoss { get; set; }

        [Required]
        [Range(typeof(decimal), "0", Decimal18_2)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal StopGain { get; set; }

        private const string Decimal18_2 = "9999999999999999";
    }
}