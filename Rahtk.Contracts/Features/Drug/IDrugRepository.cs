using Microsoft.AspNetCore.Http;
using Rahtk.Domain.Features.Pharmacy;

namespace Rahtk.Contracts.Features.Drug
{
	public interface IDrugRepository
	{
		Task<ICollection<DrugEntity>> GetDrugs();

        Task<DrugEntity> AddDrug(DrugEntity entity,IFormFile file);

		Task<ReminderEntity> AddReminder(ReminderEntity reminder, string userEmail);
    }
}