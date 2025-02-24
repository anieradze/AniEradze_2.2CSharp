using Microsoft.AspNetCore.Mvc;
using practice_6.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

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
    }
}
