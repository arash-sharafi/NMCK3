using MediatR;
using NMCK3.Domain.Common;

namespace NMCK3.Application.Abstractions.Messaging
{
    public interface ICommand : IRequest<Result>
    {
    }

    public interface ICommand<TResponse> : IRequest<Result<TResponse>>
    {
    }
}
