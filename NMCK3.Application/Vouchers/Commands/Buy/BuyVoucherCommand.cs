using NMCK3.Application.Abstractions.Messaging;
using NMCK3.Domain.Entities;
using System;
using System.Collections.ObjectModel;

namespace NMCK3.Application.Vouchers.Commands.Buy
{
    public sealed record BuyVoucherCommand(
        string UserId,
        int Count) : ICommand<ReadOnlyCollection<Voucher>>;
}
