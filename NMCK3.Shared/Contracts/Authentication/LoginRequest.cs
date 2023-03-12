namespace NMCK3.Shared.Contracts.Authentication
{
    public record LoginRequest(
        string Email,
        string Password);
}