using MediatR;

namespace Basket.Application.Queries;

public class DeleteBasketByUserNameCommand  : IRequest<Unit>
{
    public string UserName { get; set; }

    public DeleteBasketByUserNameCommand(string userName)
    {
        UserName = userName;
    }
}