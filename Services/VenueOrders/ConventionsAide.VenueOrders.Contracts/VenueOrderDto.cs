﻿using ConventionsAide.Core.Contracts;
using ConventionsAide.VenueOrders.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConventionsAide.VenueOrders.Contracts
{
    public class VenueOrderDto : EntityDto<long>
    {
        public long VenuesConfirmationFlowId { get; set; }
        public long VenueId { get; set; }
        public VenueOrderStatus Status { get; set; }
    }
}
