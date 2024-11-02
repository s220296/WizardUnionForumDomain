namespace WizardUnion.Messaging;

public interface IMessageReceiver
{
    bool ReceiveMessage(Message _message);
}
