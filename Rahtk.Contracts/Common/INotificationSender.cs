namespace Rahtk.Contracts.Common
{
	public interface INotificationSender
	{
		Task SendNotification(string deviceToken,string messageBody);
	}
}

