namespace WizardUnion.Messaging;

public interface IMessage
{
    string GetAsString();
    bool TrySend(IMessageSender _from, IMessageReceiver _to);
}

public interface IMessage<T> : IMessage
{
    T GetMessage();
}
