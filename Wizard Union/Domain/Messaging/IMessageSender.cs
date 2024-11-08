namespace WizardUnion.Messaging;

public interface IMessageSender
{
    object GetSender();
    object GetSenderID();
    bool SendMessage(IMessage _message, IMessageReceiver _receiver);
}
