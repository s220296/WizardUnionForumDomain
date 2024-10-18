using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardUnion.Names;
using WizardUnion.Places;
using WizardUnion.Birth;

namespace WizardUnion;

public static class TestingProgram
{
    public static void Run()
    {
        // PLACES

        Place moon2;
        Place babyMoon;

        Place place = new Place(100d, "Syntheon I").AddChildren(new Place[]
        {
            new Place(100d, "Apar").AddChildren(new Place[]
            {
                new Place(200d, "1st Moon of Apar"),
                (moon2 = new Place(190d, "2nd Moon of Apar")).AddChildren(new Place[]
                {
                    babyMoon = new Place(210d, "Baby Moon of Apar")
                })
            }),

            new Place(100d, "Dupar"),
            new Place(100d, "Mupar"),
            new Place(120d, "Makart")
        });

        moon2.WriteLineage();
        Console.WriteLine("------------------");
        babyMoon.WriteLineage();

        // WIZARDS

        List<Wizard> wizards = new List<Wizard>()
        {
            new (new GenerationalName(new SingleName("Joe"), 3), new BirthDetails(place, 50d)),
            new (new LongName("Ryan", "Spell", "Caster"), new BirthDetails(babyMoon, 180d)),
        };

        // BEGIN UNIVERSE

        Universe.Begin(1d);

        Console.WriteLine("\n --- WIZARDS PRESENT IN THE UNIVERSE --- \n");
        foreach (var w in wizards)
        {
            Console.WriteLine($"Profile for {w.Name.Get()} - ");
            Console.WriteLine($"\t {w.BirthDetails}");
            Console.WriteLine($"\t Currently {w.BirthDetails.CurrentAgeInEons} eons old.");
            Console.WriteLine("");
        }

    }
}
