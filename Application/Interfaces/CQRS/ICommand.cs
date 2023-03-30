using MediatR;

namespace Application.Interfaces.CQRS;

public interface ICommand : IRequest<int> { }

public interface ICommand<TResponse> : IRequest<TResponse>
{ }
