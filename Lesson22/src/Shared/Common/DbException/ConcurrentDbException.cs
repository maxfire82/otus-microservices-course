namespace Common.DbException;

public class ConcurrentDbException: Exception
{
    public ConcurrentDbException(string message) : base(message)
    {
        
    }
}