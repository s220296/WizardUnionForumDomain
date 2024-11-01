using Microsoft.Data.SqlClient;
using System.Data;
using WizardUnion;
using WizardUnion.Birth;
using WizardUnion.MagicAndSpells;
using WizardUnion.Names;
using WizardUnion.Places;

namespace WU_Test
{
    public static class DataAcquirer
    {
        public static string connectionString = "";

#pragma warning disable CS8618 
        public static SqlConnection connection;
#pragma warning restore CS8618 


        public static void InitializeConntectionString()
        {
            // I tried to use the ConfigurationManager, but it just wouldn't work
            connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=E:\\.GitHub\\WizardUnionForumDomain\\WizardUnionDB\\ForumData.mdf;Integrated Security=True;Connect Timeout=30";
        }

        public static (Wizard, int)[] AcquireAllWizards()
        {
            InitializeConntectionString();

            // SqlConnection implements IDisposable, so using using() will automatically close the SqlConnection when the using() statement is complete
            // otherwise use conenction.Close()
            // Both of these using() statements will use the same code block
            using (connection = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Wizards", connection))
            {
                // Normally connection.Open() would be used here, however the SqlDataAdapter does this for us

                DataTable wizardTable = new DataTable();
                int rows = adapter.Fill(wizardTable);

                // Initialize
                (Wizard wizard, int ID)[] wizards = new (Wizard, int)[rows];
                BirthDetails defaultDetails = new BirthDetails(Universe.Place, 0.5d);
                MagicProfile defaultProfile = new MagicProfile(new SpellMastery(), SpellProfileList.Empty);

                // Retrieve all wizards from DB
                for (int i = 0; i < rows; i++)
                {
                    string name = (string)wizardTable.Rows[i]["Name"];
                    int ID = (int)wizardTable.Rows[i]["Id"];

                    wizards[i].wizard = new Wizard(new SingleName(name), defaultDetails, defaultProfile);
                    wizards[i].ID = ID;
                }

                return wizards;
            }
        }

        public static (Place, int)[] AcquireAllPlaces()
        {
            InitializeConntectionString();

            using (connection = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Places", connection))
            {
                DataTable placeTable = new DataTable();
                int rows = adapter.Fill(placeTable);

                // Initialize
                (Place place, int ID)[] places = new (Place, int)[rows];

                // Retrieve all places from DB
                for (int i = 0; i < rows; i++)
                {
                    string name = (string)placeTable.Rows[i]["Name"];
                    double cyclesPerEon = (double)placeTable.Rows[i]["CyclesPerEon"];
                    int ID = (int)placeTable.Rows[i]["Id"];

                    places[i].place = new Place(cyclesPerEon, name);
                    places[i].ID = ID;
                }

                // Set parents for all places
                for (int j = 0; j < rows; j++)
                {
                    object parentID = placeTable.Rows[j]["Parent"];
                    if (parentID.GetType() != typeof(DBNull))
                    {
                       for (int i = 0; i < rows; i++)
                       {
                            if (places[i].ID == (int)parentID)
                                places[j].place.SetChildOf(places[i].place);
                       }
                    }
                }

                return places;
            }
        }
    }
}
