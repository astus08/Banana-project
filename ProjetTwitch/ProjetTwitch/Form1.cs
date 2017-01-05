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
        delegate void myCallback();
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

                foreach(streamer stream in followedStreams) {
                    this.Controls.Remove(stream.MyLabel);
                }

                followedStreams.Clear();
            } else if (delayBox.Text != "" && nameBox.Text != "")
            {
                state = true;
                nameBox.Enabled = false;
                delayBox.Enabled = false;
                followedStreams = twitchInit.getFollowedStreams(nameBox.Text);
                delayLenght = Int32.Parse(delayBox.Text);

                #region Create labels reccursively
                Point actualPosition = new Point(12, 71);
                int spaceBetweenLabels = 18;

                foreach (streamer stream in followedStreams)
                {
                    stream.MyLabel.Location = actualPosition;
                    stream.MyLabel.Parent = this;
                    stream.MyLabel.AutoSize = true;

                    stream.MyLabel.MouseClick += MyLabel_MouseClick;

                    actualPosition.Y += spaceBetweenLabels;
                }
                #endregion
                
                Thread thread = new Thread(this.callForScan);
                thread.Start();
                
                validateButton.Text = "Stop";
            } else
            {
                MessageBox.Show("Error");
            }
        }
        
        private void callForScan()
        {
            if (followedStreams.Count == 0)
            {
                MessageBox.Show("No streamer followed");
            } else
            {
                //Thread loop starting
                while (state)
                {
                    scanUserFollow();

                    int multiplicator = 1000;
                    long refreshTime = delayLenght * multiplicator;

                    for (int i = 0; i < multiplicator; i++)
                    {
                        Thread.Sleep(delayLenght);
                        if (!state) break;
                    }
                }
            }
        }

        private void scanUserFollow()
        {
            bool needDlg = false;
            foreach (streamer stream in followedStreams)
            {
                if (stream.MyLabel.InvokeRequired)
                {
                    needDlg = true;
                    break;
                }
            }

            if (needDlg)
            {
                myCallback d = new myCallback(scanUserFollow);
                this.Invoke(d);
            } else
            {
                foreach (streamer stream in followedStreams)
                {
                    twitchInit.isOnline(stream);
                }

                int i = 0;
                foreach (streamer stream in followedStreams)
                {
                    if (stream.State)
                    {
                        stream.MyLabel.Text = stream.displayName + " is now ONLINE";
                        stream.MyLabel.ForeColor = Color.Green;
                        if (stream.stateHasChanged)
                        {
                            this.Hide();
                            streamerTmpNotif = stream;
                            
                            notifyIcon1.ShowBalloonTip(2000, stream.displayName + " is online", "Click me to watch " + stream.displayName + " stream !", ToolTipIcon.Info);
                            stream.stateHasChanged = false;
                        }
                    } else
                    {
                        stream.MyLabel.Text = stream.displayName + " is now OFFLINE";
                        stream.MyLabel.ForeColor = Color.Red;
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
        
        private void MyLabel_MouseClick(object sender, MouseEventArgs e)
        {
            Label clickedLabel = (Label)sender;

            streamer clickedStreamer = followedStreams.Find(r => r.MyLabel == clickedLabel);
            
            System.Diagnostics.Process.Start(clickedStreamer.link);
        }
    }
}
