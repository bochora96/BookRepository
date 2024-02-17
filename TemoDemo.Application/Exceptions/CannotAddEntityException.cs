namespace TemoDemo.Application.Exceptions;

public class CannotAddEntityException(Guid id)
    : Exception($"Entity on id {id} already exists");
