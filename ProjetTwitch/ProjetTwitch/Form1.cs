using projetTwitch;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjetTwitch
{
    public partial class Form1 : Form
    {
        private twitchAPI twitchInit = new twitchAPI("oc651hjfox0rk94wahy25hpm8d17o5");
        private bool state = false;
        delegate void SetTextCallback(string text);

        public Form1()
        {
            InitializeComponent();
        }

        private void validateButton_Click(object sender, EventArgs e)
        {
            if (state) {
                state = false;
            } else if (delayBox.Text != "" && nameBox.Text != "") {
                state = true;
                Thread thread = new Thread(this.callForScan);
                thread.Start(nameBox.Text);
            } else {
                MessageBox.Show("Erreur");
            }
            
        }

        private void callForScan(object name)
        {
            while (state){
                scanUserFollow((string)name);
                Thread.Sleep(Int32.Parse(delayBox.Text)*1000);
            }
        }

        private void scanUserFollow(string name)
        {
            List<streamer> followedStreams = twitchInit.getFollowedStreams(name);

            /*streamsFolowed.Text = "";

            foreach (streamer stream in followedStreams)
            {
                if (twitchInit.isOnline(stream.name))
                {
                    streamsFolowed.Text += stream.displayName + " is now ONLINE\n";
                    //streamerState.ForeColor = Color.Green;
                }
                else
                {
                    streamsFolowed.Text += stream.displayName + " is now OFFLINE\n";
                    //streamerState.ForeColor = Color.Red;
                }
            }*/

            if (this.streamsFolowed.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(scanUserFollow);
                this.Invoke(d, new object[] { name });
            }
            else
            {
                streamsFolowed.Text = "";

                foreach (streamer stream in followedStreams)
                {
                    if (twitchInit.isOnline(stream.name))
                    {
                        streamsFolowed.Text += stream.displayName + " is now ONLINE\n";
                        //streamerState.ForeColor = Color.Green;
                    }
                    else
                    {
                        streamsFolowed.Text += stream.displayName + " is now OFFLINE\n";
                        //streamerState.ForeColor = Color.Red;
                    }
                }
            }
        }
    }
}
