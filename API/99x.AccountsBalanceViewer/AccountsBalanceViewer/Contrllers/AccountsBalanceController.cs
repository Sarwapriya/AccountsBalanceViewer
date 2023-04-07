using AccountsBalanceViewer.Application.Features.AccountsBalance.Commands.AddAccountsBalance;
using AccountsBalanceViewer.Application.Features.AccountsBalance.Queries.GetAccountsBalance;
using CsvHelper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Formats.Asn1;
using System.Globalization;

namespace AccountsBalanceViewer.API.Contrllers
{
    [Authorize]
    [Route("api/accountBalance")]
    [ApiController]
    public class AccountsBalanceController : ControllerBase
    {
        #region Fileds
        private readonly IMediator _mediator;
        #endregion

        #region Constructor
        public AccountsBalanceController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #endregion
        // GET: AccountsBalanceController

        #region API
        [HttpGet("{year}/{month}/get-account-balance")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAccountBalanceQueryVm))]
        public async Task<ActionResult<IList<GetAccountBalanceQueryVm>>> GetAccountBalanceByMonth([FromQuery] GetAccountBalanceQuery accBalance)
        {
            var accountBalances = await _mediator.Send(accBalance);
            return Ok(accountBalances);
        }

        // POST: AccountsBalanceController/Create
        [HttpPost("add-account-balance")]
        [ProducesResponseType(200)]
        public async Task<ActionResult> AddAccountBalance(IFormFile fileAccountBalances)
        {

            if (fileAccountBalances == null || fileAccountBalances.Length == 0)
                return BadRequest("No file selected.");

            var fileExtension = Path.GetExtension(fileAccountBalances.FileName);

            if (fileExtension != ".xlsx" && fileExtension != ".csv")
                return BadRequest("Invalid file type.");

            // Save the file to the server
            var filePath = Path.Combine(Path.GetTempPath(), fileAccountBalances.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await fileAccountBalances.CopyToAsync(stream);
            }

            // Read the file using EPPlus or CsvHelper
            if (fileExtension == ".xlsx")
            {
                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    var rows = worksheet.Dimension.Rows;
                    var columns = worksheet.Dimension.Columns;
                    var data = new List<object[]>();

                    for (int row = 1; row <= rows; row++)
                    {
                        var rowData = new object[columns];
                        for (int col = 1; col <= columns; col++)
                        {
                            rowData[col - 1] = worksheet.Cells[row, col].Value;
                        }
                        data.Add(rowData);
                    }

                    return Ok(data);
                }
            }
            else if (fileExtension == ".csv")
            {
                using (var reader = new StreamReader(filePath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var data = csv.GetRecords<object[]>().ToList();
                    return Ok(data);
                }
            }

            return BadRequest("Invalid file type.");






            //var result = await _mediator.Send(accountBalances);
            //return Ok(result);
        }
        #endregion
    }
}
