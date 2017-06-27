using System;
using System.Diagnostics;
using System.IO;
using System.Media;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using SHDocVw;
using Twilio;
using Twilio.Clients;
using Twilio.Http;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
namespace MUTLoader
{
    [Serializable]
    public class PlayerInfo
    {
        private static InternetExplorerMedium ie;
        public int ID;
        public int MaxPrice;
        public string name;
        public int OVR;
        private readonly string pathDownload;
        
        public PlayerInfo(int OVR, string name, int ID, int MaxPrice)
        {
            this.OVR = OVR;
            this.name = name;
            this.ID = ID;
            this.MaxPrice = MaxPrice;
            var pathUser = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            pathDownload = Path.Combine(pathUser, "Downloads");
        }

        public PlayerInfo()
        {
            OVR = 0;
            name = "no one";
            ID = 0;
        }

        public void PriceCheck()
        {
            var regexPattern = new Regex(@"\d\d?\d?\d?\d?\d?\d?\d?");
            var files = Directory.GetFiles(pathDownload, string.Format("*{0}*", ID));

            var s = "";
            try
            {
                s = File.ReadAllText(files[0]);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not find file!", "Error");
            }

            try
            {
                s = s.Substring(47, 9);
                var match = regexPattern.Match(s);
                File.Delete(files[0]);
                if (match.Success && int.Parse(match.Value) < MaxPrice) //Match and it's less than what I want to pay
                {
                    var price = int.Parse(match.Value);
                    var message = string.Format("{0} {1} up for {2:n0}!  Your limit for this card is {3:n0}!",
                        OVR, name, price, MaxPrice);
                    if (PlayerManager.Text)
                    {
                        PlayerManager.TextStringBuilder.Append(string.Format("{0}{1}", message, Environment.NewLine));
                        if (ID == PlayerManager.LastPlayerInfo().ID)
                        {
                            PlayerManager.SendText();
                        }
                    }
                    else
                    {
                        SystemSounds.Asterisk.Play();
                        MessageBox.Show(message, "Good price :)");
                    }
                }
            }
            catch (Exception ex)
            {

            }
            
        }

        public void Connect()
        {
            ie = new InternetExplorerMedium();
            ie.Navigate("http://www.muthead.com/watchlist/price-refresh/" + ID);
            Thread.Sleep(1000); //sleep to open up page
            Process.Start("MUTLoadEnter.exe");
            Thread.Sleep(500); // sleep to hit enter
            PriceCheck();

        }

        public override string ToString()
        {
            return string.Format("{0} OVR {1}, ID = {2}, Max Price = {3:n0}", OVR, name, ID, MaxPrice);
        }
    }
}