namespace WizardUnion.Messaging;

public interface IMessageReceiver
{
    object GetReceiver();
    object GetReceiverID();
    bool ReceiveMessage(IMessage _message, IMessageSender _sender);
}
