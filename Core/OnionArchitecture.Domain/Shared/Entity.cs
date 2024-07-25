namespace OnionArchitecture.Domain.Shared;

public abstract class Entity
{
    public Guid Id { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public bool IsDeleted { get; private set; }

    protected Entity()
    {
        Id = Guid.NewGuid();
        UpdatedAt = null;
        IsDeleted = false;
    }

    public void Update()
    {
        UpdatedAt = DateTime.UtcNow;
    }

    public void SetCreatedAt()
    {
        CreatedAt = DateTime.UtcNow;
    }

    public void Delete()
    {
        IsDeleted = true;
        Update();
    }
}