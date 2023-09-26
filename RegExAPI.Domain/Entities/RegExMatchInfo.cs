namespace RegExAPI.Domain.Entities;

public record RegExMatchInfo
{
    public int ExpressionIndex { get; set; }
    public int Id { get; set; }
    public string Title { get; set; }
}
