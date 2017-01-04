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
        delegate void myCallback(List<streamer> followedStreams);
        private streamer streamerTmpNotif;
        private int delayLenght;

        private List<streamer> followedStreams;

        public Form1()
        {
            InitializeComponent();
        }

        private void validateButton_Click(object sender, EventArgs e)
        {
            if (state)
            {
                state = false;
                nameBox.Enabled = true;
                delayBox.Enabled = true;
                validateButton.Text = "Start";
            } else if (delayBox.Text != "" && nameBox.Text != "")
            {
                state = true;
                followedStreams = twitchInit.getFollowedStreams(nameBox.Text);
                delayLenght = Int32.Parse(delayBox.Text);

                Thread thread = new Thread(this.callForScan);
                thread.Start(followedStreams);
                nameBox.Enabled = false;
                delayBox.Enabled = false;
                validateButton.Text = "Stop";
            } else
            {
                MessageBox.Show("Error");
            }
        }

        private void callForScan(object followedStreamsObj)
        {
            List<streamer> followedStreams = (List<streamer>)followedStreamsObj;

            if (followedStreams.Count == 0)
            {
                MessageBox.Show("No streamer followed");
            }

            //Thread loop starting
            while (state || followedStreams.Count == 0)
            {
                scanUserFollow(followedStreams);

                int multiplicator = 1000;
                long refreshTime = delayLenght * multiplicator;

                for (int i = 0; i < multiplicator; i++)
                {
                    Thread.Sleep(delayLenght);
                    if (!state) break;
                }
            }
        }

        private void scanUserFollow(List<streamer> followedStreams)
        {
            if (this.streamsFolowed.InvokeRequired)
            {
                myCallback d = new myCallback(scanUserFollow);
                this.Invoke(d, new object[] { followedStreams });
            } else
            {
                foreach (streamer stream in followedStreams)
                {
                    twitchInit.isOnline(stream);
                }

                streamsFolowed.Text = "";

                int i = 0;
                foreach (streamer stream in followedStreams)
                {
                    if (stream.State)
                    {
                        streamsFolowed.Text += stream.displayName + " is now ONLINE\n";
                        if (stream.stateHasChanged)
                        {
                            this.Hide();
                            streamerTmpNotif = stream;
                            notifyIcon1.ShowBalloonTip(2000, stream.displayName + " is online", "Click me to watch " + stream.displayName + " stream !", ToolTipIcon.Info);
                            stream.stateHasChanged = false;
                        }
                    } else
                    {
                        streamsFolowed.Text += stream.displayName + " is now OFFLINE\n";
                    }
                    i++;
                }
                Console.WriteLine("Check");
            }
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
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

        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(streamerTmpNotif.link);
        }

    }
}
