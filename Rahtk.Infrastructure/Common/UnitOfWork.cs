using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Rahtk.Contracts.Common;
using Rahtk.Contracts.Features;
using Rahtk.Contracts.Features.products.Prodcut;
using Rahtk.Contracts.Features.User;
using Rahtk.Domain.Features.User;
using Rahtk.Infrastructure.EF.Contexts;
using Rahtk.Infrastructure.EF.Repositories;
using Rahtk.Shared.Localization;

namespace Rahtk.Infrastructure.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUserRepository Users { get; private set; }

        public ICategoryRepository Category { get; private set; }

        public IProductRepository Product { get; private set; }

        private readonly RahtkContext _context;
        public readonly UserManager<RahtkUser> _userManager;
        public readonly SignInManager<RahtkUser> _signInManager;
        private readonly LanguageService _localization;
        private readonly IConfiguration _configuration;
        private readonly IUserNotifier _userNotifier;
        private readonly IFileService _fileService;
        public UnitOfWork(LanguageService localization, RahtkContext context, UserManager<RahtkUser> userManager, SignInManager<RahtkUser> signInManager, IConfiguration configuration, IUserNotifier userNotifier, IFileService fileService)
        {
            _localization = localization;
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _userNotifier = userNotifier;
            _fileService = fileService;
            InitializeRepositories();
        }
        private void InitializeRepositories()
        {
            Users = new UserRepository(context:_context,userManager:_userManager,signInManager:_signInManager,localization:_localization,configuration:_configuration, _userNotifier);

            Category = new CategoryRepository(_context, _localization, _fileService);

            Product = new ProductRepository(_context,_fileService);
        }
    }
}

