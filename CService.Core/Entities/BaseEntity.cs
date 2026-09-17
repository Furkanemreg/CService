namespace CService.Core.Entities;

public abstract class BaseEntity
{
    public int Id { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public string? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }
    public string? UpdatedBy { get; set; }

    public DateTime? DeletedDate { get; set; }
    public string? DeletedBy { get; set; }

    public bool IsDeleted { get; set; }
}