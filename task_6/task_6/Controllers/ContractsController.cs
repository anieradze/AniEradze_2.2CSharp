using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace task_6.Controllers
{
    [Route("api/contracts")]
    [ApiController]
    public class ContractsController : ControllerBase
    {
        private readonly string _connectionString;

        public ContractsController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet("export")]
        public IActionResult ExportContractsToExcel()
        {
            DataTable dataTable = GetContracts();

            if (dataTable.Rows.Count == 0)
                return NotFound("მონაცემები ვერ მოიძებნა.");

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Contracts");
                worksheet.Cell(1, 1).InsertTable(dataTable);

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    stream.Position = 0;

                    return File(stream.ToArray(),
                                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                "Contracts.xlsx");
                }
            }
        }

        private DataTable GetContracts()
        {
            string query = @"
                SELECT 
                    xelshekrulebaID AS 'ხელშეკრულების ID',
                    gadasaxdeli_l AS 'გადასახდელი ლარში',
                    gadasaxdeli_d AS 'გადასახდელი დოლარში',
                    gadaxdili_l AS 'გადახდილი ლარში',
                    gadaxdili_d AS 'გადახდილი დოლარში',
                    vali_l AS 'ვალი ლარში',
                    vali_d AS 'ვალი დოლარში',
                    ISNULL(visi_mizezit, '') AS 'მიზეზი',
                    tarigi_dawyebis AS 'დაწყების თარიღი',
                    DATEFROMPARTS(YEAR(GETDATE()), MONTH(tarigi_damtavrebis), DAY(tarigi_damtavrebis)) AS 'დასრულების თარიღი',
                    DATEDIFF(DAY, GETDATE(), DATEFROMPARTS(YEAR(GETDATE()), MONTH(tarigi_damtavrebis), DAY(tarigi_damtavrebis))) AS 'დარჩენილი დღეები'
                FROM [Shekveta].[dbo].[Xelshekruleba]
                WHERE shesruleba = 'არა' OR vali_l > 0 OR vali_d > 0;";

            DataTable dataTable = new DataTable();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(dataTable);
                }
            }

            return dataTable;
        }
    }
}

