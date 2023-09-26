using Microsoft.Extensions.Options;
using RegExAPI.Domain.Contracts;
using RegExAPI.Domain.Entities;
using RegExAPI.Domain.Models;
using RestSharp;

namespace RegExAPI.Infrastructure.Services
{
    public class JsonPlaceholderClient : IJsonClient
    {
        readonly RestClient restClient;
        public string BaseURL { get; set; } = "https://jsonplaceholder.typicode.com/posts";

        public JsonPlaceholderClient(IOptions<JsonServiceOptions> options)
        {
            restClient = new RestClient(options.Value.Url);
        }

        public async Task<IList<ReadEntry>> GetEntries()
        {
            return await restClient.GetAsync<List<ReadEntry>>(new RestRequest());
        }
    }
}
