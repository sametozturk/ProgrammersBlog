using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProgrammersBlog.Entities.Dtos;
using ProgrammersBlog.Mvc.Areas.Admin.Models;
using ProgrammersBlog.Services.Abstract;
using ProgrammersBlog.Shared.Utilities.Extensions;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;

namespace ProgrammersBlog.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _categoryService.GetAllByNonDeleted();
            return View(result.Data);
            
        }

        public IActionResult Add()
        {
            return PartialView("_CategoryAddPartial");
        }
        [HttpPost]
        public async Task<IActionResult> Add(CategoryAddDto categoryAddDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _categoryService.Add(categoryAddDto,"Samet Öztürk");
                if (result.ResultStatus==ResultStatus.Success)
                {
                    var categoryAddAjaxModel = JsonSerializer.Serialize(new CategoryAddAjaxModel
                    {
                        CategoryDto = result.Data,
                        //partialviewleri modelleriyle birlikte string olarak return ettim. partialview i modeliyle birlikte json formatında geri döndürüyorum

                        CategoryAddPartial = await this.RenderViewToStringAsync("_CategoryAddPartial", categoryAddDto)
                    });
                    return Json(categoryAddAjaxModel);
                }

            }
            var categoryAddAjaxErrorModel = JsonSerializer.Serialize(new CategoryAddAjaxModel
            {
                CategoryAddPartial = await this.RenderViewToStringAsync("_CategoryAddPartial", categoryAddDto)
            });
            return Json(categoryAddAjaxErrorModel);
        }
        public async Task<IActionResult> Update(int categoryId)
        {
            var result = await _categoryService.GetCategoryUpdateDto(categoryId);
            if (result.ResultStatus==ResultStatus.Success)
            {
                return PartialView("_CategoryUpdatePartial", result.Data);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Update(CategoryUpdateDto categoryUpdateDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _categoryService.Update(categoryUpdateDto, "Samet Öztürk");
                if(result.ResultStatus==ResultStatus.Success)
                {
                    var categoryUpdateAjaxModel = JsonSerializer.Serialize(new CategoryUpdateAjaxModel
                    {
                        CategoryDto = result.Data,
                        CategoryUpdatePartial=await this.RenderViewToStringAsync("_CategoryUpdatePartial",categoryUpdateDto)

                    });
                    return Json(categoryUpdateAjaxModel);
                }
            }
            var categoryUpdateAjaxErrorModel = JsonSerializer.Serialize(new CategoryUpdateAjaxModel
            {
                CategoryUpdatePartial = await this.RenderViewToStringAsync("_CategoryUpdatePartial", categoryUpdateDto)
            });
            return Json(categoryUpdateAjaxErrorModel);
        }

        
        public async Task<JsonResult> GetAllCategories()
        {
            var result = await _categoryService.GetAllByNonDeleted();
            var categories = JsonSerializer.Serialize(result.Data, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });
            return Json(categories);
        }
        [HttpPost]
        public async Task<JsonResult> Delete(int categoryId)
        {
            var result = await _categoryService.Delete(categoryId, "Samet Öztürk");
            var deletedCategory = JsonSerializer.Serialize(result.Data);
            return Json(deletedCategory);
        }
    }
} 
