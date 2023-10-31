using API.DTOs;
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
    public class BudgetController : ControllerBase
    {
        private readonly IBudgetService _services;
        public BudgetController(IBudgetService service)
        {
            _services = service;
        }

        [HttpGet("{budgetId}")]
        public IActionResult GetBudget(Guid budgetId)
        {
            var result = _services.GetBudget(budgetId);
            try
            {
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        
        [HttpPost]
        public IActionResult PostBudget(CreateBudgetDTO createBudgetDTO)
        {
            _services.CreateBudget(createBudgetDTO);

            return Ok();
        }

        Guid CheckAuth()
        {
            var BearerTkn = Request.Headers.Authorization.ToString();
            BearerTkn = BearerTkn.Remove(0, 7);

            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(BearerTkn);
            var userIdAString = jwt.Subject;
            return Guid.Parse(userIdAString);
        }


        [HttpGet("histogram")]
        [Authorize]
        public IActionResult GetBudgetHistogramValues()
        {
            var userId = CheckAuth();
            try
            {
               var histogramBudgetValues = _services.GetBudgetForHistogram(userId);
                return Ok(histogramBudgetValues);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

