using RegExAPI.Domain.Entities;

namespace RegExAPI.Domain.Contracts;

public interface IJsonClient
{
    Task<IList<ReadEntry>> GetEntries(CancellationToken cancellationToken);
}
