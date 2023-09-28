using Microsoft.Extensions.Options;
using RegExAPI.Domain.Contracts;
using RegExAPI.Domain.Entities;
using RegExAPI.Domain.Models;
using System.Net.Http.Json;

namespace RegExAPI.Infrastructure.Services;

public class JsonPlaceholderClient : IJsonClient
{
    private readonly HttpClient client;

    public JsonPlaceholderClient(HttpClient httpClient, IOptions<JsonServiceOptions> options)
    {
        client = httpClient;
        client.BaseAddress = new Uri(options.Value.Url);
    }

    public async Task<IList<ReadEntry>> GetEntries(CancellationToken cancellationToken)
        => await client.GetFromJsonAsync<List<ReadEntry>>(string.Empty, cancellationToken);
}
