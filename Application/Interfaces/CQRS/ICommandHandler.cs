using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Application.Interfaces.CQRS;

public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, int> 
    where TCommand : ICommand
{ }

public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
    where TResponse : class
{ }