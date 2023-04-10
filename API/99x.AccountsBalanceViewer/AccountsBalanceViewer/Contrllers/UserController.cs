using AccountsBalanceViewer.Application.Features.User.Queries.GetUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccountsBalanceViewer.API.Contrllers
{
    [Authorize]
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region Fileds
        private readonly IMediator _mediator;
        #endregion
        #region Constructor
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #endregion
        #region API
        // GET: UserController
        [HttpGet("{email}/get-user")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetUserQueryVm))]
        public async Task<ActionResult<GetUserQueryVm>> GetUser([FromQuery] GetUserQuery userRequest)
        {
            var user = await _mediator.Send(userRequest);
            return Ok(user);
        }
        #endregion
    }
}
