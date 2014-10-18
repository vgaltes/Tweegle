using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tweegle.Infrastructure.DTOs;

namespace Tweegle.Presentation.WebClient.Models
{
    public class SearchResultViewModel
    {
        private string[] splitTerms = { "http", " ", ",", ")", "-" };

        public SearchResultViewModel(List<TwitterFavorite> favorites)
        {
            this.Items = new List<SearchResultItemViewModel>();

            foreach (var favorite in favorites)
            {
                var links = favorite.Text.Split(splitTerms, StringSplitOptions.RemoveEmptyEntries)
                                .Where(s => s.ToLower().StartsWith("s://") || s.ToLower().StartsWith("://"))
                                .Distinct()
                                .Select(s => string.Format("http{0}", s))
                                .ToList();

                var linksWithA = links.Select(s => string.Format("<a href=\"{0}\">{0}</a>", s)).ToList();

                int i = 0;
                var linkText = favorite.Text;
                foreach (var link in links)
                {
                    linkText = linkText.Replace(link, linksWithA[i]);
                    i++;
                }

                Items.Add(new SearchResultItemViewModel
                {
                    Text = linkText,
                    AuthorName = favorite.AuthorName,
                    AuthorScreenName = favorite.AuthorScreenName,
                    CreatedAt = favorite.CreatedAt
                });
            }
        }

        public List<SearchResultItemViewModel> Items { get; private set; }
    }

    public class SearchResultItemViewModel
    {
        public string Text { get; set; }

        public string AuthorName { get; set; }

        public string AuthorScreenName { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}