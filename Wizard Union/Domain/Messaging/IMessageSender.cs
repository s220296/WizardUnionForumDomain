namespace WizardUnion.Messaging;

public interface IMessageSender
{
    bool GetSender<T>(out T _sender);
    bool SendMessage(IMessage _message, IMessageReceiver _receiver);
}
