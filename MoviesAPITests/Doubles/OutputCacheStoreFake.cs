using Microsoft.AspNetCore.OutputCaching;

namespace MoviesAPITests.Doubles;

public class OutputCacheStoreFake : IOutputCacheStore
{
    public ValueTask EvictByTagAsync(string tag, CancellationToken cancellationToken)
    {
        return ValueTask.CompletedTask;
    }

    public ValueTask<byte[]?> GetAsync(string key, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public ValueTask SetAsync(string key, byte[] value, string[]? tags, TimeSpan validFor, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
