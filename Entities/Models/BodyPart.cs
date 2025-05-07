using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class BodyPart
{
    [Column("BodyPartId")]
    public int Id { get; set; }

    [Required(ErrorMessage = "BodyPart Name is a required field.")]
    [MaxLength(60, ErrorMessage = "Maximum length for the BodyPart Name is 60 characters")]
    public string? Name { get; set; }

    public ICollection<Exercise>? Exercises { get; set; }
}