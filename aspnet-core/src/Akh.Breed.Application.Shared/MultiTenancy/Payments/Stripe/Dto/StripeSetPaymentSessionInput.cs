﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Akh.Breed.MultiTenancy.Payments.Stripe.Dto
{
    public class StripeSetPaymentSessionInput
    {
        public long PaymentId { get; set; }

        public string SessionId { get; set; }
    }
}

