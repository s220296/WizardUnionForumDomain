using Microsoft.Data.SqlClient;
using System.Data;
using WizardUnion;
using WizardUnion.Birth;
using WizardUnion.MagicAndSpells;
using WizardUnion.Names;
using WizardUnion.Places;
using WizardUnion.Messaging;
using WizardUnion.Unions;

namespace WU_Test;

public static class DataSubmission
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
    // Record(Wizard), Record(Place), etc...
    public static void RecordTextMessage(TextMessage _message, IMessageSender _sender, IMessageReceiver _receiver)
    {
        if (_message is null || _sender is null || _receiver is null)
            return;

        IDTypeItem sender = (IDTypeItem)_sender.GetSenderID();
        IDTypeItem receiver = (IDTypeItem)_receiver.GetReceiverID();

        InitializeConntectionString();

        string query = "INSERT INTO TextMessages VALUES (@WizardSenderID, @Message, @UnionReceiverID, @WizardReceiverID)";

        using(connection = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand(query, connection))
        {
            // We need connection.Open() here because we are not using a data adapter
            connection.Open();

            cmd.Parameters.AddWithValue("@WizardSenderID", sender.ID);
            cmd.Parameters.AddWithValue("@Message", _message.Text);

            if (receiver.Type.Equals(typeof(Wizard)))
            {
                cmd.Parameters.AddWithValue("@WizardReceiverID", receiver.ID);
                cmd.Parameters.AddWithValue("@UnionReceiverID", DBNull.Value);
            }
            else if (receiver.Type.Equals(typeof(Union)))
            {
                cmd.Parameters.AddWithValue("@UnionReceiverID", receiver.ID);
                cmd.Parameters.AddWithValue("@WizardReceiverID", DBNull.Value);
            }

            cmd.ExecuteNonQuery();
        }

        // INCOMPELTE
    }
}
