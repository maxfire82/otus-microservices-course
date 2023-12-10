using System.Threading.Channels;

namespace Common.Queue.Services.Implementation;

public class BackgroundQueue<T> : IBackgroundQueue<T>
{
    private readonly Channel<T> _queue;

    public BackgroundQueue(int capacity = 10)
    {
        BoundedChannelOptions options = new(capacity)
        {
            FullMode = BoundedChannelFullMode.Wait
        };
        _queue = Channel.CreateBounded<T>(options);
    }
    
    public async Task QueueAsync(T workItem)
    {
        if (workItem is null)
        {
            throw new ArgumentNullException(nameof(workItem));
        }

        await _queue.Writer.WriteAsync(workItem);
    }

    public async Task<T> DequeueAsync(CancellationToken ct)
    {
        T workItem = await _queue.Reader.ReadAsync(ct);

        return workItem;
    }
}