using System;
using System.ComponentModel.DataAnnotations;

namespace AmfValor.AmfMoney.PortalApi.Model
{
    public class Trade
    {
        public int Id { get; set; }

        [RegularExpression("B|S", ErrorMessage = "The only values allowed is B or S B = Buy ans S = Sell")]
        public char OperationType { get; set; }
    }
}