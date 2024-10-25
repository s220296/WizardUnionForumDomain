using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardUnion.Birth;
using WizardUnion.Places;

namespace WizardUnion.DataAcquisition
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

        public static Wizard[] AcquireAllWizards()
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

                Wizard[] wizards = new Wizard[rows];
                BirthDetails defaultDetails = new BirthDetails(Universe.Place, 0.5d);

                Console.WriteLine(wizardTable.TableName); 

                for (int i = 0; i < rows; i ++)
                {
                    string name = (string)wizardTable.Rows[i]["Name"];
                    wizards[i] = new Wizard(new Names.SingleName(name), defaultDetails);
                }

                return wizards;
            }
        }
    }
}
