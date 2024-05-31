namespace OnionArchitecture.Domain.Shared;

public abstract class Entity
{
    public Guid Id { get; private set; }
    public DateTime CreatedDate { get; private set; }
    public DateTime? UpdatedDate { get; private set; }
    public bool IsDeleted { get; private set; }

    protected Entity()
    {
        Id = Guid.NewGuid();
        CreatedDate = DateTime.UtcNow;
        UpdatedDate = null;
        IsDeleted = false;
    }

    public void Update()
    {
        UpdatedDate = DateTime.UtcNow;
    }

    public void Delete()
    {
        IsDeleted = true;
        Update();
    }
}