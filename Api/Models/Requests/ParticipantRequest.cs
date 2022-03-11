using System.ComponentModel.DataAnnotations;

namespace Api.Models.Requests;

public class ParticipantRequest
{
    [Required(ErrorMessage = "Participant name is required.")]
    public string Name { get; set; }
}