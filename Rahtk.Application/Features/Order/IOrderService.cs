using Rahtk.Domain.Features.Order;
using Rahtk.Shared.Models;

namespace Rahtk.Application.Features.Order
{
	public interface IOrderService
	{
        Task<BaseResponse<OrderEntity>> CreateOrder(CreateOrderModel model, string userEmail);

        Task<BaseResponse<ICollection<OrderEntity>>> GetOrders(string userEmail);
    }
}

