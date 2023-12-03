﻿using CodePulseAPI.Data;
using CodePulseAPI.Models.Domain;
using CodePulseAPI.Models.DTO;
using CodePulseAPI.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace CodePulseAPI.Controllers
{
    // https://localhost:xxxx/api/categories
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public ApplicationDbContext DbContext { get; }

        // 
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequestDto request)
        {
            // Map DTO to Domain Models
            var category = new Category
            {
                Name = request.Name,
                UrlHandle = request.UrlHandle,
            };

            await categoryRepository.CreateAsync(category);
                
            // Domain model to DTO
            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle,
            };

            return Ok(response);        
        }
    }
}
