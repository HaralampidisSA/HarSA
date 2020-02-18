using System.Net.Http;
using System.Text;

namespace HarSA.AspNetCore.Http
{
    public class JsonStringContent : StringContent
    {
        public JsonStringContent(string content) : this(content, Encoding.UTF8, "application/json")
        {
        }

        public JsonStringContent(string content, Encoding encoding, string mediaType) : base(content, encoding, mediaType)
        {
        }
    }
}
