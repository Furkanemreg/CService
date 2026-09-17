namespace CService.Core.Entities;

public class ActivityLog : BaseEntity
{
    public string? ActorEmail { get; set; }
    public string? TargetUserId { get; set; }
    public string? TargetEmail { get; set; }
    public string Action { get; set; } = string.Empty;
    public string? Details { get; set; }
}