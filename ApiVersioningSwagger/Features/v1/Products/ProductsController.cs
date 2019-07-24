using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ApiVersioningSwagger.Features.v1.Products
{
    [ApiController]
    [ApiVersion("1", Deprecated = true)]
    [Route("api/v{version:apiVersion}/products")]
    [Produces("application/json")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet("")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                var items = await _productRepository.GetProducts();
                return Ok(items);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}