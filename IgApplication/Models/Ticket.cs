using System.Text.Json.Serialization;

namespace IgApplication.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TicketStatus
{
    New,
    InProgress,
    Waiting,
    Resolved,
    Closed
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TicketPriority
{
    Low,
    Medium,
    High,
    Critical
}

public class Ticket
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Title { get; set; } = string.Empty;
    public string Customer { get; set; } = string.Empty;
    public string Assignee { get; set; } = string.Empty;
    public TicketPriority Priority { get; set; } = TicketPriority.Medium;
    public TicketStatus Status { get; set; } = TicketStatus.New;
    public DateTime CreatedDate { get; set; } = DateTime.Today;
    public DateTime DueDate { get; set; } = DateTime.Today.AddDays(7);
    public string? Description { get; set; }

    /// <summary>Short display ID derived from the GUID prefix.</summary>
    public string DisplayId => Id.Length >= 8 ? Id[..8].ToUpperInvariant() : Id.ToUpperInvariant();

    /// <summary>String representation of Priority for grid binding.</summary>
    public string PriorityName => Priority.ToString();

    /// <summary>String representation of Status for grid binding.</summary>
    public string StatusName => Status.ToString();

    public bool IsOverdue => DueDate.Date < DateTime.Today
                             && Status != TicketStatus.Resolved
                             && Status != TicketStatus.Closed;

    public Ticket Clone() => new()
    {
        Id = Id,
        Title = Title,
        Customer = Customer,
        Assignee = Assignee,
        Priority = Priority,
        Status = Status,
        CreatedDate = CreatedDate,
        DueDate = DueDate,
        Description = Description
    };
}
