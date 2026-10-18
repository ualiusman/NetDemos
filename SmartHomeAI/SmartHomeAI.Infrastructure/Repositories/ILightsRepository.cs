namespace SmartHomeAI.Infrastructure.Repositories;

public interface ILightsRepository
{
    Task<List<LightEntity>> GetLightsAsync();
    Task<LightEntity?> ChangeStateAsync(int id, bool isOn);
}

public class LightEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool? IsOn { get; set; }
}
