using MassTransit;

public class ReceiveObserver : IReceiveObserver
{
    public Task ConsumeFault<T>(ConsumeContext<T> context, TimeSpan duration, string consumerType, Exception exception) where T : class
    {
        throw new NotImplementedException();
    }

    public Task PostConsume<T>(ConsumeContext<T> context, TimeSpan duration, string consumerType) where T : class
    {
        throw new NotImplementedException();
    }

    public Task PostReceive(ReceiveContext context)
    {
        throw new NotImplementedException();
    }

    public Task PreReceive(ReceiveContext context)
    {
        throw new NotImplementedException();
    }

    public Task ReceiveFault(ReceiveContext context, Exception exception)
    {
        throw new NotImplementedException();
    }
}