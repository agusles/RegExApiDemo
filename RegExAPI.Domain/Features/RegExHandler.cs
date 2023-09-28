using MediatR;
using Microsoft.Extensions.Logging;
using RegExAPI.Domain.Contracts;
using RegExAPI.Domain.Entities;
using System.Diagnostics;

namespace RegExAPI.Domain.Features;

public class RegExHandler : IRequestHandler<RegExQuery, RegExResponse>
{
    private readonly ILogger<RegExHandler> logger;
    readonly IJsonClient jsonCLient;
    readonly IRegExService regExService;

    public RegExHandler(ILogger<RegExHandler> logger, IJsonClient jsonCLient, IRegExService regExService)
    {
        this.logger = logger;
        this.jsonCLient = jsonCLient;
        this.regExService = regExService;
    }

    public async Task<RegExResponse> Handle(RegExQuery request, CancellationToken cancellationToken)
    {
        logger.Log(LogLevel.Information, 
            $"RegExHandler - Start processing {request.QueryString}");

        var stopWatch = new Stopwatch();
        stopWatch.Start();
        var jsonEntries = await jsonCLient.GetEntries();
        var regexEntries = regExService.GetMatchingEntries(jsonEntries, request);

        stopWatch.Stop();

        var response = new RegExResponse
        {
            RegExQuery = request.QueryString,
            Entries = regexEntries,
            Count = regexEntries.Count,
            Total = jsonEntries.Count,
            Percentage = jsonEntries.Count != 0
                ? regexEntries.Count * 100 / jsonEntries.Count
                : 0,
            TimeElapsed = stopWatch.Elapsed.TotalMilliseconds
        };

        logger.Log(LogLevel.Information,
            $"RegExHandler - Finished processing {response.RegExQuery}: {response.Count} match/es - Time elapsed: {stopWatch.Elapsed.TotalMilliseconds} milliseconds");

        return response;
    }
}
