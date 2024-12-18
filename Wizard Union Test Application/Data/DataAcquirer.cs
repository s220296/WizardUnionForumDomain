﻿using Microsoft.Data.SqlClient;
using System.Data;
using WizardUnion;
using WizardUnion.Birth;
using WizardUnion.MagicAndSpells;
using WizardUnion.Names;
using WizardUnion.Places;

namespace WU_Test;

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

    public static IDItem<TextMessage>[] GetMessagesToWizardFrom(int _senderID, int _receiverID)
    {
        InitializeConntectionString();

        string query = $"SELECT Id, Message FROM TextMessages WHERE WizardSenderID='{_senderID}'" +
            $"AND WizardReceiverID='{_receiverID}'";

        using (connection = new SqlConnection(connectionString))
        using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
        {
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            IDItem<TextMessage>[] result = new IDItem<TextMessage>[dataTable.Rows.Count];

            for (int i = 0; i < result.Length; i++)
            {
                // If message is null, maybe occurs during some refactor?
                if (dataTable.Rows[i]["Message"] is DBNull) continue;

                TextMessage message = new TextMessage((string)dataTable.Rows[i]["Message"]);
                result[i] = new IDItem<TextMessage>(message, (int)dataTable.Rows[i]["Id"]);
            }

            return result;
        }
    }

    public static void FillWizardToWizardTextMessages(UserProfile[] _wizards)
    {
        InitializeConntectionString();

        using (connection = new SqlConnection(connectionString))
        using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM TextMessages", connection))
        {
            // Initialize Data table
            DataTable messageTable = new DataTable();
            int rows = adapter.Fill(messageTable);
            // Initialize lists
            List<IDItem<TextMessage>> messages = new List<IDItem<TextMessage>>(rows);
            List<(int sender, int receiver)> senderReceivers = new List<(int, int)>(rows);
            // Write data from table into lists

            for (int i = 0; i < rows; i++)
            {
                if (messageTable.Rows[i]["WizardReceiverID"] is DBNull)
                {
                    // This function overload is only for wizard to wizard messages
                    continue;
                }

                TextMessage message = new TextMessage((string)messageTable.Rows[i]["Message"]);
                messages.Add(new IDItem<TextMessage>(message, (int)messageTable.Rows[i]["Id"]));
                
                int senderID = (int)messageTable.Rows[i]["WizardSenderID"];
                int receiverID = (int)messageTable.Rows[i]["WizardReceiverID"];

                senderReceivers.Add((senderID, receiverID));
            }
            // Assign data to user profiles / messagers
            for (int i = 0; i < messages.Count && i < senderReceivers.Count; i++)
            {
                UserProfile sender = _wizards[0];
                UserProfile receiver = _wizards[0];

                bool gotSender = false;
                bool gotReceiver = false;

                for (int wizard = 0; wizard < _wizards.Length; wizard++) 
                {
                    if (senderReceivers[i].sender == _wizards[wizard].Wizard.ID)
                    { sender = _wizards[wizard]; gotSender = true; }
                    if (senderReceivers[i].receiver == _wizards[wizard].Wizard.ID)
                    { receiver = _wizards[wizard]; gotReceiver = true; }
                }

                if (gotSender && gotReceiver)
                {
                    sender.Messager.Messages.Add((messages[i].Item, sender.Messager, receiver.Messager));
                    receiver.Messager.Messages.Add((messages[i].Item, sender.Messager, receiver.Messager));
                }
            }
        }
    }

    public static IDItem<Wizard> GetWizardByName(string _name)
    {
        InitializeConntectionString();

        using (connection = new SqlConnection(connectionString))
        using (SqlDataAdapter adapter = new SqlDataAdapter($"SELECT * FROM Wizards WHERE Name='{_name}'", connection))
        {
            DataTable dt = new DataTable();
            int rows = adapter.Fill(dt);

            if (rows <= 0) throw new InvalidDataException($"There are no wizards with the name {_name}");
            // NOTE: there may be multiple wizards of the same name

            BirthDetails birth = new BirthDetails(Universe.Place, 0.5d);
            MagicProfile magic = new MagicProfile(new SpellMastery("Fire"), SpellProfileList.Empty);

            SingleName name = new SingleName((string)dt.Rows[0]["Name"]);

            Wizard wizard = new Wizard(name, birth, magic);

            return new IDItem<Wizard>(wizard, (int)dt.Rows[0]["Id"]);
        }
    }

    public static IDItem<Wizard>[] AcquireAllWizards()
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
            IDItem<Wizard>[] wizards = new IDItem<Wizard>[rows];
            BirthDetails defaultDetails = new BirthDetails(Universe.Place, 0.5d);
            MagicProfile defaultProfile = new MagicProfile(new SpellMastery(), SpellProfileList.Empty);

            // Retrieve all wizards from DB
            for (int i = 0; i < rows; i++)
            {
                string name = (string)wizardTable.Rows[i]["Name"];
                int ID = (int)wizardTable.Rows[i]["Id"];

                wizards[i].Item = new Wizard(new SingleName(name), defaultDetails, defaultProfile);
                wizards[i].ID = ID;
            }

            return wizards;
        }
    }

    public static IDItem<Place>[] AcquireAllPlaces()
    {
        InitializeConntectionString();

        using (connection = new SqlConnection(connectionString))
        using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Places", connection))
        {
            DataTable placeTable = new DataTable();
            int rows = adapter.Fill(placeTable);

            // Initialize
            IDItem<Place>[] places = new IDItem<Place>[rows];

            // Retrieve all places from DB
            for (int i = 0; i < rows; i++)
            {
                string name = (string)placeTable.Rows[i]["Name"];
                double cyclesPerEon = (double)placeTable.Rows[i]["CyclesPerEon"];
                int ID = (int)placeTable.Rows[i]["Id"];

                places[i].Item = new Place(cyclesPerEon, name);
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
                            places[j].Item.SetChildOf(places[i].Item);
                   }
                }
            }

            return places;
        }
    }
}
