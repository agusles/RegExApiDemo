namespace RegExAPI.Domain.Entities;

public record RegExResponse
{
    public string RegExQuery { get; set; }
    public int Count { get; set; }
    public int Total { get; set; }
    public int Percentage { get; set; }
    public IList<RegExMatchInfo> Entries { get; set; }
    public double TimeElapsed { get; internal set; }
}
