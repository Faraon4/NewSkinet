using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Core.Entities.OrderAggregate
{
    // In real time application , it should have more statuses like : shiped delivery etc
    public enum OrderStatus
    {
        [EnumMember(Value ="Pending")]
        Pending,
        [EnumMember(Value ="Payment Received")]
        PaymentReceived,
        [EnumMember(Value ="Payment Failed")]
        PaymentFailed
    }
}