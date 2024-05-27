using System.ComponentModel.DataAnnotations.Schema;

namespace Rahtk.Domain.Features.User
{
	public class PaymentOptionEntity
	{
        public int Id { get; set; }

        public string CVV { get; set; } = string.Empty;

        public string CardHolder { get; set; } = string.Empty;

        public string ExpirationDate { get; set; } = string.Empty;

        public string CardNumber { get; set; } = string.Empty;

        [ForeignKey("RahtkUserId")]
        public string RahtkUserId { get; set; }
    }
}