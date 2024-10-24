using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
            connectionString = ConfigurationManager.ConnectionStrings["WizardUnionDBConnectionString"].ConnectionString;
        }

        public static Place[] AcquirePlaces()
        {
            InitializeConntectionString();

            // SqlConnection implements IDisposable, so using using() will automatically close the SqlConnection when the using() statement is complete
            // otherwise use conenction.Close()
            // Both of these using() statements will use the same code block
            using (connection = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Place", connection))
            {
                // Normally connection.Open() would be used here, however the SqlDataAdapter does this for us

                DataTable placeTable = new DataTable();
                int rows = adapter.Fill(placeTable);

                Place[] places = new Place[rows];

                // Fill places (without parents)
                for (int i = 0; i < rows; i++)
                {
                    DataRow currentRow = placeTable.Rows[i];
                    places[i] = new Place((double)currentRow["Cycles Per Eon"], (string)currentRow["Name"]);
                }
                // Assign parents to places
                for (int i = 0; i < rows; i++)
                {
                    DataRow currentRow = placeTable.Rows[i];
                    DataRow? place = placeTable.Rows.Find(currentRow["ParentId"]);

                    if (place == null) continue;

                    places[i].SetChildOf(Array.Find(places, (parent) => parent.Name == (string)place["Name"]));
                }

                // Confirm if this works before allowing use, this was moved from test app because app.config is in this domain library
                // Therefore, all database acquisitions need to come from this library.

                return places;
            }
        }
    }
}
