using System;
using System.Collections.Generic;

namespace MYVCApp.Models
{
    public partial class Logemail
    {
        public string Recipient { get; set; } = null!;
        public DateTime DeliveryDateTime { get; set; }
        public string? Sender { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
    }
}
