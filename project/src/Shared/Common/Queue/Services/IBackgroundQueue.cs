namespace Common.Queue.Services;

public interface IBackgroundQueue<T>
{
    /// <summary>
    /// Помещаем в очередь
    /// </summary>
    /// <param name="workItem"></param>
    Task QueueAsync(T workItem);
    
    /// <summary>
    /// Извлекаем из очереди
    /// </summary>
    /// <returns></returns>
    Task<T> DequeueAsync(CancellationToken ct);
}