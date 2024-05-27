using Rahtk.Domain.Features.Order;

namespace Rahtk.Contracts.Features.Order
{
	public interface IOrderRepository
	{
		Task<ICollection<OrderEntity>> GetOrders(string email);

        Task<OrderEntity> CreateOrder(CreateOrderModel order, string email);
    }
}

