using RedisAPI.Models;

namespace RedisAPI.Data
{
    public interface IPlataformRepo
    {
        void CreatePlataform(Platform plataform);

        Platform GetPlatformById(string id);

        IEnumerable<Platform> GetAllPlatforms();
    }
}