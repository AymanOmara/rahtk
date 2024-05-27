using System.ComponentModel.DataAnnotations.Schema;
using Rahtk.Domain.Features.Products;

namespace Rahtk.Domain.Features.Order
{
	public class OrderItemEntity
	{
        public int Id { get; set; }

        public ProductEntity? Product { get; set; }
        [ForeignKey("ProductId")]
        public int? ProductId { get; set; }

        public int ProductCounter { get; set; }
    }
}
