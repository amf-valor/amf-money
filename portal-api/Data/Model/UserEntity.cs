using System;
using System.ComponentModel.DataAnnotations;

namespace AmfValor.AmfMoney.PortalApi.Data.Model
{
    public class UserEntity
    {
        public int Id { get; set; }
        [Required]
        public DateTime Birth { get; set; }
        public DateTime CreatedAt { get; set; }
        [Required]
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        public byte[] PasswordHashed { get; set; }
        [Required]
        public byte[] PasswordSalt { get; set; }
        [Required]
        public byte[] PinHashed { get; set; }
        [Required]
        public byte[] PinSalt { get; set; }
    }
}
