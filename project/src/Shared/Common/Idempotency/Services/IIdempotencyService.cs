namespace Common.Idempotency.Services;

public interface IIdempotencyService
{
    bool ValidateOperationId(string operationId);
}