using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rahtk.Contracts.Features.Order;
using Rahtk.Domain.Features.Order;
using Rahtk.Domain.Features.User;
using Rahtk.Infrastructure.EF.Contexts;

namespace Rahtk.Infrastructure.EF.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly RahtkContext _context;
        private readonly UserManager<RahtkUser> _userManager;
        public OrderRepository(RahtkContext context, UserManager<RahtkUser> userManager)
        {
            _context = context;

            _userManager = userManager;
        }

        public async Task<OrderEntity> CreateOrder(CreateOrderModel order, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var allProducts = await _context.Products.ToListAsync();
            var allDrugs = await _context.Drugs.ToListAsync();
            List<OrderItemEntity> selectedProduct = new();
            List<OrderDrugItemEntity> selectedDrugs = new();

            foreach (var element in order?.OrderItemModel)
            {
                var isFound = allProducts.FirstOrDefault(pro => pro.Id == element.ProductId);
                if (isFound != null)
                {
                    isFound.PurchasementCount += 1;
                    _context.Entry(isFound).State = EntityState.Modified;
                    selectedProduct.Add(new OrderItemEntity { ProductId = element.ProductId, ProductCounter = element.ProductCount });
                }
            }
            foreach (var element in order?.DrugItemModel)
            {
                var isFound = allDrugs.FirstOrDefault(pro => pro.Id == element.ProductId);
                if (isFound != null)
                {
                    
                    _context.Entry(isFound).State = EntityState.Modified;
                    selectedDrugs.Add(new OrderDrugItemEntity { DrugId = element.ProductId, DrugCounter = element.ProductCount });
                }
            }
            var orderEntity = new OrderEntity { Items = selectedProduct, PaymentMethod = order.PaymentMethod, PaymentOptionId = order.PaymentId, AddressId = order.AddressId, UserId = user.Id ,Drugs = selectedDrugs };
            await _context.Orders.AddAsync(orderEntity);
            await _context.SaveChangesAsync();
            return orderEntity;
        }

        public async Task<ICollection<OrderEntity>> GetOrders(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var result = await _context.Orders?.Include(or => or.Items)?.ThenInclude(pr => pr.Product)?.Include(dr => dr.Drugs)?.ThenInclude(dr => dr.Drug).Include(add => add.Address).Include(pay => pay.Payment).Where(or => or.UserId == user.Id).ToListAsync();
            return result;
        }
    }
}

