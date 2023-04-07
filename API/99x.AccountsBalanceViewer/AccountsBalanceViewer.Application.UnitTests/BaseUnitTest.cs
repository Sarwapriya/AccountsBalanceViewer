using AccountsBalanceViewer.Application.Profiles;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsBalanceViewer.Application.UnitTests
{
    public class BaseUnitTest
    {
        #region MyRegion
        protected readonly IMapper _mapper;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseUnitTest"/> class.
        /// </summary>
        public BaseUnitTest()
        {
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
