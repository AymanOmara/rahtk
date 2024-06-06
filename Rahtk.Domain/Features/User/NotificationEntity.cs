using System.ComponentModel.DataAnnotations.Schema;

namespace Rahtk.Domain.Features.User
{
	public class NotificationEntity
	{
		public int Id { get; set; }

		public string Title { get; set; }

        public string Body { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

		public RahtkUser User { get; set; }

		[ForeignKey("UserId")]
		public string UserId { get; set; }

	}
}

