using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

public class FilesController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<FilesController> _logger;
    private const string PdfMimeType = "application/pdf";

    public FilesController(IConfiguration configuration, ILogger<FilesController> logger)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpGet("trainingPlan/{id}")]
    [HttpGet("diet/{type}/{calorie}")]
    public async Task<IActionResult> DownloadFile(
        int? id,
        string? type,
        string? calorie,
        CancellationToken cancellationToken = default)
    {
        try
        {
            if (!string.IsNullOrEmpty(calorie))
            {
                return await DownloadDietFile(type!, calorie, cancellationToken);
            }
            
            if (id.HasValue)
            {
                return await DownloadTrainingPlanFile(id.Value, cancellationToken);
            }

            return BadRequest("Either id or type/calorie parameters must be provided");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error downloading file. Parameters: id={Id}, type={Type}, calorie={Calorie}", 
                id, type, calorie);
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request");
        }
    }

    private async Task<IActionResult> DownloadDietFile(string type, string calorie, CancellationToken cancellationToken)
    {
        var connectionString = _configuration.GetConnectionString("MyDBConnection");
        
        const string query = @"
            SELECT *
            FROM Food_ebook
            WHERE diet_type = @diet_type AND calories = @calories";

        await using var connection = new SqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        await using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@diet_type", type);
        command.Parameters.AddWithValue("@calories", calorie);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        
        if (!await reader.ReadAsync(cancellationToken))
        {
            _logger.LogWarning("Diet file not found for type {Type} and calorie {Calorie}", type, calorie);
            return NotFound();
        }

        var fileData = (byte[])reader["ebook_file"];
        var dietType = $"{reader["diet_type"]}_{reader["calories"]}";

        return File(fileData, PdfMimeType, dietType);
    }

    private async Task<IActionResult> DownloadTrainingPlanFile(int id, CancellationToken cancellationToken)
    {
        var connectionString = _configuration.GetConnectionString("MyDBConnection");
        
        const string query = @"
            SELECT * 
            FROM Training_Plan 
            WHERE id_training_plan = @id";

        await using var connection = new SqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        await using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", id);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        
        if (!await reader.ReadAsync(cancellationToken))
        {
            _logger.LogWarning("Training plan file not found for id {Id}", id);
            return NotFound();
        }

        var fileData = (byte[])reader["training_plan_file"];
        var fileName = reader["training_plan_name"].ToString();

        return File(fileData, PdfMimeType, fileName);
    }
}