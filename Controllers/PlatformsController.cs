using Microsoft.AspNetCore.Mvc;
using RedisAPI.Data;
using RedisAPI.Models;

namespace RedisAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlataformRepo _repo;

        public PlatformsController(IPlataformRepo repo)
        {
            _repo = repo;
        }

        [HttpGet("{id}", Name="GetPlatformById")]
        public ActionResult<Platform> GetPlatformById([FromRoute] string id)
        {
            var platform = _repo.GetPlatformById(id);

            if(platform != null)
                return Ok(platform);
            
            return NotFound();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Platform>> GetAllPlataforms()
        {
            return Ok(_repo.GetAllPlatforms());
        }

        [HttpPost]
        public ActionResult<Platform> CreatePlataform(Platform platform)
        {
            _repo.CreatePlataform(platform);

            return CreatedAtRoute(nameof(GetPlatformById), new {Id = platform.Id}, platform);
        }
    }
}