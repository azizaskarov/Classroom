using System.ComponentModel.DataAnnotations;

namespace Classroom.Mvc.Models;

public class CreateUserDto
{
    [StringLength(50,MinimumLength = 3)]
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    [StringLength(50,MinimumLength = 3)]
    public string Username { get; set; }

    [StringLength(50)]
    public string Password { get; set; } 

    [RegularExpression("^[0-9]{9}$",ErrorMessage = "Phone number is not valid")]
    public string? PhoneNumber { get; set; }

    [Required]
    public IFormFile? Photo { get; set; }
}