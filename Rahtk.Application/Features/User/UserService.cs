using Rahtk.Contracts.Common;
using Rahtk.Domain.Features.User;
using Rahtk.Shared.Models;

namespace Rahtk.Application.Features.User
{
	public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
		{
            _unitOfWork = unitOfWork;
		}

        public async Task<BaseResponse<bool>> CreateUser(RegistrationDTO registration)
        {
            var result = await _unitOfWork.Users.CreateUser(registration);
            if (!result.IsOk)
            {
                return new BaseResponse<bool>() { message = result.Error.Message, statusCode = 400 };
            }
            else
            {
                return new BaseResponse<bool>() { message = result.Value, statusCode = 200, success = true, data = true };
            }
        }
    }
}

