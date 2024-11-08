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

        UserProfile w1 = new UserProfile(wizards[0], new WizardMessager(wizards[0]));
        UserProfile w2 = new UserProfile(wizards[1], new WizardMessager(wizards[1]));

        IDItem<Union> unionID;
        {
            Union union = new Union("All Wizards United", new Condtions(), new Role("Member"));
            unionID = new IDItem<Union>(union, 24);
        }
        UnionMessager unionMessager = new UnionMessager(unionID);

        DataAcquirer.FillWizardToWizardTextMessages(new UserProfile[] {w1, w2});

        foreach ((IMessage, IMessageSender, IMessageReceiver) message in w1.Messager.Messages)
        {
            Console.WriteLine((message.Item2 as WizardMessager).Wizard.Item.Name + " says:");
            Console.WriteLine(message.Item1.GetAsString());
            Console.WriteLine("To " + (message.Item3 as WizardMessager).Wizard.Item.Name);
            Console.WriteLine("");
        }
    }
}
