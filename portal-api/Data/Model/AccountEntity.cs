﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AmfValor.AmfMoney.PortalApi.Data.Model
{
    public class AccountEntity
    {
        public int Id { get; set; }
        [Required]
        public DateTime Birth { get; set; }
        public DateTime CreatedAt { get; set; }
        [Required]
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        public byte[] HashedPassword { get; set; }
        [Required]
        public byte[] PasswordSalt { get; set; }
        [Required]
        public byte[] HashedPin { get; set; }
        [Required]
        public byte[] PinSalt { get; set; }
        public ICollection<TradingBookEntity> TradingBookEntities { get; set; }
    }
}
