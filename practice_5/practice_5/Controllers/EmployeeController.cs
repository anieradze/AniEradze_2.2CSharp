using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using practice_5.Models;
using ClosedXML.Excel;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;

namespace practice_5.Controllers
{
    [ApiController]
    [Route("api/employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly string _connectionString;

        public EmployeeController(IConfiguration configuration)
        {
            // Retrieve the connection string from the configuration
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet("ExportDataInExcel")]
        public IActionResult ExportDataInExcel()
        {
            List<employee> personalList = new List<employee>();

            using (SqlConnection connection =  new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT personaliID, gvari, saxeli, ganyofileba, qalaqi, regioni, raioni, xelfasi, asaki, staji, tarigi_dabadebis, sqesi, misamarti_saxlis, teleponi_saxlis, mobiluri, email, ierarqia FROM dbo.Personali";

                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        personalList.Add(new employee
                        {
                            PersonaliID = reader.GetInt32(0),
                            Gvari = reader.GetString(1),
                            Saxeli = reader.GetString(2),
                            Ganyofileba = reader.GetString(3),
                            Qalaqi = reader.GetString(4),
                            Regioni = reader.GetString(5),
                            Raioni = reader.GetString(6),
                            Xelfasi = reader.GetDecimal(7),
                            Asaki = reader.GetInt32(8),
                            Staji = reader.GetInt32(9),
                            TarigiDabadebis = reader.GetDateTime(10),
                            Sqesi = reader.GetString(11),
                            MisamartiSaxlis = reader.GetString(12),
                            TeleponiSaxlis = reader.GetString(13),
                            Mobiluri = reader.GetString(14),
                            Email = reader.GetString(15),
                            Ierarqia = reader.GetString(16)
                        });
                    }
                }
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("Personali");
                ws.Cell(1, 1).InsertTable(personalList);

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    stream.Position = 0;
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Personali.xlsx");
                }
            }
        }

        [HttpPost("ImportData")]
        public async Task<IActionResult> ImportData(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Invalid file.");
            }

            List<employee> newEmployees = new List<employee>();

            using (MemoryStream stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                stream.Position = 0;

                using (XLWorkbook wb = new XLWorkbook(stream))
                {
                    var sheet = wb.Worksheets.First();
                    var rows = sheet.RangeUsed().RowsUsed().Skip(1);

                    foreach (var row in rows)
                    {
                        newEmployees.Add(new employee
                        {
                            Gvari = row.Cell(1).GetValue<string>(),
                            Saxeli = row.Cell(2).GetValue<string>(),
                            Ganyofileba = row.Cell(3).GetValue<string>(),
                            Qalaqi = row.Cell(4).GetValue<string>(),
                            Regioni = row.Cell(5).GetValue<string>(),
                            Raioni = row.Cell(6).GetValue<string>(),
                            Xelfasi = row.Cell(7).GetValue<decimal>(),
                            Asaki = row.Cell(8).GetValue<int>(),
                            Staji = row.Cell(9).GetValue<int>(),
                            TarigiDabadebis = row.Cell(10).GetValue<DateTime>(),
                            Sqesi = row.Cell(11).GetValue<string>(),
                            MisamartiSaxlis = row.Cell(12).GetValue<string>(),
                            TeleponiSaxlis = row.Cell(13).GetValue<string>(),
                            Mobiluri = row.Cell(14).GetValue<string>(),
                            Email = row.Cell(15).GetValue<string>(),
                            Ierarqia = row.Cell(16).GetValue<string>()
                        });
                    }
                }
            }

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                foreach (var person in newEmployees)
                {
                    string query = @"INSERT INTO dbo.Personali (gvari, saxeli, ganyofileba, qalaqi, regioni, raioni, xelfasi, asaki, staji, tarigi_dabadebis, sqesi, misamarti_saxlis, teleponi_saxlis, mobiluri, email, ierarqia)
                                     VALUES (@Gvari, @Saxeli, @Ganyofileba, @Qalaqi, @Regioni, @Raioni, @Xelfasi, @Asaki, @Staji, @TarigiDabadebis, @Sqesi, @MisamartiSaxlis, @TeleponiSaxlis, @Mobiluri, @Email, @Ierarqia)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Gvari", person.Gvari);
                        command.Parameters.AddWithValue("@Saxeli", person.Saxeli);
                        command.Parameters.AddWithValue("@Ganyofileba", person.Ganyofileba);
                        command.Parameters.AddWithValue("@Qalaqi", person.Qalaqi);
                        command.Parameters.AddWithValue("@Regioni", person.Regioni);
                        command.Parameters.AddWithValue("@Raioni", person.Raioni);
                        command.Parameters.AddWithValue("@Xelfasi", person.Xelfasi);
                        command.Parameters.AddWithValue("@Asaki", person.Asaki);
                        command.Parameters.AddWithValue("@Staji", person.Staji);
                        command.Parameters.AddWithValue("@TarigiDabadebis", person.TarigiDabadebis);
                        command.Parameters.AddWithValue("@Sqesi", person.Sqesi);
                        command.Parameters.AddWithValue("@MisamartiSaxlis", person.MisamartiSaxlis);
                        command.Parameters.AddWithValue("@TeleponiSaxlis", person.TeleponiSaxlis);
                        command.Parameters.AddWithValue("@Mobiluri", person.Mobiluri);
                        command.Parameters.AddWithValue("@Email", person.Email);
                        command.Parameters.AddWithValue("@Ierarqia", person.Ierarqia);

                        await command.ExecuteNonQueryAsync();
                    }
                }
            }

            return Ok("Employees added successfully.");
        }
    }
}
