using Rahtk.Contracts.Common;
using Rahtk.Domain.Features.Order;
using Rahtk.Shared.Localization;
using Rahtk.Shared.Models;

namespace Rahtk.Application.Features.Order
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly LanguageService _languageService;
        public OrderService(IUnitOfWork unitOfWork, LanguageService languageService)
        {
            _unitOfWork = unitOfWork;

            _languageService = languageService;
        }

        public async Task<BaseResponse<OrderEntity>> CreateOrder(CreateOrderModel model, string userEmail)
        {
            var result = await _unitOfWork.Order.CreateOrder(model, userEmail);
            return new BaseResponse<OrderEntity> { data = result, statusCode = 200, success = true };
        }

        public async Task<BaseResponse<ICollection<OrderEntity>>> GetOrders(string userEmail)
        {
            var result = await _unitOfWork.Order.GetOrders(userEmail);
            return new BaseResponse<ICollection<OrderEntity>> { data = result, statusCode = 200, success = true };
        }
    }
}

