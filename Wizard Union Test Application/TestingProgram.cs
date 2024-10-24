using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardUnion.Birth;
using WizardUnion.Names;
using WizardUnion.Places;
using WizardUnion;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace WU_Test
{
    internal static class TestingProgram
    {
        internal static List<Place> places = new List<Place>();

        private static void Init(int _args)
        {
            
        }

        internal static void Run(int _args)
        {
            Init(_args);

            Universe.Begin(1d);

            Wizard ryan = new Wizard(new FullName("Ryan", "Dick"), new BirthDetails(new Place(25d, "Mufat"), 13d));

            Console.WriteLine($"{ryan} I am {ryan.BirthDetails.CurrentAgeInEons} eons old.");
            Console.WriteLine("===========================================");
            foreach(Place place in places)
            {
                Console.WriteLine("-------------------------");
                place.WriteLineage();
                Console.WriteLine("-------------------------");
            }
        }
    }
}
