using AiPrompt.Model.Data;
using SQLite;

namespace AiPrompt.Model.Entity;

[Table("config")]
public record Config : BaseEntity {
    public string? SourcePath { get; set; }
    public string? Language { get; set; }
}