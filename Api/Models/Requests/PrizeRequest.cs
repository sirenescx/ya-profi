using System.ComponentModel.DataAnnotations;

namespace Api.Models.Requests;

public class PrizeRequest
{
    [Required(ErrorMessage = "Description is required.")]
    public string Description { get; set; }
}