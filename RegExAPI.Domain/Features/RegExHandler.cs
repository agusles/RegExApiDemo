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

        var jsonEntries = await jsonCLient.GetEntries(cancellationToken);
        var matchingEntries = regExService.GetMatchingEntries(jsonEntries, request);

        stopWatch.Stop();

        var response = BuildResponse(request, stopWatch, jsonEntries, matchingEntries);

        logger.Log(LogLevel.Information,
            $"RegExHandler - Finished processing {response.RegExQuery}: {response.Count} match/es - Time elapsed: {stopWatch.Elapsed.TotalMilliseconds} milliseconds");

        return response;
    }

    private static RegExResponse BuildResponse(RegExQuery request, Stopwatch stopWatch, IList<ReadEntry> jsonEntries, IList<RegExMatchInfo> regexEntries)
        => new()
            {
                RegExQuery = request.QueryString,
                Entries = regexEntries,
                Count = regexEntries.Count,
                Total = jsonEntries.Count,
                Percentage = GetMathingPercentage(jsonEntries, regexEntries),
                TimeElapsed = stopWatch.Elapsed.TotalMilliseconds
            };

    private static int GetMathingPercentage(IList<ReadEntry> jsonEntries, IList<RegExMatchInfo> regexEntries)
        => jsonEntries.Count != 0
            ? regexEntries.Count * 100 / jsonEntries.Count
            : 0;
}
