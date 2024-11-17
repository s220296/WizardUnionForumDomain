using Azure.Core;
using WizardUnion;

namespace WU_Test;

public static class FullMessagerDemo
{
    private static UserProfile s_user;
    private static UserProfile s_recipient;
    private static string s_preWrite;

    private static bool SelectWizard(string _name, bool _recipient)
    {
        if (string.IsNullOrEmpty(_name)) return false;

        try
        {
            IDItem<Wizard> wizard = DataAcquirer.GetWizardByName(_name);

            if ((s_user != null && s_user.Wizard.ID == wizard.ID) || (s_recipient != null && s_recipient.Wizard.ID == wizard.ID))
                return false;

            if (!_recipient)
                s_user = new UserProfile(wizard, new WizardMessager(wizard));
            else // if recipient
                s_recipient = new UserProfile(wizard, new WizardMessager(wizard));

            return true;
        }
        catch (Exception ex) 
        {
            Console.WriteLine(ex.Message);

            return false;
        }
    }

    public static void Run(int _args)
    {
        s_preWrite = new string("");

        Console.WriteLine("Select a Wizard to become by entering their name");
        while (!SelectWizard(Console.ReadLine(), false))
        { }

        AddToPreWrite($"Wizard {s_user.Wizard.Item.Name} is the sender\n");
        Console.WriteLine($"Wizard {s_user.Wizard.Item.Name} was sucessfully selected as the sender");

        Console.WriteLine("\nSelect a Wizard recipient by entering their name");
        while (!SelectWizard(Console.ReadLine(), true))
        { }

        AddToPreWrite($"Wizard {s_recipient.Wizard.Item.Name} is the recipient\n");
        Console.WriteLine($"Wizard {s_recipient.Wizard.Item.Name} was successfully selected as the recipient");

        Console.Clear();
        WritePreWrite();

        IDItem<TextMessage>[] messagesToRecipient = DataAcquirer.GetMessagesToWizardFrom(s_user.Wizard.ID, s_recipient.Wizard.ID);
        IDItem<TextMessage>[] messagesFromRecipient = DataAcquirer.GetMessagesToWizardFrom(s_recipient.Wizard.ID, s_user.Wizard.ID);

        int messagesLength = messagesToRecipient.Length + messagesFromRecipient.Length;

        (TextMessage text, bool toRecip)[] messages = new (TextMessage text, bool isRecip)[messagesLength];

        int toRecip = 0, fromRecip = 0;

        for(int i = 0; i < messages.Length; i++)
        {
            int toID = toRecip < messagesToRecipient.Length ? messagesToRecipient[toRecip].ID : int.MaxValue;
            int fromID = fromRecip < messagesFromRecipient.Length ? messagesFromRecipient[fromRecip].ID : int.MaxValue;

            // If message to recipient is chronologically (based on ID) earlier than from recipient
            if (toID < fromID)
            {
                messages[i] = (messagesToRecipient[toRecip].Item, true);

                toRecip++;
            }
            else if (fromID < toID) // If message from recipient is chronologically earlier
            {
                messages[i] = (messagesFromRecipient[fromRecip].Item, false);

                fromRecip++;
            }
            // There should be no case where toID and fromID are equal, unless messaging self (prevented in SelectWizard())

            if (messages[i].toRecip)
                AddToPreWrite($"{s_user.Wizard.Item.Name} to {s_recipient.Wizard.Item.Name}:\n");
            else // If recipient to sender
                AddToPreWrite($"{s_recipient.Wizard.Item.Name} to {s_user.Wizard.Item.Name}:\n");
            AddToPreWrite($"{messages[i].text.Text}\n\n");
        }

        Console.Clear();

        while (true)
        {
            WritePreWrite();

            Console.WriteLine($"Send a message to {s_recipient.Wizard.Item.Name} as {s_user.Wizard.Item.Name}.");

            string input = Console.ReadLine();
            if (input.Equals("quit")) break;

            DataSubmission.RecordTextMessage(new TextMessage(input), s_recipient.Messager, s_user.Messager);

            Console.Clear();
        }
    }

    private static void WritePreWrite() => Console.WriteLine(s_preWrite);

    private static void AddToPreWrite(string _preWriteAddition) => s_preWrite += _preWriteAddition;
}
