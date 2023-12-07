namespace Domain.Common.Interfaces.DataAccess;

public interface IEntity<out T>
{
    T Id { get; }
}