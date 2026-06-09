using System.Text.Json;
using IgApplication.Models;
using Microsoft.JSInterop;

namespace IgApplication.Services;

public class TicketService
{
    private readonly IJSRuntime _js;
    private const string StorageKey = "service-desk-tickets-v1";

    private List<Ticket> _tickets = new();
    private bool _initialized = false;

    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() },
        WriteIndented = false
    };

    public event Action? OnChange;

    public TicketService(IJSRuntime js) => _js = js;

    public async Task<List<Ticket>> GetTicketsAsync()
    {
        await EnsureInitializedAsync();
        return _tickets.ToList();
    }

    public async Task AddTicketAsync(Ticket ticket)
    {
        await EnsureInitializedAsync();
        ticket.Id = Guid.NewGuid().ToString();
        _tickets.Add(ticket);
        await PersistAsync();
        OnChange?.Invoke();
    }

    public async Task UpdateTicketAsync(Ticket ticket)
    {
        await EnsureInitializedAsync();
        var idx = _tickets.FindIndex(t => t.Id == ticket.Id);
        if (idx >= 0)
            _tickets[idx] = ticket;
        await PersistAsync();
        OnChange?.Invoke();
    }

    public async Task DeleteTicketAsync(string id)
    {
        await EnsureInitializedAsync();
        _tickets.RemoveAll(t => t.Id == id);
        await PersistAsync();
        OnChange?.Invoke();
    }

    private async Task EnsureInitializedAsync()
    {
        if (_initialized) return;
        _initialized = true;

        try
        {
            var json = await _js.InvokeAsync<string?>("localStorageInterop.getItem", StorageKey);
            if (!string.IsNullOrWhiteSpace(json))
            {
                _tickets = JsonSerializer.Deserialize<List<Ticket>>(json, _jsonOptions) ?? new();
                return;
            }
        }
        catch { /* first run or corrupt data – seed fresh */ }

        _tickets = GenerateMockTickets();
        await PersistAsync();
    }

    private async Task PersistAsync()
    {
        var json = JsonSerializer.Serialize(_tickets, _jsonOptions);
        await _js.InvokeVoidAsync("localStorageInterop.setItem", StorageKey, json);
    }

    // ── Mock Data Seed ───────────────────────────────
    private static List<Ticket> GenerateMockTickets()
    {
        var customers = new[] {
            "Acme Corp", "Globex Ltd", "Initech", "Umbrella Co",
            "Stark Industries", "Wayne Enterprises", "Weyland Corp",
            "Cyberdyne Systems", "Soylent Corp", "Bluth Company"
        };

        var assignees = new[] {
            "Alice Chen", "Bob Martinez", "Carol White",
            "David Kim", "Emma Johnson"
        };

        var titles = new[] {
            "Login page fails on Safari",
            "Payment gateway timeout",
            "Export to PDF broken",
            "Email notifications not sent",
            "Dashboard loads slowly",
            "Search returns incorrect results",
            "File upload limit too low",
            "Password reset link expired",
            "API rate limiting issues",
            "Mobile app crashes on startup",
            "SSO integration failing",
            "Report scheduler not running",
            "User permissions not applying correctly",
            "Two-factor auth bypass vulnerability",
            "Invoice total miscalculated",
            "Bulk import fails silently",
            "Audit logs missing entries",
            "Webhook delivery failures",
            "Session timeout too aggressive",
            "Dark mode contrast issues",
            "Calendar sync not working",
            "CSV export encoding error",
            "Customer data not loading",
            "Notification center always empty",
            "Performance degradation after update",
            "Signature capture not saving",
            "Multi-tenant routing broken",
            "Plugin conflict causing 500 error"
        };

        var statuses  = Enum.GetValues<TicketStatus>();
        var priorities = Enum.GetValues<TicketPriority>();
        var rng = new Random(42);
        var list = new List<Ticket>();

        for (int i = 0; i < titles.Length; i++)
        {
            var created = DateTime.Today.AddDays(-rng.Next(2, 90));
            var due     = created.AddDays(rng.Next(3, 30));
            var status  = statuses[rng.Next(statuses.Length)];
            var priority = priorities[rng.Next(priorities.Length)];

            // Make a few tickets intentionally overdue and critical for the "Needs Attention" section
            if (i < 4)
            {
                due = DateTime.Today.AddDays(-rng.Next(1, 7));
                status = i % 2 == 0 ? TicketStatus.InProgress : TicketStatus.Waiting;
            }
            if (i < 3)
                priority = TicketPriority.Critical;

            list.Add(new Ticket
            {
                Id          = Guid.NewGuid().ToString(),
                Title       = titles[i],
                Customer    = customers[rng.Next(customers.Length)],
                Assignee    = assignees[rng.Next(assignees.Length)],
                Priority    = priority,
                Status      = status,
                CreatedDate = created,
                DueDate     = due,
                Description = $"Issue reported by customer: {titles[i]}. Requires investigation and resolution."
            });
        }

        return list;
    }
}
