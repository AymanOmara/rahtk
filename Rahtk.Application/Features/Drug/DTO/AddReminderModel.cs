namespace Rahtk.Application.Features.Drug.DTO
{
	public class AddReminderModel
	{
        public int DrugId { get; set; }

        public int ReminderIntervalDays { get; set; }
    }
}