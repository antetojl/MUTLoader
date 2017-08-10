using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.VisualBasic;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace MUTLoader
{
    /// <summary>
    ///     Interaction logic for PlayerManager.xaml
    /// </summary>
    public partial class PlayerManager : INotifyPropertyChanged
    {
        private static bool _again;
        public string TextDisplay
        {
            get { return Text ? "Turn texting OFF" : "Turn texting ON"; }
        }
        public static bool Text;
        private static int _delay = 30000;
        public static List<PlayerInfo> Players = new List<PlayerInfo>();
        private const string ACCOUNT_SID = "AC48b53aeca95fd4f7cb402e949efe34cd";
        private const string AUTH_TOKEN = "15426b96c97f91a0faedb3ca5d8f8da1";
        private const string TWILIO_NUMBER = "+17039409022";
        private static string _myNumber = "+17033419466";
        public static StringBuilder TextStringBuilder = new StringBuilder();
        public static string PathDownload;

        /// <summary>
        ///     Interaction logic to begin link between UI and code behind.
        /// </summary>
        public PlayerManager()
        {
            _again = false;
            PathDownload = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            TwilioClient.Init(ACCOUNT_SID, AUTH_TOKEN);
            InitializeComponent();
            DataContext = this;

            FileStream fs;
            var formatter = new BinaryFormatter();

            //settings file exists
            if (File.Exists("Settings.bin"))
            {
                fs = new FileStream("Settings.bin", FileMode.Open);
                var settings = (List<int>) formatter.Deserialize(fs);
                _delay = settings[0];
                Text = settings[1] != 0;
            }

            //number file exists
            if (File.Exists("Number.bin"))
            {
                fs = new FileStream("Number.bin", FileMode.Open);
                _myNumber = (string) formatter.Deserialize(fs);
            }

            //players file exists
            if (File.Exists("SavedPlayers.bin"))
            {
                fs = new FileStream("SavedPlayers.bin", FileMode.Open);
                Players = (List<PlayerInfo>) formatter.Deserialize(fs);
            }
        }

        /// <summary>
        ///     Called when Go Button is clicked.
        /// </summary>
        /// <param name="sender">Not used.</param>
        /// <param name="e">Not used.</param>
        private void ExecuteButton_OnClick(object sender, RoutedEventArgs e)
        {
            _again = true;
            ExecuteButton.IsEnabled = false;
            if (Players.Count < 1)
            {
                return;
            }
            Background_Searcher();
        }

        /// <summary>
        ///     Work around to constantly search for players but not to freeze the UI.
        /// </summary>
        private static async void Background_Searcher()
        {
            //reset string for text
            TextStringBuilder.Clear();

            while (_again)
            {
                //Lists to iterate through for PlayerInfo and Threads
                var threads = new List<Thread>();

                //Create threads and run program for each player
                foreach (var p in Players)
                {
                    var t = new Thread(p.Connect);
                    threads.Add(t);
                    t.Start();
                    await Task.Delay(2500);
                }

                //Join all threads at finish so program continues without hitting enter on pop-up message.
                foreach (var t in threads)
                {
                    t.Join();
                }

                //Time between refresh
                await Task.Delay(_delay);
            }
        }

        /// <summary>
        ///     Called when Add button is clicked.
        /// </summary>
        /// <param name="sender">Not used.</param>
        /// <param name="e">Not used.</param>
        private void AddPlayerButton_OnClick(object sender, RoutedEventArgs e)
        {
            int parsed;

            //all true = value input
            if (int.TryParse(OVRBox.Text, out parsed) && int.TryParse(IDBox.Text, out parsed) &&
                int.TryParse(PriceBox.Text, out parsed))
            {
                var p = new PlayerInfo(int.Parse(OVRBox.Text), NameBox.Text, int.Parse(IDBox.Text),
                    int.Parse(PriceBox.Text));
                Players.Add(p);
                MessageBox.Show(string.Format("Added {0} OVR {1} to search list!", OVRBox.Text, NameBox.Text),
                    "Added!");

                //resets text boxes
                OVRBox.Text = "";
                NameBox.Text = "";
                IDBox.Text = "";
                PriceBox.Text = "";
            }

            else
            {
                MessageBox.Show("Please use valid input!", "Error");
            }
        }

        /// <summary>
        ///     Called when window is closed in normal flow.
        /// </summary>
        /// <param name="sender">Not used.</param>
        /// <param name="e">Not used.</param>
        private void Window_Closed(object sender, EventArgs e)
        {
            var text = Text ? 1 : 0;
            var settings = new List<int> {_delay, text};
            //serialize players and settings
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("SavedPlayers.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, Players);
            stream = new FileStream("Settings.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, settings);
            stream = new FileStream("Number.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, _myNumber);
            stream.Close();
        }

        /// <summary>
        ///     Logic for removing player from list of PlayerInfo items.
        /// </summary>
        /// <param name="id">ID of player to remove.</param>
        private static void RemovePlayer(int id)
        {
            var success = false;
            PlayerInfo deleted = null;
            foreach (var p in Players)
            {
                if (p.ID != id)
                {
                    continue;
                }
                deleted = p;
                success = true;
                break;
            }

            if (success)
            {
                var result =
                    MessageBox.Show(
                        string.Format("Are you sure you want to remove {0} from the player list?", deleted),
                        "WARNING", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    Players.Remove(deleted);
                    MessageBox.Show(
                        string.Format("Removed {1} {2} with ID {0}!", deleted.ID, deleted.OVR, deleted.name));
                }

                else
                {
                    MessageBox.Show(string.Format("Player with ID {0} not removed!", id), "Warning");
                }
            }

            else
            {
                MessageBox.Show(string.Format("Player with ID {0} not found!", id), "Error");
            }
        }

        /// <summary>
        ///     Called when Remove button is clicked.
        /// </summary>
        /// <param name="sender">Not used.</param>
        /// <param name="e">Not used.</param>
        private void RemovePlayerButton_OnClick(object sender, RoutedEventArgs e)
        {
            var sb = new StringBuilder();
            foreach (var p in Players)
            {
                sb.Append(p + "\n");
            }
            var response =
                Interaction.InputBox(string.Format("What is the ID of the player you wish to remove? \n\n {0}", sb),
                    "Remove");

            if (response.Equals(""))
            {
                return;
            }

            int parsed;
            if (int.TryParse(response, out parsed))
            {
                RemovePlayer(parsed);
            }

            else
            {
                MessageBox.Show("Please use a valid ID!", "Error");
            }
        }

        /// <summary>
        ///     Activated when View Players button is clicked.
        /// </summary>
        /// <param name="sender">Not used.</param>
        /// <param name="e">Not used.</param>
        private void ViewPlayersButton_OnClick(object sender, RoutedEventArgs e)
        {
            var sb = new StringBuilder();

            foreach (var p in Players)
            {
                var s = string.Format("Player {0}: {1}\n",
                    Players.IndexOf(p), p);
                sb.Append(s);
            }

            MessageBox.Show(sb.ToString(), "Players");
        }

        /// <summary>
        ///     Activated when Help button is clicked.
        /// </summary>
        /// <param name="sender">Not used.</param>
        /// <param name="e">Not used.</param>
        private void HelpButtonClick(object sender, RoutedEventArgs e)
        {
            var msg = new StringBuilder();
            msg.Append("Step 1: Add desired player to your watchlist on MUTHead.\n\n");
            msg.Append("Step 2: Right click on your watchlist and select 'View Page Source'.\n\n");
            msg.Append("Step 3: Find the player you want in the website's HTML by CTRL+F and typing their name. " +
                       "Once you find their name, look for the /watchlist/price-refresh/xxxxx link next to their name, and copy this number (the xxxxx).\n\n");
            msg.Append(
                "Step 4: Use this number as the ID inside the app.  You can put the player's name and OVR (purely for usability) " +
                "as well as your max price for this card and the ID you copied.  Your max price is the price that whenever this card " +
                "is found under that price, the alert will pop-up on your screen.\n\n");
            msg.Append(
                "Step 5: Once you have added the players you want to search for, hit GO.  The program will continuously refresh " +
                "through the items every 30 seconds (modify this value in the Settings tab).  Once a player is found below the desired " +
                "price, the notification sound and message box will appear, containing the max price and current price of the card, as well as the OVR and name. " +
                "The program will not loop through the players again, nor start the time between refresh, until the OK button is pressed.  Press the Stop button " +
                "to keep the program from looping through the players again.\n\n");
            msg.Append("Happy sniping :) !!");

            MessageBox.Show(msg.ToString(), "Help");
        }

        /// <summary>
        ///     Activated when Stop button is clicked.
        /// </summary>
        /// <param name="sender">Not used.</param>
        /// <param name="e">Not used.</param>
        private void StopButton_OnClickButtonClick(object sender, RoutedEventArgs e)
        {
            _again = false;
            ExecuteButton.IsEnabled = true;
        }

        /// <summary>
        ///     Activated when Change refresh rate button is clicked.
        /// </summary>
        /// <param name="sender">Not used.</param>
        /// <param name="e">Not used.</param>
        private void DelayButton_OnClick(object sender, RoutedEventArgs e)
        {
            var response = Interaction.InputBox(
                string.Format("How many seconds would you like to have between refresh?{0}" +
                              "Current refresh rate is {1} seconds.", Environment.NewLine, _delay / 1000), "Remove");

            if (response.Equals(""))
            {
                return;
            }

            int parsed;

            if (int.TryParse(response, out parsed))
            {
                parsed = int.Parse(response, (NumberStyles) parsed);
                if (parsed <= 2147483)
                {
                    _delay = parsed * 1000;
                }
                else
                {
                    MessageBox.Show("Please use a smaller number for delay!", "Error");
                }
            }

            else
            {
                MessageBox.Show("Please use valid number for delay!", "Error");
            }
        }

        /// <summary>
        ///     Activated when Modify player button is clicked.
        /// </summary>
        /// <param name="sender">Not used.</param>
        /// <param name="e">Not used.</param>
        private void ModifyPlayerButton_OnClick(object sender, RoutedEventArgs e)
        {
            var sb = new StringBuilder();
            foreach (var p in Players)
            {
                sb.Append(p + "\n");
            }
            var response =
                Interaction.InputBox(
                    string.Format("What is the ID of the player that you want to change the max price for? \n\n {0}",
                        sb),
                    "Remove");

            if (response.Equals(""))
            {
                return;
            }

            int parsed;
            if (int.TryParse(response, out parsed))
            {
                ModifyPlayer(parsed);
            }

            else
            {
                MessageBox.Show("Please use a valid ID!", "Error");
            }
        }

        /// <summary>
        ///     Logic for modifying a player in list of PlayerInfo items.
        /// </summary>
        /// <param name="id">ID of player to modify.</param>
        private static void ModifyPlayer(int id)
        {
            var success = false;
            PlayerInfo modified = null;
            foreach (var p in Players)
            {
                if (p.ID != id)
                {
                    continue;
                }
                modified = p;
                success = true;
                break;
            }

            if (success)
            {
                var result =
                    Interaction.InputBox(string.Format(
                        "{0} {1} current max price is {2:n0}.  What price should this be changed to? Do not add commas.",
                        modified.OVR, modified.name, modified.MaxPrice), "Change price");
                if (result == "")
                {
                    return;
                }

                int parsed;
                if (int.TryParse(result, out parsed) && int.Parse(result) >= 0)
                {
                    parsed = int.Parse(result);
                    Players.Remove(modified);
                    modified.MaxPrice = parsed;
                    Players.Add(modified);
                    MessageBox.Show(
                        string.Format("Modified {1} {2} price to {0:n0}!", modified.MaxPrice, modified.OVR,
                            modified.name));
                }

                else
                {
                    MessageBox.Show("Please use a valid price!", "Error");
                }
            }

            else
            {
                MessageBox.Show(string.Format("Player with ID {0} not found!", id), "Error");
            }
        }

        /// <summary>
        ///     Activated when Text button is clicked.
        /// </summary>
        /// <param name="sender">Not used.</param>
        /// <param name="e">Not used.</param>
        private void TextButton_OnClick(object sender, RoutedEventArgs e)
        {
            Text = !Text;
            TextChanged();
        }

        /// <summary>
        ///     Returns the last player in the list's info.
        /// </summary>
        /// <returns>PlayerInfo item that is last players.</returns>
        public static PlayerInfo LastPlayerInfo()
        {
            return Players.Count > 0 ? Players[Players.Count - 1] : null;
        }

        /// <summary>
        ///     Helper method to send text.
        /// </summary>
        public static void SendText()
        {
            _again = false;
            try
            {
                var text = MessageResource.Create(
                    new PhoneNumber(_myNumber),
                    from: new PhoneNumber(TWILIO_NUMBER),
                    body: TextStringBuilder.ToString());
                MessageBox.Show(string.Format("Success!{0}{0}{1}", Environment.NewLine, text.Body));
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error!{0}{0}{1}", Environment.NewLine, ex));
            }
        }

        /// <summary>
        ///     Activated when Change Number button is clicked.
        /// </summary>
        /// <param name="sender">Not used.</param>
        /// <param name="e">Not used.</param>
        private void NumberButton_OnClick(object sender, RoutedEventArgs e)
        {
            var newNumber =
                Interaction.InputBox(string.Format("Your current phone number to receive text message alerts is: {0}." +
                                                   "{1}{1}What number would you like to receive text message alerts to when a player is available for under your max price?  " +
                                                   "{1}{1}You may add dashes/parentheses or omit them, but please do not add the +1 at the start of the number, as it is added for you.",
                    _myNumber, Environment.NewLine), "Change number");
            if (newNumber == "")
            {
                return;
            }

            if (newNumber.Contains("-"))
            {
                newNumber = newNumber.Replace("-", "");
            }

            if (newNumber.Contains("("))
            {
                newNumber = newNumber.Replace("(", "");
            }

            if (newNumber.Contains(")"))
            {
                newNumber = newNumber.Replace(")", "");
            }

            long parsed;
            if (long.TryParse(newNumber, out parsed))
            {
                _myNumber = "+1" + parsed;
            }

            else
            {
                MessageBox.Show("Please use a valid number!", "Error");
            }
        }

        /// <summary>
        ///     Method for handling Text attribute changing.
        /// </summary>
        private void TextChanged()
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("TextDisplay"));
            }
        }

        /// <summary>
        ///     Activated when Reset Settings button is clicked.
        /// </summary>
        /// <param name="sender">Not used.</param>
        /// <param name="e">Not used.</param>
        private void ResetSettings_OnClick(object sender, RoutedEventArgs e)
        {
            var response = MessageBox.Show("Are you sure you you want to reset all settings?", "Warning",
                MessageBoxButton.YesNo);
            if (response == MessageBoxResult.Yes)
            {
                _delay = 30000;
                Text = false;
                TextChanged();
                _myNumber = "+1";
            }
        }

        /// <summary>
        ///     Activated when Reset Players button is clicked.
        /// </summary>
        /// <param name="sender">Not used.</param>
        /// <param name="e">Not used.</param>
        private void ResetPlayers_OnClick(object sender, RoutedEventArgs e)
        {
            var response = MessageBox.Show("Are you sure you you want to reset all player info?", "Warning",
                MessageBoxButton.YesNo);
            if (response == MessageBoxResult.Yes)
            {
                Players = new List<PlayerInfo>();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}