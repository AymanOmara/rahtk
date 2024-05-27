using Rahtk.Application.Features.Drug.DTO;
using Rahtk.Domain.Features.Pharmacy;
using Rahtk.Shared.Models;

namespace Rahtk.Application.Features.Drug
{
	public interface IDrugService
	{
		Task<BaseResponse<ICollection<DrugEntity>>> GetDrugs();

        Task<BaseResponse<DrugEntity>> AddDrug(AddDrugModel drugModel);

		Task<BaseResponse<bool>> AddReminder(AddReminderModel reminder, string userEmail);
    }
}

