namespace Rahtk.Domain.Features.Pharmacy
{
	public class DrugEntity
	{
		public int Id { get; set; }

		public string ImagePath { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;
	}
}