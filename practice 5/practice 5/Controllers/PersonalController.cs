using ClosedXML.Excel;
using ExcelImportExport.Models;
using Microsoft.AspNetCore.Mvc;

[Route("api/personali")]
[ApiController]
public class PersonaliController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ExcelService _excelService;

    public PersonaliController(AppDbContext context, ExcelService excelService)
    {
        _context = context;
        _excelService = excelService;
    }

    [HttpGet("excel")]
    public IActionResult GetExcel()
    {
        var personalebi = _context.Personali.ToList();
        var fileContents = _excelService.GenerateExcel(personalebi);
  
        return File(fileContents, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Personalebi.xlsx");
    }

    public async Task<IActionResult> AddEmployeesAsync()
    {
        var newEmployees = new List<Personali>
        {
            new Personali { Gvari = "გელაშვილი", Saxeli = "თემო", Ganyofileba = "სავაჭრო", Qalaqi = "თბილისი", Xelfasi = 1200, Asaki = 35, Staji = 10, Tarigi_Dabadebis = new DateTime(1989, 3, 15), Sqesi = "კაცი", Email = "temo@gmail.com", Ierarqia = 3 },
            new Personali { Gvari = "მიხელაძე", Saxeli = "ნინო", Ganyofileba = "სამედიცინო", Qalaqi = "ქუთაისი", Xelfasi = 1100, Asaki = 29, Staji = 5, Tarigi_Dabadebis = new DateTime(1995, 1, 10), Sqesi = "ქალი", Email = "nino@gmail.com", Ierarqia = 2 },
            new Personali { Gvari = "ჯანაშვილი", Saxeli = "გიორგი", Ganyofileba = "ინფორმატიკა", Qalaqi = "ბათუმი", Xelfasi = 1000, Asaki = 40, Staji = 15, Tarigi_Dabadebis = new DateTime(1982, 8, 25), Sqesi = "კაცი", Email = "giorgi@gmail.com", Ierarqia = 4 },
            new Personali { Gvari = "აბაშიძე", Saxeli = "ელენა", Ganyofileba = "საგანმანათლებლო", Qalaqi = "ზუგდიდი", Xelfasi = 950, Asaki = 30, Staji = 8, Tarigi_Dabadebis = new DateTime(1993, 11, 20), Sqesi = "ქალი", Email = "elena@gmail.com", Ierarqia = 1 },
            new Personali { Gvari = "ბალაშვილი", Saxeli = "ლევანი", Ganyofileba = "სამსახურის ტექნიკა", Qalaqi = "გორი", Xelfasi = 1300, Asaki = 25, Staji = 3, Tarigi_Dabadebis = new DateTime(1998, 6, 30), Sqesi = "კაცი", Email = "levani@gmail.com", Ierarqia = 5 }
        };

        await _context.Personali.AddRangeAsync(newEmployees);
        await _context.SaveChangesAsync();

        return Ok(new { Message = "5 თანამშრომელი დაემატა!" });
    }

    [HttpPost("addandexcel")]
    public async Task<IActionResult> AddAndGenerateExcel()
    {
        var result = await AddEmployeesAsync();
        if (result is OkObjectResult okResult)
        {
            return GetExcel();
        }

        return BadRequest(new { Message = "Error adding employees!" });
    }
}
