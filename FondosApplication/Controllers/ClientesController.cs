using FondosApplication.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FondosApplication.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IMongoCollection<Cliente> _clientesCollection;

        public ClientesController(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("banco");
            _clientesCollection = database.GetCollection<Cliente>("Clientes");
        }

        [HttpGet]
        [Route("getCliente")]
        public async Task<IActionResult> Index()
        {
            var cliente = await _clientesCollection.Find(_ => true).ToListAsync();
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = cliente });
        }

        [HttpPost]
        [Route("Store")]

        public async Task<IActionResult> Create(Cliente cliente)
        {
            cliente.Id = ObjectId.GenerateNewId().ToString();
            cliente.saldo = 500000;
            try
            {
                await _clientesCollection.InsertOneAsync(cliente);
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "cliente creado" });
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("Edit")]
        public async Task<IActionResult> Update(Cliente cliente)
        {
            if (cliente.Id == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            if (ModelState.IsValid)
            {

                await _clientesCollection.ReplaceOneAsync(p => p.Id == cliente.Id, cliente);
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "fondo cancelado" });
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

        }
    }
}
