using WizardUnion;
using WizardUnion.MagicAndSpells;
using WizardUnion.Places;
using WizardUnion.Unions;

namespace WU_Test;

internal static class TestingProgram
{
    internal static IDItem<Wizard>[] wizards;
    internal static IDItem<Place>[] places;
    internal static IDItem<Union>[] unions;
    internal static IDItem<SpellProfile>[] spells;

    static TestingProgram()
    {
        wizards = Array.Empty<IDItem<Wizard>>();
        places = Array.Empty<IDItem<Place>>();
        unions = Array.Empty<IDItem<Union>>();
        spells = Array.Empty<IDItem<SpellProfile>>();
    }

    private static void Init(int _args)
    {
#pragma warning disable CA1416
        Console.SetWindowSize(100, 80);
        Console.SetWindowPosition(1, 1);
#pragma warning restore CA1416

        wizards = DataAcquirer.AcquireAllWizards();
        places = DataAcquirer.AcquireAllPlaces();
    }

    internal static void Run(int _args)
    {
        Init(_args);

        Universe.Begin(1.32d);

        WriteSubheading("WIZARDS");

        foreach (IDItem<Wizard> wizard in wizards)
        {
            Console.WriteLine("My name is " + wizard.Item.Name.Get() + " and I am from " + wizard.Item.BirthDetails.PlaceOfBirth + ".");
            Console.WriteLine("I am " + wizard.Item.BirthDetails.CurrentAgeInEons + " eons old and I know " + wizard.Item.MagicProfile.KnownSpells.Count + " spells.");
            WriteSeperator();
        }
        Console.WriteLine("");

        WriteSubheading("PLACES");

        Console.WriteLine(Universe.Place.Name + " is currently in cycle " + Universe.Place.AgeInCycles());
        WriteSeperator();

        foreach (IDItem<Place> place in places)
        {
            Console.WriteLine(place.Item.Name + " is currently in cycle " + place.Item.AgeInCycles() + ".");
            Console.WriteLine("\tLineage:");
            place.Item.WriteLineage();
            WriteSeperator();
        }
    }

    private static void WriteSubheading(string _title)
    {
        Console.WriteLine($"============{_title}============");
    }

    private static void WriteSeperator()
    {
        Console.WriteLine("-------------------------");
    }
}
