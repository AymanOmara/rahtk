using Rahtk.Domain.Features.User;
using Rahtk.Shared.Models;

namespace Rahtk.Application.Features
{
	public interface IUserService
	{
        Task<BaseResponse<bool>> CreateUser(RegistrationDTO registration);



    }
}

