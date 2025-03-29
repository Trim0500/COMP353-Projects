using System;
using System.Collections.Generic;

namespace MYVCApp.Models2;

public partial class Payment
{
    public int Id { get; set; }

    public decimal? Amount { get; set; }

    public DateTime? PaymentDate { get; set; }

    public DateTime? EffectiveDate { get; set; }

    public string? Method { get; set; }

    public int? CmnFk { get; set; }

    public virtual Clubmember? CmnFkNavigation { get; set; }
}
