using Rahtk.Domain.Features.User;
using Rahtk.Shared.Models;

namespace Rahtk.Contracts.Features.User
{
	public interface IUserRepository
	{
        Task<Result<string, Exception>> CreateUser(RegistrationDTO registration);
    }
}

