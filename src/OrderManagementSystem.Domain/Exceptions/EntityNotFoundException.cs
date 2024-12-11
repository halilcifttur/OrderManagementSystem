namespace OrderManagementSystem.Domain.Exceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException()
            : base("Entity is not found.")
    {
    }
}