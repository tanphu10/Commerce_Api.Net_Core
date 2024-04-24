using AutoMapper;
using Commerce.Core.Domain.Content;
using Commerce.Core.Domain.Identity;
using Commerce.Core.Models.Content;
using Commerce.Core.SeedWorks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> CreateProduct([FromBody] CreateUpdateProductRequest request)
        {

            var data = _mapper.Map<CreateUpdateProductRequest, Product>(request);
            _unitOfWork.Products.Add(data);

            var result = await _unitOfWork.CompleteAsync();
            return result > 0 ? Ok() : BadRequest();
        }
    }
}
