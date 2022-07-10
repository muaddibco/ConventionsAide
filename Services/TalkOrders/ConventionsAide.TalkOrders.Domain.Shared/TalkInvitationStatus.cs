using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConventionsAide.TalkOrders.Domain.Shared
{
    public enum TalkInvitationStatus
    {
        New = 1,
        Sent,
        Tentative,
        Confirmed,
        Rejected,
        Cancelled
    }
}
