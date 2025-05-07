using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class Exercise
{
    [Column("ExerciseId")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Exercise Name is a required field")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Exercise name must be between 2 and 100 characters.")]
    public string? Name { get; set; }

    [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
    public string? Description { get; set; }

    public List<string>? Instructions { get; set; }

    [ForeignKey(nameof(BodyPart))]
    public int BodyPartId { get; set; } = 1;
    public BodyPart? BodyPart { get; set; }

    [ForeignKey(nameof(Category))] 
    public int CategoryId { get; set; } = 1;
    public Category? Category { get; set; }

    [StringLength(1000, ErrorMessage = "Url cannot exceed 1000 characters.")]
    public string? IconUrl { get; set; }
    public List<string>? ImageUrls { get; set; }

    // For user created exercises
    [ForeignKey(nameof(User))] 
    public Guid? CreatedByUserId { get; set; }
    public User? CreatedByUser { get; set; }

    public bool IsUserCreated { get; set; } = false;
}

