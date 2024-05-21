using Rahtk.Contracts.Features;
using Rahtk.Contracts.Features.products.Prodcut;
using Rahtk.Contracts.Features.User;

namespace Rahtk.Contracts.Common
{
	public interface IUnitOfWork
	{
        public IUserRepository Users
        {
            get;
        }

        public ICategoryRepository Category
        {
            get;
        }

        public IProductRepository Product
        {
            get;
        }

    }
}

