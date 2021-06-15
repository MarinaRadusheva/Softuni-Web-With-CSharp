using MyWebServer.Server.Common;

namespace MyWebServer.Server.Http
{
    public class HttpHeader
    {
        public HttpHeader(string name, string value)
        {
            Guard.AgainstNull(name, nameof(name));
            Guard.AgainstNull(value, nameof(value));
            this.Name = name;
            this.Value = value;
        }
        public string Name { get; set; } 
        public string Value { get; set; }
        public override string ToString()
        {
            return $"{this.Name}: {this.Value}";
        }
    }
}
