using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

public class UpdateUserDTO
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
}