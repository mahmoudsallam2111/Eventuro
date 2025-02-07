using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;

namespace Evently.Common.Infrastructure.Caching;
public static class CacheOptions
{
    public static DistributedCacheEntryOptions DefaultExpiration()
    {
        return new DistributedCacheEntryOptions()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2)
        };
    }

    public static DistributedCacheEntryOptions Create(TimeSpan? expiration)
    {
       return expiration is not null ?
            new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = expiration } :
            DefaultExpiration();
    }
}
