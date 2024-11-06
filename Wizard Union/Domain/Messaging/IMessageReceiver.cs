namespace WizardUnion.Messaging;

public interface IMessageReceiver
{
    bool ReceiveMessage(IMessage _message, IMessageSender _sender);
}
