using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models.DTO;
using WebApplication1.Models;
using WebApplication1.Models.Domain;
using WebApplication1.Repositories;
using Microsoft.Identity.Client;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequestDto request)
        {
            var category = new Category
            {
                Name = request.Name,
                UrlHandle = request.UrlHandle
            };

            await categoryRepository.CreateAsync(category);

            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };

            return Ok(response);
        }

        // GET https://localhost:7279/api/Categories
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await categoryRepository.GetAllAsync();

            // Map Domain Model to DTO
            var response = new List<CategoryDto>();
            foreach (var category in categories)
            {
                response.Add(new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    UrlHandle = category.UrlHandle
                });
            }

            return Ok(response);
        }
    }
}
