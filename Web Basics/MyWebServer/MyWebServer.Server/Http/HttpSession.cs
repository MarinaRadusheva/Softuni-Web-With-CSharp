using MyWebServer.Server.Common;
using System.Collections.Generic;

namespace MyWebServer.Server.Http
{
    public class HttpSession
    {

        public const string SessionCookieName = "MySessionId";

        private Dictionary<string, string> data;

        public HttpSession(string id)
        {
            Guard.AgainstNull(id, nameof(id));
            this.Id = id;
            this.data = new Dictionary<string, string>();
        }
        public string Id { get; private set; }
     

        public string this[string key]
        {
            get => this.data[key];
            set => this.data[key] = value;
        }

        public bool ContainsKey(string key)
        {
            return this.data.ContainsKey(key);
        }
    }
}
