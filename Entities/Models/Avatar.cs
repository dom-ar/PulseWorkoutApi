using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class Avatar
{
    [Column("AvatarId")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Image URL is a required field.")]
    [Url(ErrorMessage = "Invalid URL format.")]
    public string? ImageUrl { get; set; }

    //public ICollection<User>? Users { get; set; }
}