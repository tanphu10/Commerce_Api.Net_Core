using AutoMapper;
using Azure;
using Commerce.Api.Extentisions;
using Commerce.Core.Domain.Content;
using Commerce.Core.Domain.Identity;
using Commerce.Core.Models;
using Commerce.Core.Models.Content;
using Commerce.Core.SeedWorks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Net;
using System.Text.Json;

namespace Commerce.Api.Controllers.Admin
{
    [Route("api/admin/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        public ProductController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }
        [HttpPost]
        //[Authorize(Posts.Create)]
        public async Task<IActionResult> CreateProduct([FromBody] CreateUpdateProductRequest request)
        {

            if (await _unitOfWork.Products.IsSlugAlreadyExisted(request.Slug))
            {
                return BadRequest("Đã tồn tại slug");
            }
            var product = _mapper.Map<CreateUpdateProductRequest, Product>(request);
            _unitOfWork.Products.Add(product);
            var result = await _unitOfWork.CompleteAsync();
            return result > 0 ? Ok() : BadRequest();
        }
        [HttpPut]
        public async Task<IActionResult> UpdatePost(Guid id, [FromBody] CreateUpdateProductRequest request)
        {
            if (await _unitOfWork.Products.IsSlugAlreadyExisted(request.Slug, id))
            {
                return BadRequest("Đã tồn tại slug");
            }
            var post = await _unitOfWork.Products.GetByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            if (post.CategoryId != request.CategoryId)
            {
                var category = await _unitOfWork.Categories.GetByIdAsync(request.CategoryId);
                post.CategoryName = category.Name;
                post.CategorySlug = category.Slug;
            }
            _mapper.Map(request, post);

            await _unitOfWork.CompleteAsync();

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePosts([FromQuery] Guid[] ids)
        {
            foreach (var id in ids)
            {
                var post = await _unitOfWork.Products.GetByIdAsync(id);
                if (post == null)
                {
                    return NotFound();
                }
                _unitOfWork.Products.Remove(post);
            }
            var result = await _unitOfWork.CompleteAsync();
            return result > 0 ? Ok() : BadRequest();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ProductDto>> GetPostById(Guid id)
        {
            var post = await _unitOfWork.Products.GetByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }

        [HttpGet]
        [Route("paging")]
        public async Task<ActionResult<PagedResult<ProductInListDto>>> GetProductPaging(string? keyword, Guid? categoryId,
            int pageIndex, int pageSize = 10)
        {
            var userId = User.GetUserId();
            var result = await _unitOfWork.Products.GetAllPaging(keyword, userId, categoryId, pageIndex, pageSize);
            return Ok(result);
        }

        //[HttpGet]
        //[Route("series-belong/{postId}")]
        //[Authorize(Posts.View)]
        //public async Task<ActionResult<List<SeriesInListDto>>> GetSeriesBelong(Guid postId)
        //{
        //    var result = await _unitOfWork.Posts.GetAllSeries(postId);
        //    return Ok(result);
        //}

        [HttpGet("approve/{id}")]
        public async Task<IActionResult> ApprovePostProduct(Guid id)
        {
            await _unitOfWork.Products.Approve(id, User.GetUserId());
            await _unitOfWork.CompleteAsync();
            return Ok();
        }

        [HttpGet("approval-submit/{id}")]
        public async Task<IActionResult> SendToApprove(Guid id)
        {
            await _unitOfWork.Products.SendToApprove(id, User.GetUserId());
            await _unitOfWork.CompleteAsync();
            return Ok();
        }

        [HttpPost("return-back/{id}")]
        public async Task<IActionResult> ReturnBack(Guid id, [FromBody] ReturnBackRequest model)
        {
            await _unitOfWork.Products.ReturnBack(id, User.GetUserId(), model.Reason);
            await _unitOfWork.CompleteAsync();
            return Ok();
        }

        [HttpGet("return-reason/{id}")]
        public async Task<ActionResult<string>> GetReason(Guid id)
        {
            var note = await _unitOfWork.Products.GetReturnReason(id);
            return Ok(note);
        }

        [HttpGet("activity-logs/{id}")]
        //[Authorize(Posts.Approve)]
        public async Task<ActionResult<List<ProductActivityLogDto>>> GetActivityLogs(Guid id)
        {
            var logs = await _unitOfWork.Products.GetActivityLogs(id);
            return Ok(logs);
        }


    }
}
