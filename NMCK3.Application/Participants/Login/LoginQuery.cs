using NMCK3.Application.Abstractions.Messaging;

namespace NMCK3.Application.Participants.Login
{
    public sealed record LoginQuery(
        string Email,
        string Password) : ICommand<string>;
}
