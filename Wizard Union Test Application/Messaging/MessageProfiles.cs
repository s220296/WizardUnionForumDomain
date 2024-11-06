using WizardUnion.Messaging;

namespace WU_Test;

public class WizardMessager : IMessageReceiver, IMessageSender
{
    public bool ReceiveMessage(IMessage _message, IMessageSender _sender)
    {
        throw new NotImplementedException();
    }

    public bool SendMessage(IMessage _message, IMessageReceiver _receiver)
    {
        throw new NotImplementedException();
    }
}

public class UnionMessager : IMessageReceiver
{
    public bool ReceiveMessage(IMessage _message, IMessageSender _sender)
    {
        throw new NotImplementedException();
    }
}

public class TextMessage : IMessage
{
    public string Text;

    public TextMessage(string _text) { Text = _text; }

    public string GetAsString()
    {
        return Text;
    }

    public bool TrySend(IMessageSender _from, IMessageReceiver _to)
    {
        throw new NotImplementedException();
    }
}