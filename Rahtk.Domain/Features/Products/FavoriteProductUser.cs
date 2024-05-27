using Rahtk.Domain.Features.User;

namespace Rahtk.Domain.Features.Products
{
	public class FavoriteProductUser
	{

		public int Id { get; set; }

        public string UserId { get; set; }

        public RahtkUser? User { get; set; }

        public int ProductId { get; set; }

        public ProductEntity? Product { get; set; }

    }
}

