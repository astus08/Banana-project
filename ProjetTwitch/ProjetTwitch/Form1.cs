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
        delegate void myCallback(string text, List<streamer> followedStreams);
        private streamer streamerTmpNotif;

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
            List<streamer> followedStreams = twitchInit.getFollowedStreams((string)name);
            while (state)
            {
                scanUserFollow((string)name, followedStreams);
                Thread.Sleep(Int32.Parse(delayBox.Text) * 1000);
            }
        }

        private void scanUserFollow(string name, List<streamer> followedStreams)
        {
            if (this.streamsFolowed.InvokeRequired)
            {
                myCallback d = new myCallback(scanUserFollow);
                this.Invoke(d, new object[] { name, followedStreams });
            }
            else
            {
                foreach (streamer stream in followedStreams)
                {
                    twitchInit.isOnline(stream);
                }

                streamsFolowed.Text = "";

                foreach (streamer stream in followedStreams)
                {
                    if (stream.state)
                    {
                        streamsFolowed.Text += stream.displayName + " is now ONLINE\n";
                        if (stream.stateHasChanged)
                        {
                            this.Hide();
                            streamerTmpNotif = stream;
                            notifyIcon1.ShowBalloonTip(1000, stream.displayName + " is online", "Click me to watch " + stream.displayName + " stream !", ToolTipIcon.Info);
                            stream.stateHasChanged = false;
                        }
                    }
                    else
                    {
                        streamsFolowed.Text += stream.displayName + " is now OFFLINE\n";
                    }
                }
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Process.Start(streamerTmpNotif.link);
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void hideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Move(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
            }
        }
    }
}
