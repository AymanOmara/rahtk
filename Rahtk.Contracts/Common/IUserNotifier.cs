using System;
namespace Rahtk.Contracts.Common
{
    public interface IUserNotifier
    {
        Task Notify(string channel, string body);
    }
}

