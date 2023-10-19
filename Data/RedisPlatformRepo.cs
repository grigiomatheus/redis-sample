using System.Text.Json;
using System.Text.Json.Serialization;
using RedisAPI.Models;
using StackExchange.Redis;

namespace RedisAPI.Data
{
    public class RedisPlatformRepo : IPlataformRepo
    {
        private readonly IConnectionMultiplexer _redis;

        public RedisPlatformRepo(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        public void CreatePlataform(Platform plataform)
        {
            if (plataform == null)
                throw new ArgumentNullException(nameof(plataform));

            var db = _redis.GetDatabase();

            var serialPlat = JsonSerializer.Serialize(plataform);

            // db.StringSet(plataform.Id, serialPlat);
            // db.SetAdd("PlatformsSet", serialPlat);

            db.HashSet("hashplatform", new HashEntry[]{
                new HashEntry(plataform.Id, serialPlat)
            });
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            var db = _redis.GetDatabase();

            // var set = db.SetMembers("PlatformsSet");
            var hash =  db.HashGetAll("hashplatform");

            if (hash.Length > 0)
                return hash.Select(x => JsonSerializer.Deserialize<Platform>(x.Value));

            return null;
        }

        public Platform GetPlatformById(string id)
        {
            var db = _redis.GetDatabase();

            // var stringPlatform = db.StringGet(id);
            var plataform = db.HashGet("hashplatform", id);

            // if (!string.IsNullOrEmpty(stringPlatform))
            if (!string.IsNullOrEmpty(plataform))
                return JsonSerializer.Deserialize<Platform>(plataform);

            return null;
        }
    }

}