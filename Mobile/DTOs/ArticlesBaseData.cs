using System.Collections.Generic;

namespace Mobile.DTOs
{
    public class ArticlesBaseData
    {
        public IEnumerable<ArticleUnits> Units { get; set; }
        public IEnumerable<ArticleGroups> ArticleGroups { get; set; }
        public IEnumerable<Store> Stores { get; set; }
    }
}