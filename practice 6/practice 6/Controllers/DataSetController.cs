using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Validations;
using practice_6.Models;
using System.IO;
using System.Text.Json;

namespace practice_6.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataSetController : ControllerBase
    {
        private const string FilePath = "dataset.json";
        [HttpPost("parse-file")]
        public async Task<IActionResult> ParseFile()
        {
            if (!System.IO.File.Exists(FilePath))
            {
                return NotFound("File not found");
            }

            var jsonData = await System.IO.File.ReadAllTextAsync(FilePath);
            var dataset = JsonSerializer.Deserialize<Dataset>(jsonData);

            if (dataset == null)
            {
                return BadRequest("Invalid dataset format");
            }

            return Ok("File parsed successfully");
        }

        [HttpGet("customers")]
        public async Task<IActionResult> GetCustomers()
        {
            if (!System.IO.File.Exists(FilePath))
            {
                return NotFound("File not found");
            }

            var jsonData = await System.IO.File.ReadAllTextAsync(FilePath);
            var dataset = JsonSerializer.Deserialize<Dataset>(jsonData);

            if (dataset == null)
            {
                return BadRequest("Invalid dataset format");
            }

            var customers = new Dictionary<string, Customer>();
            int idCounter = 1;

            foreach (var evt in dataset.Events)
            {
                if (!customers.ContainsKey(evt.SrcNumber))
                {
                    customers[evt.SrcNumber] = new Customer { Id = idCounter++, PhoneNumber = evt.SrcNumber };
                }

                if (!customers.ContainsKey(evt.DstNumber))
                {
                    customers[evt.DstNumber] = new Customer { Id = idCounter++, PhoneNumber = evt.DstNumber };
                }
            }

            return Ok(customers.Values);
        }

        [HttpGet("events")]
        public async Task<IActionResult> GetEvents()
        {
            if (!System.IO.File.Exists(FilePath))
            {
                return NotFound("File not found");
            }

            var jsonData = await System.IO.File.ReadAllTextAsync(FilePath);
            var dataset = JsonSerializer.Deserialize<Dataset>(jsonData);

            if (dataset == null)
            {
                return BadRequest("Invalid dataset format");
            }

            return Ok(dataset.Events);
        }

        [HttpGet("generate-excel")]
        public async Task<IActionResult> GenerateExcelFile()
        {
            if (!System.IO.File.Exists(FilePath))
            {

                return NotFound("Dataset file not found");
            }
            var jsonData = await System.IO.File.ReadAllTextAsync(FilePath);
            var dataset = JsonSerializer.Deserialize<Dataset>(jsonData);
            if (dataset == null)
            {
                return BadRequest("Invalid dataset format");

            }
            var customers = new Dictionary<string, Customer>();
            int idCounter = 1;

            foreach (var evt in dataset.Events)
            {
                if (!customers.ContainsKey(evt.SrcNumber))
                {
                    customers[evt.SrcNumber] = new Customer { Id = idCounter++, PhoneNumber = evt.SrcNumber };
                }

                if (!customers.ContainsKey(evt.DstNumber))
                {

                    customers[evt.DstNumber] = new Customer { Id = idCounter++, PhoneNumber = evt.DstNumber };
                }

            }

            using var workbook = new XLWorkbook();

            var eventsSheet = workbook.Worksheets.Add("Events");
            var customersSheet = workbook.Worksheets.Add("Customers");

            eventsSheet.Cell(1, 1).Value = "Type";
            eventsSheet.Cell(1, 2).Value = "ScrNumber";
            eventsSheet.Cell(1, 3).Value = "DstNumber";
            eventsSheet.Cell(1, 4).Value = "Duration";


            int eventRow = 2;
            foreach (var evt in dataset.Events)
            {
                eventsSheet.Cell(eventRow, 1).Value = evt.Type;
                eventsSheet.Cell(eventRow, 2).Value = evt.SrcNumber;
                eventsSheet.Cell(eventRow, 3).Value = evt.DstNumber;
                eventsSheet.Cell(eventRow, 4).Value = evt.Duration;
                eventRow++;

            }

            customersSheet.Cell(1, 1).Value = "Id";
            customersSheet.Cell(1, 2).Value = "PhoneNumber";

            int customersRow = 2;
            foreach (var customer in customers.Values)
            {
                customersSheet.Cell(customersRow, 1).Value = customer.Id;
                customersSheet.Cell(customersRow, 2).Value = customer.PhoneNumber;
                customersRow++;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return File(stream.ToArray(), "application/vnd.openxmlFormats-officedocument.spreadsheetml.sheet", "dataset.xlsx");
        }
    }
}



