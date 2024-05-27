using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rahtk.Contracts.Common;
using Rahtk.Contracts.Features.Drug;
using Rahtk.Domain.Features.Pharmacy;
using Rahtk.Domain.Features.User;
using Rahtk.Infrastructure.EF.Contexts;
using Rahtk.Infrastructure.EF.Services;

namespace Rahtk.Infrastructure.EF.Repositories
{
    public class DrugRepository : IDrugRepository
    {
        private readonly RahtkContext _context;
        public readonly UserManager<RahtkUser> _userManager;
        private readonly IFileService _fileService;
        private readonly IReminderService _reminderService;
        public DrugRepository(RahtkContext context, UserManager<RahtkUser> userManager, IFileService fileService, IReminderService reminderService)
        {
            _context = context;

            _userManager = userManager;

            _fileService = fileService;

            _reminderService = reminderService;
        }

        public async Task<DrugEntity> AddDrug(DrugEntity entity, IFormFile file)
        {
            var path = await _fileService.SaveFileAsync(file);
            entity.ImagePath = path;
            await _context.Drugs.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<ICollection<DrugEntity>> GetDrugs()
        {
            var result = await _context.Drugs.ToListAsync();
            return result;
        }

        public async Task<ReminderEntity> AddReminder(ReminderEntity reminder, string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);

            reminder.UserId = user.Id;

            var drug = await _context.Drugs.FindAsync(reminder.DrugId);

            reminder.DrugId = drug.Id;

            await _context.Reminders.AddAsync(reminder);

            await _context.SaveChangesAsync();

            await _reminderService.ScheduleReminder(reminder);

            return reminder;
        }
    }
}

