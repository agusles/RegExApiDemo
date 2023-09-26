using MediatR;
using System.ComponentModel.DataAnnotations;

namespace RegExAPI.Domain.Entities;

public record RegExQuery: IRequest<RegExResponse>
{
    
    public RegExQuery(string queryString)
    {
        this.QueryString = queryString;
    }

    [Required(ErrorMessage ="QueryString is required")]
    public string QueryString { get; set; }
}
