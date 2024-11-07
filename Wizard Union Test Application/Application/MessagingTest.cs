using WizardUnion;
using WizardUnion.Messaging;
using WizardUnion.Unions;

namespace WU_Test;

public static class MessagingTest
{
    private class Condtions : IUnionMembershipConditions
    {
        public UnionJoinAttemptInfo IsSatisfiedBy(Wizard _wizard)
        {
            return UnionJoinAttemptInfo.JOINED;
        }
    }

    private struct Role : IUnionRole
    {
        string Name { get; set; }

        public Role(string _name) { Name = _name; }

        public int GetAccessLevel()
        {
            return 0;
        }

        public UnionRoleInfo GetRoleProperty(string _name)
        {
            return new UnionRoleInfo("Name", Name);
        }
    }

    public static void Run(int _args)
    {
        IDItem<Wizard>[] wizards = DataAcquirer.AcquireAllWizards();

        UserProfile w1 = new UserProfile(wizards[0], new WizardMessager(wizards[0].Item));
        UserProfile w2 = new UserProfile(wizards[1], new WizardMessager(wizards[1].Item));

        Union union = new Union("All Wizards United", new Condtions(), new Role("Member"));
        UnionMessager unionMessager = new UnionMessager(union);

        bool flip = false;

        while (true)
        {
            Console.Clear();

            Console.WriteLine($"-- This is the message board of {union.Name} --");

            foreach((IMessage message, IMessageSender sender) message in unionMessager.MessageBoard)
            {
                Console.WriteLine($"{message.sender.GetSender()} says:");
                Console.WriteLine($"- {message.message.GetAsString()}\n");
            }

            if (flip)
            {
                Console.WriteLine($"\n Write a message as Ryan");
                TextMessage text = new TextMessage(Console.ReadLine());
                text.TrySend(w1.Messager, unionMessager);
            }
            else
            {
                Console.WriteLine($"\n Write a message as Harry");
                TextMessage text = new TextMessage(Console.ReadLine());
                text.TrySend(w2.Messager, unionMessager);
            }

            flip = !flip;
            if (unionMessager.MessageBoard[^1].Item1.GetAsString() == "EXIT")
                break;
        }
    }
}
