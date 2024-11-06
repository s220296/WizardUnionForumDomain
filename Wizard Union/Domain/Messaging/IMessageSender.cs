namespace WizardUnion.Messaging;

public interface IMessageSender
{
    object GetSender();
    bool SendMessage(IMessage _message, IMessageReceiver _receiver);
}
