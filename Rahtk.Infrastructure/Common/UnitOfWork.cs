using Rahtk.Contracts.Common;
using Rahtk.Contracts.Features;
using Rahtk.Contracts.Features.Address;
using Rahtk.Contracts.Features.Order;
using Rahtk.Contracts.Features.Payment;
using Rahtk.Contracts.Features.Product.Prodcut;
using Rahtk.Contracts.Features.Reminder;
using Rahtk.Contracts.Features.User;
using Rahtk.Infrastructure.EF.Contexts;

namespace Rahtk.Infrastructure.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUserRepository Users { get; }
        public ICategoryRepository Category { get; }
        public IProductRepository Product { get; }
        public IAddressRepository Address { get; }
        public IPaymentRepository Payment { get; }
        public IOrderRepository Order { get; }
        public IReminderRepository Reminder { get; }

        private readonly RahtkContext _context;

        public UnitOfWork(
            RahtkContext context,
            IUserRepository users,
            ICategoryRepository category,
            IProductRepository product,
            IAddressRepository address,
            IPaymentRepository payment,
            IOrderRepository order,
            IReminderRepository reminder
        )
        {
            _context = context;
            Users = users;
            Category = category;
            Product = product;
            Address = address;
            Payment = payment;
            Order = order;
            Reminder = reminder;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}