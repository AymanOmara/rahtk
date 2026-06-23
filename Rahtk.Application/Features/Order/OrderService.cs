using Rahtk.Contracts.Common;
using Rahtk.Domain.Features.Order;
using Rahtk.Shared.Models;

namespace Rahtk.Application.Features.Order
{
    public class OrderService(IUnitOfWork unitOfWork) : IOrderService
    {
        public async Task<BaseResponse<OrderEntity>> CreateOrder(CreateOrderModel model, string userEmail)
        {
            var result = await unitOfWork.Order.CreateOrder(model, userEmail);
            await unitOfWork.SaveChangesAsync();
            return new BaseResponse<OrderEntity> { Data = result, StatusCode = 200, Success = true };
        }

        public async Task<BaseResponse<ICollection<OrderEntity>>> GetOrders(string userEmail)
        {
            var result = await unitOfWork.Order.GetOrders(userEmail);
            return new BaseResponse<ICollection<OrderEntity>> { Data = result, StatusCode = 200, Success = true };
        }
    }
}