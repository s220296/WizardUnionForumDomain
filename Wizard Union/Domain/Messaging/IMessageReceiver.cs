namespace WizardUnion.Messaging;

public interface IMessageReceiver
{
    object GetReceiver();
    bool ReceiveMessage(IMessage _message, IMessageSender _sender);
}
