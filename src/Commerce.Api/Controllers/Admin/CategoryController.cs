using AutoMapper;
using Commerce.Core.Domain.Content;
using Commerce.Core.Models;
using Commerce.Core.Models.Content;
using Commerce.Core.SeedWorks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Commerce.Api.Controllers.Admin
{
    [Route("api/admin/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CategoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateUpdateCategoryRequest request)
        {
            var data = _mapper.Map<CreateUpdateCategoryRequest, Category>(request);
            _unitOfWork.Categories.Add(data);
            var result = await _unitOfWork.CompleteAsync();
            return result > 0 ? Ok() : BadRequest();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] CreateUpdateCategoryRequest request)
        {
            var data = await _unitOfWork.Categories.GetByIdAsync(id);
            if (data == null)
            {
                return NotFound();
            }
            _mapper.Map(request, data);

            var result = await _unitOfWork.CompleteAsync();
            return result > 0 ? Ok() : BadRequest();
        }
        [HttpGet("all/item")]
        public async Task<ActionResult<List<CategoryDto>>> GetAllCategory()
        {
            var data = await _unitOfWork.Categories.GetAllAsync();
            return Ok(data);

        }
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteCategory([FromQuery] Guid[] ids)
        {
            foreach (var id in ids)
            {
                var data = await _unitOfWork.Categories.GetByIdAsync(id);
                if (data == null)
                {
                    return NotFound();
                }
                if (await _unitOfWork.Categories.HasPost(id))
                {
                    return BadRequest("Danh mục đang chứa bài viết, không thể xóa");
                }
                _unitOfWork.Categories.Remove(data);
            }
            var result = await _unitOfWork.CompleteAsync();
            return result > 0 ? Ok() : BadRequest();
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<CategoryDto>> GetRoomCategoryId(Guid id)
        {
            var data = await _unitOfWork.Categories.GetByIdAsync(id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpGet]
        [Route("paging")]
        public async Task<ActionResult<PagedResult<CategoryDto>>> GetRoomCategoryPaging(string? keyword, int pageIndex, int pageSize = 10)
        {
            var result = await _unitOfWork.Categories.GetCategoryPagingAsync(keyword, pageIndex, pageSize);
            return Ok(result);
        }
    }
}
