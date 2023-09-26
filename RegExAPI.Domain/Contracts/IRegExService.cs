using RegExAPI.Domain.Entities;

namespace RegExAPI.Domain.Contracts;

public interface IRegExService
{
    IList<RegExMatchInfo> GetMatchingEntries(IList<ReadEntry> entries, RegExQuery query);
}
