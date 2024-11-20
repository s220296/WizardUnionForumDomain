using Azure.Core;
using WizardUnion;

namespace WU_Test;

public static class FullMessagerDemo
{
    private static UserProfile s_user;
    private static UserProfile s_recipient;
    private static string s_preWrite;
    private static string s_messagesPreWrite;
    private static bool s_keepRefreshing;

    private class CancelToken
    {
        public bool cancel = false;
    }

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
        while (true)
        {
            RunLoop(_args);

            s_keepRefreshing = false;

            Console.WriteLine("Write 'exit' to exit or press enter to continue");
            if (Console.ReadLine().Equals("exit")) return;

            Console.Clear();
        }
    }

    private static void RunLoop(int _args)
    {
        s_preWrite = new string("");
        s_messagesPreWrite = new string("");
        s_user = null;
        s_recipient = null;
        s_keepRefreshing = false;

        AddToPreWrite("Write 'quit' to quit\n");

        Console.WriteLine("\tLIST OF WIZARD NAMES");
        {
            IDItem<Wizard>[] wizards = DataAcquirer.AcquireAllWizards();
            foreach (IDItem<Wizard> wiz in wizards)
            {
                Console.WriteLine(wiz.Item.Name);
            }
        }
        Console.WriteLine("\n");

        Console.WriteLine("Select a Wizard to become by entering their name");
        while (!SelectWizard(Console.ReadLine(), false))
        { }

        AddToPreWrite($"Wizard {s_user.Wizard.Item.Name} is the sender\n");
        Console.WriteLine($"Wizard {s_user.Wizard.Item.Name} was sucessfully selected as the sender");

        Console.WriteLine("\nSelect a Wizard recipient by entering their name");
        while (!SelectWizard(Console.ReadLine(), true))
        { }

        AddToPreWrite($"Wizard {s_recipient.Wizard.Item.Name} is the recipient\n\n");
        Console.WriteLine($"Wizard {s_recipient.Wizard.Item.Name} was successfully selected as the recipient");

        AddDbMessagesToPreWrite();

        Console.Clear();

        while (true)
        {
            string input = new string("");

            s_keepRefreshing = true;

            do
            {
                AddDbMessagesToPreWrite();

                WritePreWrite();

                Console.WriteLine($"Send a message to {s_recipient.Wizard.Item.Name} as {s_user.Wizard.Item.Name}.");

                // Will continue to refresh every second
                CancelToken token = new CancelToken();
                RefreshPage(1000, token);

                input = Console.ReadLine();

                token.cancel = true;

                if (input.Equals("quit")) return;
            }
            while (string.IsNullOrWhiteSpace(input));

            DataSubmission.RecordTextMessage(new TextMessage(input), s_user.Messager, s_recipient.Messager);

            Console.Clear();
        }
    }

    private static async void RefreshPage(int _milliInterval, CancelToken _token)
    {
        while (s_keepRefreshing && !_token.cancel)
        {
            await Task.Delay(_milliInterval);

            string prePreWrite = GetFullPreWrite();

            await Task.Run(AddDbMessagesToPreWrite);

            if (!prePreWrite.Equals(GetFullPreWrite()))
            {
                Console.Clear();
                WritePreWrite();
                Console.WriteLine($"Send a message to {s_recipient.Wizard.Item.Name} as {s_user.Wizard.Item.Name}.");
            }
        }
    }

    private static void AddDbMessagesToPreWrite()
    {
        s_messagesPreWrite = new string("");

        IDItem<TextMessage>[] messagesToRecipient = DataAcquirer.GetMessagesToWizardFrom(s_user.Wizard.ID, s_recipient.Wizard.ID);
        IDItem<TextMessage>[] messagesFromRecipient = DataAcquirer.GetMessagesToWizardFrom(s_recipient.Wizard.ID, s_user.Wizard.ID);

        int messagesLength = messagesToRecipient.Length + messagesFromRecipient.Length;

        (TextMessage text, bool toRecip)[] messages = new (TextMessage text, bool isRecip)[messagesLength];

        int toRecip = 0, fromRecip = 0;

        for (int i = 0; i < messages.Length; i++)
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
                AddToMessagesPreWrite($"{s_user.Wizard.Item.Name} to {s_recipient.Wizard.Item.Name}:\n");
            else // If recipient to sender
                AddToMessagesPreWrite($"{s_recipient.Wizard.Item.Name} to {s_user.Wizard.Item.Name}:\n");
            AddToMessagesPreWrite($"{messages[i].text.Text}\n\n");
        }
    }

    private static void WritePreWrite() => Console.WriteLine(s_preWrite + "\n" + s_messagesPreWrite);
    private static string GetFullPreWrite() => s_preWrite + "\n" + s_messagesPreWrite;

    private static void AddToPreWrite(string _preWriteAddition) => s_preWrite += _preWriteAddition;

    private static void AddToMessagesPreWrite(string _preWriteAddition) => s_messagesPreWrite += _preWriteAddition;
}
