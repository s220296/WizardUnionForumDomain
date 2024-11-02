namespace WizardUnion.Messaging;

public struct Message
{
    public string Text;

    public Message(string _message, IMessageReceiver _receiver)
    {
        // HOW DOES RECEIVER RECEIVE MESSAGE?
        // HOW ARE MESSAGES STORED?
        // HOW DO WE KNOW WHO SENT THE MESSAGE? (probs need an IMessageSender)
        (Text) = (_message);
    }
}
