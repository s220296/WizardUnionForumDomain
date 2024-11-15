using WizardUnion;

namespace WU_Test;

public static class FullMessagerDemo
{
    private static UserProfile s_user;
    private static UserProfile s_recipient;

    private static bool SelectWizard(string _name, bool _recipient)
    {
        if (string.IsNullOrEmpty(_name)) return false;

        try
        {
            IDItem<Wizard> wizard = DataAcquirer.GetWizardByName(_name);

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
        Console.WriteLine("Select a Wizard to become by entering their name");
        while (!SelectWizard(Console.ReadLine(), false))
        { }

        Console.WriteLine($"Wizard {s_user.Wizard.Item.Name} was sucessfully selected");

        Console.WriteLine("\nSelect a Wizard recipient by entering their name");
        while (!SelectWizard(Console.ReadLine(), true))
        { }

        Console.WriteLine($"Wizard {s_recipient.Wizard.Item.Name} was successfully selected");

        while (true)
        {
            string input = Console.ReadLine();
            if (input.Equals("quit")) break;



        }
    }
}
