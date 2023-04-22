using AccountsBalanceViewer.Application.Features.AccountsBalance.Commands.AddAccountsBalance;
using AccountsBalanceViewer.Application.Features.AccountsBalance.Queries.GetAccountsBalance;
using CsvHelper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using OfficeOpenXml;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;

namespace AccountsBalanceViewer.API.Contrllers
{
    //[Authorize]
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
        [HttpGet("get-account-balance")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAccountBalanceQueryVm))]
        public async Task<ActionResult<IList<GetAccountBalanceQueryVm>>> GetAccountBalanceByMonth([FromQuery] GetAccountBalanceQuery accBalance)
        {
            var accountBalances = await _mediator.Send(accBalance);
            return Ok(accountBalances);
        }

        // POST: AccountsBalanceController/Create
        [HttpPost("add-account-balance")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<AddAccountBalanceCommandVm>))]
        public async Task<ActionResult<IList<AddAccountBalanceCommandVm>>> AddAccountBalance(IFormFile fileAccountBalances)
        {

            if (fileAccountBalances == null || fileAccountBalances.Length == 0)
                return BadRequest("No file selected.");

            var fileExtension = Path.GetExtension(fileAccountBalances.FileName);

            if (fileExtension != ".xlsx" && fileExtension != ".txt")
                return BadRequest("Invalid file type.");

            // Save the file to the server
            var filePath = Path.Combine(Path.GetTempPath(), fileAccountBalances.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await fileAccountBalances.CopyToAsync(stream);
            }
            List<AddAccountBalanceCommandVm> returnResultList = new List<AddAccountBalanceCommandVm>();
            List<AddAccountBalanceCommand> balanceCommandList = new List<AddAccountBalanceCommand>();
            // Read the file using EPPlus or CsvHelper
            if (fileExtension == ".xlsx")
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
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
                    for (int i = 0; i < data[1].Length; i++)
                    {
                        balanceCommandList.Add(new AddAccountBalanceCommand()
                        {
                            AccountType = data[0][i].ToString(),
                            Amount = Convert.ToDecimal(data[1][i].ToString())
                        });
                    }
                    //var result = await _mediator.Send(balanceCommandList);
                    //return Ok(result);
                }
            }
            else if (fileExtension == ".txt")
            {
                var sepList = new List<string>();

                // Read the file and display it line by line.
                using (var file = new StreamReader(filePath))
                {
                    string line;
                    while ((line = file.ReadLine()) != null)
                    {
                        var delimiters = new char[] { '\t' };
                        string[] segments = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

                        for (int i = 0; i < segments.Length; i++)
                        {
                            AddAccountBalanceCommand balanceItem = new AddAccountBalanceCommand();
                            if (int.TryParse(segments[i], out _))
                            {
                                balanceCommandList[i].Amount = Convert.ToDecimal(segments[i].ToString());
                            }
                            else
                            {
                                balanceItem.AccountType = segments[i].ToString();
                                balanceCommandList.Add(balanceItem);
                            }
                        }
                    }
                    file.Close();
                    //var result = await _mediator.Send(balanceCommandList);
                    //return Ok(result);
                }

            }
            if (balanceCommandList != null)
            {
                foreach (var item in balanceCommandList)
                {
                    var result = await _mediator.Send(item);
                    returnResultList.Add(result);
                }
                return Ok(returnResultList);
            }
            return BadRequest("Invalid file type.");
        }
        #endregion
    }
}
