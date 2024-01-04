using SQLite;

namespace AiPrompt.Model.Entity;

public abstract record BaseEntity {
    [PrimaryKey] public Guid Id { get; set; }
}