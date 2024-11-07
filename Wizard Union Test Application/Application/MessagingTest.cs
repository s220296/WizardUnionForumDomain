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

        while (true)
        {
            Console.WriteLine("\nYou are Ryan and you are messaging Harry.");
            Console.Write("Message: ");
            TextMessage newMessage = new TextMessage(Console.ReadLine());
            Console.WriteLine("");

            Console.Clear();

            newMessage.TrySend(w1.Messager, w2.Messager);
        }
    }
}
