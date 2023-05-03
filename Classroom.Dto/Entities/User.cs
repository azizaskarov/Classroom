using Microsoft.AspNetCore.Identity;

namespace Classroom.Dto.Entities;

public class User : IdentityUser<Guid>
{
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    public  string? PhotoUrl { get; set; }

    public List<UserSchool> UserSchools { get; set; }

}