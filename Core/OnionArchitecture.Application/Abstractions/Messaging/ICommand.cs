using MediatR;
using OnionArchitecture.Shared;

namespace OnionArchitecture.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result>, IBaseCommand;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand;

public interface IBaseCommand;