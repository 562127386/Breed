﻿using System.Collections.Generic;
using Akh.Breed.Editions.Dto;
using Akh.Breed.MultiTenancy.Payments;

namespace Akh.Breed.Web.Models.Payment
{
    public class ExtendEditionViewModel
    {
        public EditionSelectDto Edition { get; set; }

        public List<PaymentGatewayModel> PaymentGateways { get; set; }
    }
}
