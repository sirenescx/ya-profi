using System.ComponentModel.DataAnnotations;

namespace Api.Models.Requests;

public class PromotionRequest
{
    [Required(ErrorMessage = "Promo name is required.")]
    public string Name { get; set; }
    public string? Description { get; set; }
}