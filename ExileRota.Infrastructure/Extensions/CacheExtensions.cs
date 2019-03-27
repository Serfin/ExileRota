using System;
using System.Runtime.Caching;
using ExileRota.Infrastructure.DTO;

namespace ExileRota.Infrastructure.Extensions
{
    public static class CacheExtensions
    {
        public static void SetJwt(this MemoryCache cache, Guid tokenId, JwtDto token)
            => cache.Set(GetJwtKey(tokenId), token, DateTimeOffset.Now.AddSeconds(5));

        public static object GetJwt(this MemoryCache cache, Guid tokenId)
            => cache.Get(GetJwtKey(tokenId));

        private static string GetJwtKey(Guid tokenId)
            => $"{tokenId}-jwt";
    }
}