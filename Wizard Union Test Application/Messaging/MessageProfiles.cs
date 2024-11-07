using WizardUnion.Messaging;
using WizardUnion;
using WizardUnion.Unions;

namespace WU_Test;

public class WizardMessager : IMessageReceiver, IMessageSender
{
    public Wizard Wizard { get; protected set; }

    public WizardMessager(Wizard _wizard)
    {
        (Wizard) = (_wizard);
    }

    public bool ReceiveMessage(IMessage _message, IMessageSender _sender)
    {
        if (_sender is null) return false;

        // Store message

        string senderName = _sender.GetSender().ToString();

        if (_sender.GetSender() is Wizard)
            senderName = (_sender.GetSender() as Wizard).Name.Get();

        Console.WriteLine($"I, {Wizard.Name}, received a message from {senderName}: ");
        Console.WriteLine($"- {_message.GetAsString()}");

        return true;
    }

    public bool SendMessage(IMessage _message, IMessageReceiver _receiver)
    {
        return _receiver.ReceiveMessage(_message, this);
    }

    public object GetSender()
    {
        return Wizard;
    }
}

public class UnionMessager : IMessageReceiver
{
    public Union Union { get; protected set; }
    public List<(IMessage, IMessageSender)> MessageBoard { get; protected set; }

    public UnionMessager(Union _union)
    {
        (Union) = (_union);
        MessageBoard = new List<(IMessage, IMessageSender)>();
    }

    public bool ReceiveMessage(IMessage _message, IMessageSender _sender)
    {
        MessageBoard.Add((_message, _sender));

        return true;
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
        return _from.SendMessage(this, _to);
    }
}