using System.Security.Claims;
using Classroom.Dto.Context;
using Classroom.Dto.Entities;
using Classroom.Mvc.Helpers;
using Classroom.Mvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Classroom.Mvc.Controllers;


[Authorize]
public class SchoolsController : Controller
{
    private readonly AppDbContext _context;

    public SchoolsController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var schools = _context.Schools.ToList();

        return View(schools);
    }

    [HttpGet]
    public IActionResult CreateSchool()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateSchool([FromForm] CreateSchoolDto createSchoolDto)
    {
        
        if (!ModelState.IsValid)
            return View(createSchoolDto);


        var school = new School()
        {
            Name = createSchoolDto.Name,
            Description = createSchoolDto.Description,
        };

        

        if (createSchoolDto.Photo != null)
        {
            school.PhotoUrl = await FileHelper.SaveSchoolFile(createSchoolDto.Photo);
        }

        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        if (userId != null)
        {
            school.UserSchools = new List<UserSchool>()
            {
                new UserSchool()
                {
                    UserId = userId,
                    Type = EUserSchool.Creater,
                }
            };
        }

        _context.Schools.Add(school);

        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }


}