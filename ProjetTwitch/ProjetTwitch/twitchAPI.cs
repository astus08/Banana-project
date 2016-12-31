using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
            string[] listTypes = { "teams", "channels", "games", "users", "streams", "search"};
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

        public bool isOnline(string streamer)
        {
            WebClient client = new WebClient();
            string myURL = getURL("streams") + streamer + "?" + this.clientID;
            string chaine = client.DownloadString(myURL);
            var m_data = JsonConvert.DeserializeObject<dynamic>(chaine);
            if (m_data.stream != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
