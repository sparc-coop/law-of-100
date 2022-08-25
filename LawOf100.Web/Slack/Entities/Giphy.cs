using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawOf100.Features.Slack.Entities
{
    public class Gifobject
    {
        public Data Data { get; set; }
    }

    public class Data
    {
        public string id { get; set; }
        public string url { get; set; }
        public string slug { get; set; }
        public string bitly_gif_url { get; set; }
        public string bitly_url { get; set; }
        public string embed_url { get; set; }

        public Images Images { get; set; }
    }

    public class Images
    {
        public Downsized Downsized { get; set; }
    }

    public class Downsized
    {
        public string Url { get; set; }
    }

}
