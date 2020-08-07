using System.Threading.Tasks;
using Mobile.DTOs;

namespace Mobile.Abstractions
{
    public interface IArticleService
    {
        Task<Article> GetArticle(string barcode);
    }
}