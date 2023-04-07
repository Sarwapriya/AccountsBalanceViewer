using AccountsBalanceViewer.Application.Contracts.Persistance;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsBalanceViewer.Application.UnitTests
{
    public static class RepositoryMock
    {
        public static Mock<IBalanceRepository> GetAccountBalanceByMonth()
        {
            return new Mock<IBalanceRepository>();
        }
    }
}
