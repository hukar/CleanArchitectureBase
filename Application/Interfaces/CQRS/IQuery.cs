using MediatR;

namespace Application.Interfaces.CQRS;

public interface IQuery<TResponse> : IRequest<TResponse>
{ }

