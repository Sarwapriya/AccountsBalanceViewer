using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsBalanceViewer.Domain.Entities
{
    public class Balance : BaseEntity
    {
        public string Month { get; set; }
        public string Year { get; set; }
        public decimal Amount { get; set; }
        public long AccountTypeId { get; set; }
        public AccountType AccountType { get; set; }
    }
}
