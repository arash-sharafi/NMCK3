using NMCK3.Application.Abstractions.Messaging;
using System.Collections.ObjectModel;

namespace NMCK3.Application.Vouchers.Commands.Buy
{
    public sealed record BuyVoucherCommand(
        string BuyerId,
        int Count) : ICommand<ReadOnlyCollection<string>>;
}
