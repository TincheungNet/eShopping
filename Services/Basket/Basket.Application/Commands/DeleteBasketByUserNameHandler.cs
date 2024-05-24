using Basket.Application.Queries;
using Basket.Core.Repositories;
using MediatR;

namespace Basket.Application.Commands;

public class DeleteBasketByUserNameHandler : IRequestHandler<DeleteBasketByUserNameCommand, Unit>
{
    private readonly IBasketRepository _basketRepository;

    public DeleteBasketByUserNameHandler(IBasketRepository basketRepository)
    {
        _basketRepository = basketRepository;
    }
    public async Task<Unit> Handle(DeleteBasketByUserNameCommand request, CancellationToken cancellationToken)
    {
        await _basketRepository.DeleteBasket(request.UserName);
        return Unit.Value;
    }


}