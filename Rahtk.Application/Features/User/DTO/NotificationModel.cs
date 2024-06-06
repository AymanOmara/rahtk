using System;
namespace Rahtk.Application.Features.User
{
	public class NotificationModel
	{
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Date { get; set; } = string.Empty;

        public string Body { get; set; } = string.Empty;
    }
}

