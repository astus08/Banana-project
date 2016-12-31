using projetTwitch;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjetTwitch
{
    public partial class Form1 : Form
    {
        private twitchAPI twitchInit = new twitchAPI("oc651hjfox0rk94wahy25hpm8d17o5");

        public Form1()
        {
            InitializeComponent();
        }

        private void validateButton_Click(object sender, EventArgs e)
        {
            string streamer = streamerBox.Text;

            if (twitchInit.isOnline(streamer))
            {
                streamerState.Text = streamer + " is now ONLINE";
                streamerState.ForeColor = Color.Green;
            }
            else
            {
                streamerState.Text = streamer + " is now OFFLINE";
                streamerState.ForeColor = Color.Red;
            }
        }
    }
}
