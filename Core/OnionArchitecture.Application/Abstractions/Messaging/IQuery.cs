using MediatR;
using OnionArchitecture.Shared;

namespace OnionArchitecture.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;