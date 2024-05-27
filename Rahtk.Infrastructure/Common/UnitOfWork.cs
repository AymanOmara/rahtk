using Hangfire;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Rahtk.Contracts.Common;
using Rahtk.Contracts.Features;
using Rahtk.Contracts.Features.Address;
using Rahtk.Contracts.Features.Drug;
using Rahtk.Contracts.Features.Order;
using Rahtk.Contracts.Features.Payment;
using Rahtk.Contracts.Features.products.Prodcut;
using Rahtk.Contracts.Features.User;
using Rahtk.Domain.Features.User;
using Rahtk.Infrastructure.EF.Contexts;
using Rahtk.Infrastructure.EF.Repositories;
using Rahtk.Infrastructure.EF.Services;
using Rahtk.Shared.Localization;

namespace Rahtk.Infrastructure.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUserRepository Users { get; private set; }

        public ICategoryRepository Category { get; private set; }

        public IProductRepository Product { get; private set; }

        public IAddressRepository Address { get; private set; }

        public IPaymentRepository Payment { get; private set; }

        public IOrderRepository Order { get; private set; }

        public IDrugRepository Drug { get; private set; }

        private readonly RahtkContext _context;
        public readonly UserManager<RahtkUser> _userManager;
        public readonly SignInManager<RahtkUser> _signInManager;
        private readonly LanguageService _localization;
        private readonly IConfiguration _configuration;
        private readonly IUserNotifier _userNotifier;
        private readonly IFileService _fileService;
        private readonly INotificationSender _sender;
        private readonly IReminderService _reminderService;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IRecurringJobManager _recurringJobManager;
        public UnitOfWork(LanguageService localization, RahtkContext context, UserManager<RahtkUser> userManager, SignInManager<RahtkUser> signInManager, IConfiguration configuration, IUserNotifier userNotifier, IFileService fileService, INotificationSender sender, IBackgroundJobClient backgroundJobClient, IRecurringJobManager recurringJobManager)
        {
            _localization = localization;
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _userNotifier = userNotifier;
            _fileService = fileService;
            _sender = sender;
            _backgroundJobClient = backgroundJobClient;
            _recurringJobManager = recurringJobManager;
            _reminderService = new ReminderService(_context,_backgroundJobClient, _recurringJobManager, _sender);
            InitializeRepositories();
        }
        private void InitializeRepositories()
        {
            Users = new UserRepository(context:_context,userManager:_userManager,signInManager:_signInManager,localization:_localization,configuration:_configuration, _userNotifier);

            Category = new CategoryRepository(_context, _localization, _fileService, _userManager, _sender);

            Product = new ProductRepository(_context,_fileService,_userManager);

            Address = new AddressRepository(_context, _userManager);

            Payment = new PaymentRepository(_context, _userManager);

            Order = new OrderRepository(_context, _userManager);

            Drug = new DrugRepository(_context, _userManager, _fileService, _reminderService);
        }
    }
}