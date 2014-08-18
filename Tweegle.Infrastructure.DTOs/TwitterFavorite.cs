using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tweegle.Infrastructure.DTOs
{
    public class TwitterFavorite
    {
        public TwitterFavorite(ulong id, string text, string authorName, string authorScreenName,
            DateTime createdAt, List<string> hashtags)
        {
            Id = id;
            Text = text;
            AuthorName = authorName;
            AuthorScreenName = authorScreenName;
            CreatedAt = createdAt;
            Hashtags = hashtags;
        }

        public ulong Id { get; private set; }
        public string Text { get; private set; }
        public string AuthorScreenName { get; private set; }

        public string AuthorName { get; private set; }

        public DateTime CreatedAt { get; private set; }
        public List<string> Hashtags { get; private set; }
    }
}
