using WizardUnion;
using WizardUnion.MagicAndSpells;
using WizardUnion.Places;
using WizardUnion.Unions;

namespace WU_Test
{
    internal static class TestingProgram
    {
        internal static (Wizard wizard, int ID)[] wizards;
        internal static (Place place, int ID)[] places;
        internal static (Union union, int ID)[] unions;
        internal static (SpellProfile spell, int ID)[] spells;

        static TestingProgram()
        {
            wizards = Array.Empty<(Wizard, int)>();
            places = Array.Empty<(Place, int)>();
            unions = Array.Empty<(Union, int)>();
            spells = Array.Empty<(SpellProfile, int)>();
        }

        private static void Init(int _args)
        {
            Console.SetWindowSize(100, 80);
            Console.SetWindowPosition(1, 1);

            wizards = DataAcquirer.AcquireAllWizards();
            places = DataAcquirer.AcquireAllPlaces();
        }

        internal static void Run(int _args)
        {
            Init(_args);

            Universe.Begin(1.32d);

            WriteSubheading("WIZARDS");

            foreach ((Wizard wizard, int ID) wizard in wizards)
            {
                Console.WriteLine("My name is " + wizard.wizard.Name.Get() + " and I am from " + wizard.wizard.BirthDetails.PlaceOfBirth + ".");
                Console.WriteLine("I am " + wizard.wizard.BirthDetails.CurrentAgeInEons + " eons old and I know " + wizard.wizard.MagicProfile.KnownSpells.Count + " spells.");
                WriteSeperator();
            }
            Console.WriteLine("");

            WriteSubheading("PLACES");

            Console.WriteLine(Universe.Place.Name + " is currently in cycle " + Universe.Place.AgeInCycles());
            WriteSeperator();

            foreach ((Place place, int ID) place in places)
            {
                Console.WriteLine(place.place.Name + " is currently in cycle " + place.place.AgeInCycles() + ".");
                Console.WriteLine("\tLineage:");
                place.place.WriteLineage();
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
}
