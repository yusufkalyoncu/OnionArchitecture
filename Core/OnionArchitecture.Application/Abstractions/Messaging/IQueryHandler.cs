using MediatR;
using OnionArchitecture.Shared;

namespace OnionArchitecture.Application.Abstractions.Messaging;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>> where TQuery : IQuery<TResponse>;