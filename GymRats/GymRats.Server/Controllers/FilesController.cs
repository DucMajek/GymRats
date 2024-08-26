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

    [HttpGet("download/{id}")]
    public async Task<IActionResult> DownloadFile(int id)
    {
        var connectionString = _configuration.GetConnectionString("Default");

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
}
