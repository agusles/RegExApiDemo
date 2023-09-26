using Microsoft.Extensions.Logging;
using RegExAPI.Domain.Contracts;
using RegExAPI.Domain.Entities;
using System.Text.RegularExpressions;

namespace RegExAPI.Infrastructure.Services;

public class RegExService : IRegExService
{
    readonly ILogger<RegExService> _logger;

    public RegExService(ILogger<RegExService> logger)
    {
        _logger = logger;
    }

    public IList<RegExMatchInfo> GetMatchingEntries(IList<ReadEntry> entries, RegExQuery query)
    {
        var regExResult = entries
            .Select(entry => new { Entry = entry, Match = Regex.Match(entry.Title, query.QueryString)} )
            .Where(regEx => regEx.Match.Success);

        return regExResult
            .Select(regEx => new RegExMatchInfo
            { 
                ExpressionIndex = regEx.Match.Index,
                Id = regEx.Entry.Id,
                Title = regEx.Entry.Title,
            }).ToList();
    }
}
