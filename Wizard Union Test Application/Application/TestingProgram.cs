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
using WU_Test.DataAcquisition;

namespace WU_Test
{
    internal static class TestingProgram
    {
        internal static Wizard[] wizards;

        static TestingProgram()
        {
            wizards = Array.Empty<Wizard>();
        }

        private static void Init(int _args)
        {
            wizards = DataAcquirer.AcquireAllWizards();
        }

        internal static void Run(int _args)
        {
            Init(_args);

            Universe.Begin(1d);

            Console.WriteLine("================WIZARDS======================");
            foreach(Wizard wizard in wizards)
            {
                Console.WriteLine("-------------------------");
                Console.WriteLine("My name is " + wizard.Name.Get());
                Console.WriteLine("-------------------------");
            }
        }
    }
}
