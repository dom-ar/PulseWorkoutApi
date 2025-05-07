using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.VisualBasic;

namespace Entities.Models;

public class Category
{
    [Column("CategoryId")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Category Name is a required field.")]
    [MaxLength(60, ErrorMessage = "Maximum length for the Category Name is 60 characters")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Category must have one of the set ExerciseType: Weight, Distance, TimeOnly, RepsOnly")]
    [EnumDataType(typeof(ExerciseType), ErrorMessage = "Invalid ExerciseType value.")]
    public ExerciseType ExerciseType { get; set; }

    public ICollection<Exercise>? Exercises { get; set; }
}

public enum ExerciseType
{
    Weight,
    Distance,
    TimeOnly,
    RepsOnly
}