using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Windows;
using Microsoft.VisualBasic;

namespace MUTLoader
{
    /// <summary>
    ///     Interaction logic for PlayerManager.xaml
    /// </summary>
    public partial class PlayerManager
    {
        private bool _again;
        private static int _delay = 30000;
        private static List<PlayerInfo> _players = new List<PlayerInfo>();

        public PlayerManager()
        {
            _again = false;
            InitializeComponent();

            try
            {
                var fs = new FileStream("SavedPlayers.bin", FileMode.Open);
                var formatter = new BinaryFormatter();
                _players = (List<PlayerInfo>) formatter.Deserialize(fs);
            }
            catch (Exception ex)
            {
                // nothing to load
            }
        }

        private void ExecuteButton_OnClick(object sender, RoutedEventArgs e)
        {
            _again = true;
            if (_players.Count <= 0)
            {
                return;
            }
            var worker = new BackgroundWorker();
            worker.DoWork += Background_Searcher;
            worker.RunWorkerAsync();
        }

        private void Background_Searcher(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            while (_again)
            {
                //Lists to iterate through for PlayerInfo and Threads
                var threads = new List<Thread>();

                //Create threads and run program for each player
                foreach (var p in _players)
                {
                    var t = new Thread(p.Connect);
                    threads.Add(t);
                    t.Start();
                    Thread.Sleep(2500);
                }

                //Join all threads at finish so program continues without hitting enter on pop-up message.
                foreach (var t in threads)
                {
                    t.Join();
                }

                //Time between refresh
                Thread.Sleep(_delay);
            }
        }

        private void AddPlayerButton_OnClick(object sender, RoutedEventArgs e)
        {
            int parsed;

            //all true = value input
            if (int.TryParse(OVRBox.Text, out parsed) && int.TryParse(IDBox.Text, out parsed) &&
                int.TryParse(PriceBox.Text, out parsed))
            {
                var p = new PlayerInfo(int.Parse(OVRBox.Text), NameBox.Text, int.Parse(IDBox.Text),
                    int.Parse(PriceBox.Text));
                _players.Add(p);
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

        private void PlayerManager_OnClosed(object sender, EventArgs e)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("SavedPlayers.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, _players);
            stream.Close();
        }

        private void RemovePlayer(int id)
        {
            var success = false;
            PlayerInfo deleted = null;
            foreach (var p in _players)
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
                        string.Format("Are you sure you want to remove {0} from the player list?", deleted.ToString()),
                        "WARNING", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    _players.Remove(deleted);
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

        private void RemovePlayerButton_OnClickPlayerButton_OnClick(object sender, RoutedEventArgs e)
        {
            var sb = new StringBuilder();
            foreach (var p in _players)
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

        private void ViewPlayersButton_OnClick(object sender, RoutedEventArgs e)
        {
            var sb = new StringBuilder();

            foreach (var p in _players)
            {
                var s = string.Format("Player {0}: {1}\n",
                    _players.IndexOf(p), p.ToString());
                sb.Append(s);
            }

            MessageBox.Show(sb.ToString(), "Players");
        }

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
                "Step 5: Once you have added the players you want to search for, hit execute.  The program will continuously refresh " +
                "through the items every 30 seconds (modify this value in the Settings tab).  Once a player is found below the desired " +
                "price, the notification sound and message box will appear, containing the max price and current price of the card, as well as the OVR and name. " +
                "The program will not loop through the players again, nor start the time between refresh, until the OK button is pressed.  Press the Stop button " +
                "to keep the program from looping through the players again.\n\n");
            msg.Append("Happy sniping :) !!");

            MessageBox.Show(msg.ToString(), "Help");
        }

        private void StopButton_OnClickButtonClick(object sender, RoutedEventArgs e)
        {
            _again = false;
        }

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

        private void ModifyPlayerButton_OnClickPlayerButton_OnClickPlayerButton_OnClick(object sender, RoutedEventArgs e)
        {
            var sb = new StringBuilder();
            foreach (var p in _players)
            {
                sb.Append(p + "\n");
            }
            var response =
                Interaction.InputBox(string.Format("What is the ID of the player that you want to change the max price for? \n\n {0}", sb),
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

        private static void ModifyPlayer(int id)
        {
            var success = false;
            PlayerInfo modified = null;
            foreach (var p in _players)
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
                    Interaction.InputBox(string.Format("{0} {1} current max price is {2:n0}.  What price should this be changed to? Do not add commas.",
                                        modified.OVR, modified.name, modified.MaxPrice),"Change price");
                if (result == "")
                {
                    return;
                }

                int parsed;
                if (int.TryParse(result, out parsed) && int.Parse(result) >= 0)
                {
                    parsed = int.Parse(result);
                    _players.Remove(modified);
                    modified.MaxPrice = parsed;
                    _players.Add(modified);
                    MessageBox.Show(
                        string.Format("Modified {1} {2} price to {0:n0}!", modified.MaxPrice, modified.OVR, modified.name));
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
    }
}