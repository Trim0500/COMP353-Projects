using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MYVCApp.Models
{
    public partial class Payment
    {
        public int Id { get; set; }

        [Range(0, 999.99)]
        public decimal? Amount { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime? EffectiveDate { get; set; }

        [MaxLength(10)]
        public string? Method { get; set; }
        public int? CmnFk { get; set; }

        public virtual Clubmember? CmnFkNavigation { get; set; }
    }
}
