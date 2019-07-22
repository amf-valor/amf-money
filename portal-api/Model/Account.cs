using System;
using System.ComponentModel.DataAnnotations;

namespace AmfValor.AmfMoney.PortalApi.Model
{
    public class Account
    {
        [Required]
        public DateTime Birth { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [StringLength(32, MinimumLength = 8)]
        public string Password { get; set; }
        [Required]
        [StringLength(4)]
        public string Pin { get; set; }
    }
}
