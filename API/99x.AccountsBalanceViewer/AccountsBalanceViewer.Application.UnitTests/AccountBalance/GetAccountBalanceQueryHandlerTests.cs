using AccountsBalanceViewer.Application.Contracts.Persistance;
using AccountsBalanceViewer.Application.Features.AccountsBalance.Queries.GetAccountsBalance;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AccountsBalanceViewer.Application.UnitTests.AccountBalance
{
    public class GetAccountBalanceQueryHandlerTests : BaseUnitTest
    {
        #region Fields
        private readonly Mock<IBalanceRepository> _mockBalanceHistoryRepository;
        #endregion
        #region Constructor
        public GetAccountBalanceQueryHandlerTests()
        {
            _mockBalanceHistoryRepository = RepositoryMock.GetAccountBalanceByMonth();
        }
        #endregion
        #region Test Methods
        [Fact]
        [Trait("Feature", "GetAccountBalanceQueryHandler")]
        public async Task Should_Return_AccountBalances_When_Month_And_Year_NotNull()
        {
            //Arrenge
            _mockBalanceHistoryRepository.Setup(m => m.GetBalanceAsync(It.IsAny<GetAccountBalanceQuery>()))
                .ReturnsAsync(new List<GetAccountBalanceQueryVm>
                {
                    new GetAccountBalanceQueryVm
                    {
                        Id= 1,
                        AccountType = "R&D",
                        Year = "2023",
                        Month = "01",
                        Amount = 2000
                    },
                    new GetAccountBalanceQueryVm
                    {
                        Id= 2,
                        AccountType = "Canteen",
                        Year = "2023",
                        Month = "01",
                        Amount = 1000
                    },
                    new GetAccountBalanceQueryVm
                    {
                        Id= 3,
                        AccountType = "CEO's Car",
                        Year = "2023",
                        Month = "01",
                        Amount = 10000
                    }
                });
            //Act
            var handler = new GetAccountBalanceQueryHandler( _mockBalanceHistoryRepository.Object );
            var result = await handler.Handle(new GetAccountBalanceQuery() { Year = "2023", Month = "01" }, CancellationToken.None);
            //Asset
            result.Count.ShouldBe(3);
        }

        [Fact]
        [Trait("Feature", "GetAccountBalanceQueryHandler")]
        public async Task Should_Return_AccountBalances_When_Request_Worng_Month_And_Year()
        {
            //Arrenge
            _mockBalanceHistoryRepository.Setup(m => m.GetBalanceAsync(It.IsAny<GetAccountBalanceQuery>()))
                .ReturnsAsync(new List<GetAccountBalanceQueryVm>
                {
                    new GetAccountBalanceQueryVm
                    {
                        Id= 0,
                        AccountType = string.Empty,
                        Year = string.Empty,
                        Month = string.Empty,
                        Amount = 0
                    }
                });
            //Act
            var handler = new GetAccountBalanceQueryHandler(_mockBalanceHistoryRepository.Object);
            var result = await handler.Handle(new GetAccountBalanceQuery() { Year = "2023", Month = "01" }, CancellationToken.None);
            //Asset
            result.FirstOrDefault().AccountType.ShouldBe(string.Empty);
        }
        #endregion
    }
}
