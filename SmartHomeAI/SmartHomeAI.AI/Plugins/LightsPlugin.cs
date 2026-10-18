using System.ComponentModel;
using System.Text.Json.Serialization;
using Microsoft.SemanticKernel;
using SmartHomeAI.Infrastructure.Repositories;

namespace SemanticKernel.Plugins;

public class LightsPlugin
{
   private readonly ILightsRepository _repository;

   public LightsPlugin(ILightsRepository repository)
   {
      _repository = repository;
   }

   [KernelFunction("get_lights")]
   [Description("Gets a list of lights and their current state")]
   public async Task<List<LightModel>> GetLightsAsync()
   {
      var lights = await _repository.GetLightsAsync();

      return lights.Select(light => new LightModel
      {
         Id = light.Id,
         Name = light.Name,
         IsOn = light.IsOn
      }).ToList();
   }

   [KernelFunction("change_state")]
   [Description("Changes the state of the light")]
   public async Task<LightModel?> ChangeStateAsync(int id, bool isOn)
   {
      var light = await _repository.ChangeStateAsync(id, isOn);

      return light is null ? null : new LightModel
      {
         Id = light.Id,
         Name = light.Name,
         IsOn = light.IsOn
      };
   }
}

public class LightModel
{
   [JsonPropertyName("id")]
   public int Id { get; set; }

   [JsonPropertyName("name")]
   public required string Name { get; set; }

   [JsonPropertyName("is_on")]
   public bool? IsOn { get; set; }
}