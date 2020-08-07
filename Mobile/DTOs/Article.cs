using System;
using System.Collections.Generic;

namespace Mobile.DTOs
{
    public class Article
    {
        public int Id { get; set; }
        public string Barcode { set; get; }
        public string Name { get; set; }
        public int UnitId { get; set; }
        public int ContentAmount { get; set; }
        public bool HasLifetime { get; set; }
        public DateTime NextExpireDate { get; set; }
        public string ImageUrl { get; set; }

        public IEnumerable<ArticleStore> Stores { get; set; }

        public Article()
        {
            Stores = new List<ArticleStore>();
        }
    }
}