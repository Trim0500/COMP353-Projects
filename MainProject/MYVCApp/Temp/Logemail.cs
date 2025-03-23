using System;
using System.Collections.Generic;

namespace MYVCApp.Temp
{
    public partial class Logemail
    {
        public string Recipient { get; set; } = null!;
        public string DeliveryDateTime { get; set; } = null!;
        public string? Sender { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
    }
}
