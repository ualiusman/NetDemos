using Microsoft.Data.SqlClient;

namespace SmartHomeAI.Infrastructure.Repositories;

public class SqlLightsRepository : ILightsRepository
{
    private readonly string _connectionString;

    public SqlLightsRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<List<LightEntity>> GetLightsAsync()
    {
        var lights = new List<LightEntity>();

        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("SELECT Id, Name, IsOn FROM Lights", connection);

        await connection.OpenAsync();

        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            lights.Add(new LightEntity
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                IsOn = reader.IsDBNull(2) ? null : reader.GetBoolean(2)
            });
        }

        return lights;
    }

    public async Task<LightEntity?> ChangeStateAsync(int id, bool isOn)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        using (var updateCmd = new SqlCommand(
            "UPDATE Lights SET IsOn = @IsOn WHERE Id = @Id", connection))
        {
            updateCmd.Parameters.AddWithValue("@Id", id);
            updateCmd.Parameters.AddWithValue("@IsOn", isOn);

            var rows = await updateCmd.ExecuteNonQueryAsync();

            if (rows == 0)
                return null;
        }

        using var selectCmd = new SqlCommand(
            "SELECT Id, Name, IsOn FROM Lights WHERE Id = @Id", connection);

        selectCmd.Parameters.AddWithValue("@Id", id);

        using var reader = await selectCmd.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            return new LightEntity
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                IsOn = reader.IsDBNull(2) ? null : reader.GetBoolean(2)
            };
        }

        return null;
    }
}
