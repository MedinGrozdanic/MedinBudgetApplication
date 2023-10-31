using API.DTOs;
using Microsoft.AspNetCore.Mvc;
using Service.Services;
using Service.Services.ServiceInterfaces;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _services;

        public CategoryController(ICategoryService service)
        {
            _services = service;
        }

        [HttpGet]
        public IActionResult ListCategories()
        {
            var listCategoriesDTOs = new List<CategoryDTO>();
            var listCategories = _services.ListCategories();

            try
            {
                foreach (var category in listCategories)
                {
                    listCategoriesDTOs.Add(new CategoryDTO
                    {
                        CategoryName = category.CategoryName,
                        CategoryId = category.CategoryId
                        
                    });
                }
                return Ok(listCategoriesDTOs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
