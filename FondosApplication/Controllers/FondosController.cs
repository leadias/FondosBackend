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
    public class FondosController : Controller
    {

        private readonly IMongoCollection<Fondo> _fondosCollection;

        public FondosController(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("banco");
            _fondosCollection = database.GetCollection<Fondo>("Fondos"); 
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var fondos = await _fondosCollection.Find(_=>true).ToListAsync();
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = fondos });
        }

        [HttpPost]

        public async Task<IActionResult> Create(Fondo fondo)
        {
            fondo.Id = ObjectId.GenerateNewId().ToString();
            fondo.Estado = "aperturado";
            try {
                await _fondosCollection.InsertOneAsync(fondo);
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "fondo aperturado" });
            }
            catch (System.Exception ex){
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message });
            }
        }

        [HttpPut]

        public async Task<IActionResult> Update(Fondo fondo)
        {
            fondo.Estado = "Cancelado";
            if(fondo.Id == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            if (ModelState.IsValid)
            {
                await _fondosCollection.ReplaceOneAsync(p => p.Id == fondo.Id, fondo);
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "fondo cancelado" });
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

        
        }


        }
}
