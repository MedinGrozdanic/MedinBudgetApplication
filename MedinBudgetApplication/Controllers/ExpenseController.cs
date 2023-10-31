using API.DTOs;
using DAL;
using DAL.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs;
using Service.Services;
using Service.Services.ServiceInterfaces;
using System.IdentityModel.Tokens.Jwt;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _services;
        
        public ExpenseController(IExpenseService service)
        {
            _services = service;
        }

        Guid CheckAuth()
        {
            var BearerTkn = Request.Headers.Authorization.ToString();
            BearerTkn = BearerTkn.Remove(0, 7);

            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(BearerTkn);
            var userIdAString = jwt.Subject;
            return Guid.Parse(userIdAString);
        }

        [HttpGet]
        [Authorize]
        public IActionResult ListExpenses()
        {
            var userId = CheckAuth();
          
            var listExpensesDTOs = new List<GetExpenseDTO>();
            var listExpenses = _services.ListExpenses(userId);

            try
            {
                foreach (var expense in listExpenses)
                {
                    var listCategories = new List<string>();
                    listCategories.Add(expense.Category.CategoryName);
                    listExpensesDTOs.Add(new GetExpenseDTO()
                    {
                        ExpenseId = expense.ExpenseId,
                        Amount = expense.Amount,
                        Comment = expense.Comment,
                        Receiver = expense.Receiver,
                        TimeStamp = expense.DateStamp,
                        CategoryName = listCategories
                    });
                }
                return Ok(listExpensesDTOs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateExpense(CreateExpenseDTO expenseDTO)
        {
            var userId = CheckAuth();
            expenseDTO.UserId = userId;

            try
            {
                _services.CreateExpense(
                    expenseDTO.Amount,
                    expenseDTO.Receiver,
                    expenseDTO.TimeStamp,
                    expenseDTO.Comment,
                    expenseDTO.UserId,
                    expenseDTO.CategoryName
                    );

                return Created("api/", expenseDTO);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpDelete("{expenseId}")]
        [Authorize]
        public async Task<IActionResult> DeleteExpense([FromRoute] Guid expenseId)
        {
           
            var expense = await _services.FindExpense(expenseId);
            if (expense == null)
            {
                return NotFound();
            }

            try
            {
                await _services.DeleteExpense(expenseId);
                return Ok();
            }
            catch
            {
                return Conflict();
            }
        }

        [HttpPut("{expenseId}")]
        [Authorize]
        public async Task<IActionResult> EditExpense([FromRoute] Guid expenseId, EditExpense expenseDTO)
        {
            var expense = await _services.FindExpense(expenseId);
            if (expense == null)
            {
                return NotFound();
            }
            try
            {
                await _services.EditExpense(expenseId, expenseDTO);
                return Ok();
            }
            catch
            {
                return Conflict(expense);
            }
        }



        [HttpGet("/PieChart")]
        [Authorize]
        public PieChartDTO GetCategoriesSpentSum()
        {
            var userId = CheckAuth();

            return _services.GetSumPerCategory(userId);
        }

        [HttpGet("TotalSumsSpent")]
        [Authorize]
        public SpentThisYearAndMonthDTO TotalSumsSpentMonthAndYear()
        {
            var userId = CheckAuth();

            return _services.GetSpentThisYearAndMonth(userId);
        }
        [HttpGet("/SpentPerMonth")]
        [Authorize]
        public SumsSpentPerMonthDTO GetSumSpentEveryMonth()
        {
            var userId = CheckAuth();
            return _services.GetSumsSpentPerMonthThisYear(userId);
        }

       

    }

}
