using NMCK3.Application.Abstractions.Messaging;

namespace NMCK3.Application.Participants.Register
{
    public sealed record RegisterCommand(
        string FirstName,
        string LastName,
        string Email,
        string Password) : ICommand<string>;
}
