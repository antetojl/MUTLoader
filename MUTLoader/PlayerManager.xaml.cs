using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using Microsoft.VisualBasic;

namespace MUTLoader
{
    /// <summary>
    ///     Interaction logic for PlayerManager.xaml
    /// </summary>
    public partial class PlayerManager
    {
        public List<PlayerInfo> players = new List<PlayerInfo>();
        private int _delay = 30000;

        public PlayerManager()
        {
            InitializeComponent();

            try
            {
                var fs = new FileStream("SavedPlayers.bin", FileMode.Open);
                var formatter = new BinaryFormatter();
                players = (List<PlayerInfo>) formatter.Deserialize(fs);
            }
            catch (Exception ex)
            {
                // ignored
            }
        }

        private void ExecuteButton_OnClick(object sender, RoutedEventArgs e)
        {
            //if (players.Count > 10)
            //{
            //    _delay += (players.Count - 10) * 2500;
            //}

            while (true)
            {
                //Lists to iterate through for PlayerInfo and Threads
                var threads = new List<Thread>();

                //Create threads and run program for each player
                foreach (var p in players)
                {
                    var t = new Thread(p.Connect);
                    threads.Add(t);
                    t.Start();
                    Thread.Sleep(2500);
                }

                //Join all threads at finish so program continues without hitting enter on pop-up message.
                foreach (var t in threads)
                    t.Join();

                //Kills Internet Explorer with cmd.exe and then closes cmd.exe
                var process = new Process();
                var startInfo =
                    new ProcessStartInfo
                    {
                        WindowStyle = ProcessWindowStyle.Hidden,
                        FileName = "cmd.exe",
                        Arguments = "/C taskkill.exe /f /im iexplore.exe"
                    };
                process.StartInfo = startInfo;
                process.Start();
                process.Close();
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
                players.Add(p);
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

        public static void Start(List<PlayerInfo> players)
        {
            //Lists to iterate through for PlayerInfo and Threads
            var threads = new List<Thread>();

            //Create threads and run program for each player
            foreach (var p in players)
            {
                var t = new Thread(p.Connect);
                threads.Add(t);
                t.Start();
                Thread.Sleep(2500);
            }

            //Join all threads at finish so program continues without hitting enter on pop-up message.
            foreach (var t in threads)
                t.Join();

            //Kills Internet Explorer with cmd.exe and then closes cmd.exe
            var process = new Process();
            var startInfo =
                new ProcessStartInfo
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = "cmd.exe",
                    Arguments = "/C taskkill.exe /f /im iexplore.exe"
                };
            process.StartInfo = startInfo;
            process.Start();
            process.Close();
        }

        private void PlayerManager_OnClosed(object sender, EventArgs e)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("SavedPlayers.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, players);
            stream.Close();
        }

        public void RemovePlayer(int id)
        {
            var success = false;
            PlayerInfo deleted = null;
            foreach (var p in players)
            {
                if (p.ID == id)
                {
                    deleted = p;
                    success = true;
                    break;
                }
            }

            if (success)
            {
                var result =
                    MessageBox.Show(
                        string.Format("Are you sure you want to remove {0} from the player list?", deleted.ToString()),
                        "WARNING", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    players.Remove(deleted);
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
            StringBuilder sb = new StringBuilder();
            foreach (PlayerInfo p in players)
            {
                sb.Append(p.ToString() + "\n");
            }
            var response = Interaction.InputBox(string.Format("What is the ID of the player you wish to remove? \n\n {0}", sb.ToString()), "Remove");
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

            foreach (var p in players)
            {
                var s = string.Format("Player {0}: {1}\n",
                    players.IndexOf(p), p.ToString());
                sb.Append(s);
            }

            MessageBox.Show(sb.ToString(), "Players");
        }

        private void Control_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void HelpButtonClick(object sender, RoutedEventArgs e)
        {
            StringBuilder msg = new StringBuilder();
            msg.Append("Step 1: Add desired player to your watchlist on MUTHead.\n\n");
            msg.Append("Step 2: Right click on your watchlist and select 'View Page Source'.\n\n");
            msg.Append("Step 3: Find the player you want in the website's HTML by CTRL+F and typing their name. " +
                       "Once you find their name, look for the /watchlist/price-refresh/xxxxx link next to their name, and copy this number (the xxxxx).\n\n");
            msg.Append(
                "Step 4: Use this number as the ID inside the app.  You can put the player's name and OVR (purely for usability) " +
                "as well as your max price for this card and the ID you copied.  Your max price is the price that whenever this card " +
                "is found under that price, the alert will pop-up on your screen.\n\n");
            msg.Append(
                "Step 5: Once you have added the players you want to search for, hit execute.  The program will continuously loop " +
                "through the items every 30 seconds (or more if there are more than 10 players in the list).  Once a player is found below the desired " +
                "price, the notification sound and message box will appear, containing the max price and current price of the card, as well as the OVR and name. " +
                "The program will not continue until this message box is closed.\n\n");
            msg.Append("Happy sniping :) !!");

            MessageBox.Show(msg.ToString(), "Help");




        }
    }
}