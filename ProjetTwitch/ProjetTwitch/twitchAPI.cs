using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace projetTwitch
{
    class twitchAPI
    {
        private string clientID;

        public twitchAPI(string clientID)
        {
            this.clientID = "client_id=" + clientID;
        }

        public string getURL(string type)
        {
            string baseURL = "https://api.twitch.tv/kraken/";
            string[] listTypes = { "teams", "channels", "games", "users", "streams", "search" };
            string[] listURL = { baseURL + "teams/",
                                 baseURL + "channels/",
                                 baseURL + "games/",
                                 baseURL + "users/",
                                 baseURL + "streams/",
                                 baseURL + "search/"};

            if (Array.IndexOf(listTypes, type) != -1)
            {
                return listURL[Array.IndexOf(listTypes, type)];
            }
            else
            {
                return null;
            }
        }

        public void isOnline(streamer stream)
        {
            WebClient client = new WebClient();
            string myURL = getURL("streams") + stream.name + "?" + this.clientID;
            try
            {
                string chaine = client.DownloadString(myURL);
                var m_data = JsonConvert.DeserializeObject<dynamic>(chaine);
                if (m_data.stream != null)
                {
                    stream.State = true;
                }
                else
                {
                    stream.State = false;
                }
            } catch (WebException)
            {
                Console.WriteLine("Server error : " + stream.ToString());
            }
            
        }

        public List<streamer> getFollowedStreams(string user)
        {
            WebClient client = new WebClient();
            string baseURL = getURL("users");
            string myURL;
            if (baseURL != null)
            {
                myURL = baseURL + user + "/follows/channels?" + this.clientID;
            }
            else
            {
                return new List<streamer>();
            }

            string chaine;
            try {
                chaine = client.DownloadString(myURL);
            } catch (WebException) {
                return new List<streamer>();
            }

            var m_data = JsonConvert.DeserializeObject<dynamic>(chaine);

            List<streamer> followedStreams = new List<streamer>();

            foreach (var obj in m_data.follows)
            {
                streamer target = new streamer(obj.channel.name.ToString(), 
                                               obj.channel.display_name.ToString(), 
                                               obj.channel.url.ToString(),
                                               obj.channel.logo.ToString());
                followedStreams.Add(target);
            }

            return followedStreams;
        }
    }

    class streamer
    {
        public String name { get; set; }
        public String displayName { get; set; }
        public bool state = false;
        public bool stateHasChanged { get; set; }
        public string link { get; set; }
        public Image logo { get; set; }

        public bool State{
            get {
                return state;
            }
            set {
                if (value==true && state == false)
                {
                    this.stateHasChanged = true;
                }
                state = value;
            }
        }
        
        public streamer(String name, String displayName, String link, String logo)
        {
            this.name = name;
            this.displayName = displayName;
            this.state = true;
            this.stateHasChanged = false;
            this.link = link;

            //Load logo
            using (WebClient webClient = new WebClient())
            {
                byte[] data = webClient.DownloadData(logo);

                using (MemoryStream mem = new MemoryStream(data))
                {
                    this.logo = Image.FromStream(mem);
                }

            }
        }

        public override string ToString()
        {
            String returnValue = this.name + " aka " + this.displayName;
            return returnValue;
        }

    }
}
