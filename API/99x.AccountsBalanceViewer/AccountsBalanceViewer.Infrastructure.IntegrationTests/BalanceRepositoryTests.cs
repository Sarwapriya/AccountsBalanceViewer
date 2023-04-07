using AccountsBalanceViewer.Application.Contracts.Persistance;
using AccountsBalanceViewer.Application.Features.AccountsBalance.Queries.GetAccountsBalance;
using AccountsBalanceViewer.Domain.Entities;
using AccountsBalanceViewer.Persistance.Repositories;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AccountsBalanceViewer.Infrastructure.IntegrationTests
{
    public class BalanceRepositoryTests : BaseUnitTest
    {
        #region Fields
        private readonly IRepository<Balance> _balanceRepository;
        #endregion
        #region Constructor
        public BalanceRepositoryTests()
        {
            _balanceRepository = new Repository<Balance>(_accountsBalanceViewerContext);
        }
        #endregion
        #region TestMethods
        [Fact]
        [Trait("Feature", "GetAccountBalanceQueryHandler")]
        public async Task Should_Return_AccountBalances_When_RecoredExistOnDb()
        {
            //Arrenge
            var testData = new List<Balance>
            {
                new Balance
                {
                    Id= 1,
                        AccountTypeId = 1,
                        Year = "2023",
                        Month = "01",
                        Amount = 2000
                }
            };
            _accountsBalanceViewerContext.Balances.AddRange(testData);
            _accountsBalanceViewerContext.SaveChanges();


            //Act
            var result = await _balanceRepository.GetAsync();
            //Asset
            result.Count().ShouldBe(1);
        }
        [Fact]
        [Trait("Feature", "GetAccountBalanceQueryHandler")]
        public async Task Should_Return_EmptyAccountBalances_When_NoRecordsOnDb()
        {
            //Arrenge

            //Act
            var result = await _balanceRepository.GetAsync();
            //Asset
            result.Count().ShouldBe(0);
        }
        #endregion
    }
}
