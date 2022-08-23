using CrudMongo.Models;
using CrudMongo.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrudMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly ProdutoServices _produtoServices;

        public ProdutosController(ProdutoServices produtoServices)
        {
            _produtoServices = produtoServices;
        }

        [HttpGet]
        [Route("get-list")]
        public async Task<List<Produto>>GetProdutos() => await _produtoServices.GetAsync();

        [HttpGet]
        [Route("get-id")]
        public async Task<Produto> GetProdutoById(string id) => await _produtoServices.GetAsync(id);

        [HttpPost]
        public async Task<Produto>PostProduto(Produto produto)
        {
            await _produtoServices.CreateAsync(produto);
            return produto;
        }
    }
}
