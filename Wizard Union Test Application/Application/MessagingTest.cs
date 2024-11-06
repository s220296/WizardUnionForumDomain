using WizardUnion;
using WizardUnion.Messaging;

namespace WU_Test;

public static class MessagingTest
{
    public static void Run(int _args)
    {
        IDItem<Wizard>[] wizards = DataAcquirer.AcquireAllWizards();

        UserProfile w1 = new UserProfile(wizards[0], new WizardMessager(wizards[0].Item));
        UserProfile w2 = new UserProfile(wizards[1], new WizardMessager(wizards[1].Item));

        Console.WriteLine($"{w1.Wizard.Name} is sending a message to {w2.Wizard.Name}.");

        TextMessage message = new TextMessage("Hello friend!");
        if (!message.TrySend(w1.Messager, w2.Messager))
        {
            Console.WriteLine("Message not received");
        }

        Console.WriteLine($"\n{w2.Wizard.Name} is replying to {w1.Wizard.Name}");

        TextMessage reply = new TextMessage("Hello to you!");
        if (!reply.TrySend(w2.Messager, w1.Messager))
        {
            Console.WriteLine("Reply not received");
        }
    }
}
