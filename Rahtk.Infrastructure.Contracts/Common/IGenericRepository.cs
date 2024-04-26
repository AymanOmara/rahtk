namespace Rahtk.Infrastructure.Contracts.Common
{
	public interface IGenericRepository<T>
	{
		Task<T> Create(T value);

		//Task<> 
	}
}

