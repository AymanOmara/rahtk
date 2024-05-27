namespace Rahtk.Application.Features.Payment.DTO
{
	public class CreatePaymentOptionModel
	{
        public string CVV { get; set; } = string.Empty;

        public string CardHolder { get; set; } = string.Empty;

        public string ExpirationDate { get; set; } = string.Empty;

        public string CardNumber { get; set; } = string.Empty;
    }
}

