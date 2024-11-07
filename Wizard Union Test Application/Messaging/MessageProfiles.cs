using WizardUnion.Messaging;
using WizardUnion;
using WizardUnion.Unions;

namespace WU_Test;

public class WizardMessager : IMessageReceiver, IMessageSender
{
    public IDItem<Wizard> Wizard { get; protected set; }
    public List<(IMessage, IMessageSender, IMessageReceiver)> Messages;

    public WizardMessager(IDItem<Wizard> _wizard)
    {
        (Wizard) = (_wizard);

        Messages = new List<(IMessage, IMessageSender, IMessageReceiver)>();
    }

    public bool ReceiveMessage(IMessage _message, IMessageSender _sender)
    {
        if (_sender is null) return false;

        Messages.Add((_message, _sender, this));

        return true;
    }

    public bool SendMessage(IMessage _message, IMessageReceiver _receiver)
    {
        if (_receiver.ReceiveMessage(_message, this))
        {
            Messages.Add((_message, this, _receiver));
            return true;
        }

        return false;
    }

    public object GetSender()
    {
        return Wizard.GetTypeAndID();
    }

    public object GetReceiver()
    {
        return Wizard.GetTypeAndID();
    }
}

public class UnionMessager : IMessageReceiver
{
    public IDItem<Union> Union { get; protected set; }
    public List<(IMessage, IMessageSender)> MessageBoard { get; protected set; }

    public UnionMessager(IDItem<Union> _union)
    {
        (Union) = (_union);
        MessageBoard = new List<(IMessage, IMessageSender)>();
    }

    public bool ReceiveMessage(IMessage _message, IMessageSender _sender)
    {
        MessageBoard.Add((_message, _sender));

        return true;
    }

    public object GetReceiver()
    {
        return Union.GetTypeAndID();
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
        if (_from.SendMessage(this, _to))
        {
            DataSubmission.RecordTextMessage(this, _from, _to);

            return true;
        }

        return false;
    }
}