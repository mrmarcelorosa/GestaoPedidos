using GestaoPedidos.DTO;
using GestaoPedidos.Models;
using GestaoPedidos.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestaoPedidos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Retorna todos os produtos disponíveis.
        /// </summary>
        /// <returns>Lista de produtos.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<Product>), 200)]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllAsync();
            return Ok(products);
        }

        /// <summary>
        /// Retorna um produto pelo ID.
        /// </summary>
        /// <param name="id">ID do produto.</param>
        /// <returns>Detalhes do produto.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Product), 200)]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        /// <summary>
        /// Cria um novo produto.
        /// </summary>
        /// <param name="product">Dados do produto.</param>
        /// <returns>Produto criado.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Product), 201)]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            if (product == null)
                return BadRequest();

            await _productService.AddAsync(product);
            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }

        /// <summary>
        /// Atualiza um produto existente.
        /// </summary>
        /// <param name="id">ID do produto.</param>
        /// <param name="product">Dados atualizados do produto.</param>
        /// <returns>Sem conteúdo.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            if (id != product.Id)
                return BadRequest();

            await _productService.UpdateAsync(product);
            return NoContent();
        }

        /// <summary>
        /// Remove um produto pelo ID.
        /// </summary>
        /// <param name="id">ID do produto.</param>
        /// <returns>Sem conteúdo.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Retorna uma lista reduzida de produtos para utilizar em select(dropdown).
        /// </summary>
        /// <returns>Lista de produtos no formato simplificado.</returns>
        [HttpGet("selects")]
        [ProducesResponseType(typeof(List<ProductDTO>), 200)]
        public async Task<IActionResult> GetClientAddresses()
        {
            var productDTOs = await _productService.GetProductsSelectAsync();
            if (productDTOs == null || !productDTOs.Any())
            {
                return Ok(new List<ProductDTO>());
            }

            return Ok(productDTOs);
        }
    }
}
