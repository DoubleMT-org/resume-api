using Resume.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Resume.Domain.Commons;
public abstract class Auditable<T>
{
    [Required]
    public T Id { get; set; }

    [JsonIgnore]
    public EntityState State { get; set; }

    [JsonIgnore]
    public string CreatedAt { get; private set; }

    [JsonIgnore]
    public string UpdatedAt { get; set; }

    public void Create()
    {
        CreatedAt = DateTime.UtcNow.ToString("dd.MM.yyyy HH:mm");
        State = EntityState.Active;
    }

    public void Update()
    {
        UpdatedAt = DateTime.UtcNow.ToString("dd.MM.yyyy HH:mm");
    }
    public void Delete()
    {
        State = EntityState.Deleted;
    }
}
