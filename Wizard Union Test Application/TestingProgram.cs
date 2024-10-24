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
        internal static string connectionString = "";

#pragma warning disable CS8618 
        internal static SqlConnection connection;
#pragma warning restore CS8618 

        internal static List<Place> places = new List<Place>();

        private static void Init(int _args)
        {
            connectionString = ConfigurationManager.ConnectionStrings["WizardUnionDB.Properties.Settings.WizardUnionDBConnectionString"].ConnectionString;
            PopulatePlaces();
        }

        private static void PopulatePlaces()
        {
            // SqlConnection implements IDisposable, so using using() will automatically close the SqlConnection when the using() statement is complete
            // otherwise use conenction.Close()
            // Both of these using() statements will use the same code block
            using (connection = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Place", connection))
            {
                // Normally connection.Open() would be used here, however the SqlDataAdapter does this for us

                DataTable placeTable = new DataTable();
                int rows = adapter.Fill(placeTable);

                // Fill places (without parents)
                for (int i = 0; i < rows; i++)
                {
                    DataRow currentRow = placeTable.Rows[i];
                    places.Add(new Place((double)currentRow["Cycles Per Eon"], (string)currentRow["Name"]));
                }
                // Assign parents to places
                for (int i = 0; i < rows; i++)
                {
                    DataRow currentRow = placeTable.Rows[i];
                    DataRow? place = placeTable.Rows.Find(currentRow["ParentId"]);

                    if (place == null) continue;

                    places[i].SetChildOf(places.Find((parent) => parent.Name == (string)place["Name"]));
                }
            }
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
