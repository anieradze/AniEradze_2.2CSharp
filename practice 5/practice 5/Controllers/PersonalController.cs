using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class PersonalController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly WorkWithExcel<Personal> _excelHelper;

    public PersonalController(ApplicationDbContext context)
    {
        _context = context;
        _excelHelper = new WorkWithExcel<Personal>();
    }

    [HttpGet("excel")]
    public async Task<IActionResult> GetPersonalExcel()
    {
        var personals = await _context.Personals.ToListAsync();

        if (!personals.Any())
        {
            return NotFound("მონაცემები ვერ მოიძებნა.");
        }

        var workbook = _excelHelper.Generate(personals);

        using (var stream = new MemoryStream())
        {
            workbook.SaveAs(stream);
            var content = stream.ToArray();
            return File(content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Personals.xlsx");
        }
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddPersonals()
    {
        var newEmployees = new List<Personal>
        {
            new Personal { Gvari = "აბაშიძე", Saxeli = "გიორგი", Ganyofileba = "IT", Qalaqi = "თბილისი", Asaki = 30 },
            new Personal { Gvari = "ბერიძე", Saxeli = "ანა", Ganyofileba = "HR", Qalaqi = "ბათუმი", Asaki = 28 },
            new Personal { Gvari = "გაბრიაძე", Saxeli = "ლევან", Ganyofileba = "ბუღალტერია", Qalaqi = "ქუთაისი", Asaki = 35 },
            new Personal { Gvari = "დვალიშვილი", Saxeli = "მარიამი", Ganyofileba = "იურიდიული", Qalaqi = "რუსთავი", Asaki = 26 },
            new Personal { Gvari = "ელიზბარაშვილი", Saxeli = "სანდრო", Ganyofileba = "მარკეტინგი", Qalaqi = "გორი", Asaki = 32 }
        };

        _context.Personals.AddRange(newEmployees);
        await _context.SaveChangesAsync();

        var workbook = _excelHelper.Generate(newEmployees);

        using (var stream = new MemoryStream())
        {
            workbook.SaveAs(stream);
            var content = stream.ToArray();
            return File(content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "NewPersonals.xlsx");
        }
    }
}
