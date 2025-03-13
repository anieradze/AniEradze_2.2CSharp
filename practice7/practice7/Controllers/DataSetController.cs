using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace practice7.Controllers
{
    public class DataSetController : ControllerBase
    {
        [HttpPost("UploadDataset")]
        public IActionResult UploadDataset(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            // Parse the JSON file
            var dataSet = ParseJsonFile(file);

            // Generate Excel from dataset
            var excelFile = GenerateExcelFile(dataSet);

            // Return the Excel file
            return File(excelFile, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "dataset.xlsx");
        }

        private DataSet ParseJsonFile(IFormFile file)
        {
            using (var stream = new StreamReader(file.OpenReadStream()))
            {
                string jsonContent = stream.ReadToEnd();
                return JsonConvert.DeserializeObject<DataSet>(jsonContent);
            }
        }

        private byte[] GenerateExcelFile(DataSet dataSet)
        {
            using (var package = new ExcelPackage())
            {
                // Create Customers sheet
                var customersSheet = package.Workbook.Worksheets.Add("Customers");
                customersSheet.Cells[1, 1].Value = "Id";
                customersSheet.Cells[1, 2].Value = "PhoneNumber";

                int customerRow = 2;
                foreach (var customer in dataSet.Customers)
                {
                    customersSheet.Cells[customerRow, 1].Value = customer.Id;
                    customersSheet.Cells[customerRow, 2].Value = customer.PhoneNumber;
                    customerRow++;
                }

                // Create Events sheet
                var eventsSheet = package.Workbook.Worksheets.Add("Events");
                eventsSheet.Cells[1, 1].Value = "Type";
                eventsSheet.Cells[1, 2].Value = "SrcNumber";
                eventsSheet.Cells[1, 3].Value = "DstNumber";
                eventsSheet.Cells[1, 4].Value = "Time";
                eventsSheet.Cells[1, 5].Value = "Duration";
                eventsSheet.Cells[1, 6].Value = "SrcLoc";
                eventsSheet.Cells[1, 7].Value = "DstLoc";

                int eventRow = 2;
                foreach (var ev in dataSet.Events)
                {
                    eventsSheet.Cells[eventRow, 1].Value = ev.Type;
                    eventsSheet.Cells[eventRow, 2].Value = ev.SrcNumber;
                    eventsSheet.Cells[eventRow, 3].Value = ev.DstNumber;
                    eventsSheet.Cells[eventRow, 4].Value = ev.Time;
                    eventsSheet.Cells[eventRow, 5].Value = ev.Duration;

                    // Join SrcLoc and DstLoc into string for Excel
                    eventsSheet.Cells[eventRow, 6].Value = string.Join(",", ev.SrcLoc);
                    eventsSheet.Cells[eventRow, 7].Value = string.Join(",", ev.DstLoc);

                    eventRow++;
                }

                // Return the Excel file as byte array
                return package.GetAsByteArray();
            }
        }
    }

}