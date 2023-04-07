using AccountsBalanceViewer.Application.Profiles;
using AccountsBalanceViewer.Persistance;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AccountsBalanceViewer.Infrastructure.IntegrationTests
{
    public class BaseUnitTest
    {
        #region MyRegion
        protected readonly IMapper _mapper;
        protected readonly AccountsBalanceViewerContext _accountsBalanceViewerContext;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseUnitTest"/> class.
        /// </summary>
        public BaseUnitTest()
        {
            //set the automapper profile
            var dbContextOptions = new DbContextOptionsBuilder<AccountsBalanceViewerContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
            .Options;

            _accountsBalanceViewerContext = new AccountsBalanceViewerContext(dbContextOptions);

            //set the automapper profile
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configurationProvider.CreateMapper();
        }
        #endregion
    }
}