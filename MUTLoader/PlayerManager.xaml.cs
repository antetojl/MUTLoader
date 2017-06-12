using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public List<PlayerInfo> players = new List<PlayerInfo>();

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
            while (true)
            {
                Start(players);
                Thread.Sleep(10000);
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
    }
}