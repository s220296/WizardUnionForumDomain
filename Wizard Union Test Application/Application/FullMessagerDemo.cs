using WizardUnion;

namespace WU_Test;

public static class FullMessagerDemo
{
    private static UserProfile s_user;

    private static bool SelectWizard(string _name)
    {
        if (string.IsNullOrEmpty(_name)) return false;

        try
        {
            IDItem<Wizard> wizard = DataAcquirer.GetWizardByName(_name);

            s_user = new UserProfile(wizard, new WizardMessager(wizard));

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
        Console.WriteLine("Select a Wizard by entering their name");
        while (!SelectWizard(Console.ReadLine()))
        { }

        Console.WriteLine($"Wizard {s_user.Wizard.Item.Name} was sucessfully selected");

        while (true)
        {
            string input = Console.ReadLine();
            if (input.Equals("quit")) break;



        }
    }
}
