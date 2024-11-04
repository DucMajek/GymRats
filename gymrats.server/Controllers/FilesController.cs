using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

[ApiController]
[Route("api/[controller]")]
public class FilesController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public FilesController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet("downloadExcelFile/{id}")]
    public async Task<IActionResult> DownloadExcelFile(int id)
    {
        var connectionString = _configuration.GetConnectionString("MyDBConnection");

        byte[] fileData = null;
        string fileName = null;
        string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        using (var connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();

            var query = "select nazwa_planu, zawartosc_planu_treningowego from Plan_treningowy where id_plan_treningowy = @id";

            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        fileName = reader["nazwa_planu"].ToString() + ".xlsx";
                        fileData = (byte[])reader["zawartosc_planu_treningowego"];
                    }
                }
            }
        }

        if (fileData == null)
        {
            return NotFound();
        }

        return File(fileData, contentType, fileName);
    }

    [HttpGet("downloadPdfFile/{id}/{calorie}")]
    public async Task<IActionResult> DownloadPdfFile(int id, string calorie)
    {
        var connectionString = _configuration.GetConnectionString("MyDBConnection");

        byte[]? fileData = null;
        string? dietType = null;
        string? calories = null;
        string contentType = "application/pdf";

        using (var connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();

            var query = "select rodzaj_diety, zawartosc_jadlospisu, kalorycznosc from Jadlospis where id_jadlospisu = @id and kalorycznosc = @kalorycznosc";

            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@kalorycznosc", calorie);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        calories = reader["kalorycznosc"].ToString();
                        dietType = reader["rodzaj_diety"].ToString() + calories + ".pdf";
                        fileData = (byte[])reader["zawartosc_jadlospisu"];
                    }
                }
            }
        }

        if (fileData == null)
        {
            return NotFound();
        }

        return File(fileData, contentType, dietType);
    }
}
