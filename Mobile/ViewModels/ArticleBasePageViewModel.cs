using System.Text;
using Mobile.Abstractions;
using Mobile.DTOs;
using Prism.Navigation;

namespace Mobile.ViewModels
{
    public class ArticleBasePageViewModel : ViewModelBase
    {
        private readonly IArticleService _articles;
        private string _articleName;
        private bool _hasExpireDate;
        private int _contentAmount;
        private string _unit;
        private string _articleImage;

        public string ArticleName
        {
            get => _articleName;
            set => SetProperty(ref _articleName, value);
        }

        public bool HasExpireDate
        {
            get => _hasExpireDate;
            set => SetProperty(ref _hasExpireDate, value);
        }

        public int ContentAmount
        {
            get => _contentAmount;
            set => SetProperty(ref _contentAmount, value);
        }

        public string Unit
        {
            get => _unit;
            set => SetProperty(ref _unit, value);
        }

        public string ArticleImage
        {
            get => _articleImage;
            set => SetProperty(ref _articleImage, value);
        }

        public ArticleBasePageViewModel() { }

        public ArticleBasePageViewModel(IArticleService articles)
        {
            _articles = articles;
        }

        //protected ArticleBasePageViewModel()
        //{
        //    Title = "Article";

        //    ArticleImage = "https://i.ebayimg.com/images/g/sV0AAOSwJFVdtO7F/s-l500.jpg";
        //    ArticleName = "Braun Reinigungskartuschen";
        //    ContentAmount = 1;
        //    Unit = "Stk";

        //}

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters["barcode"] is string barcode)
            {
                Article article = await _articles.GetArticle(barcode);
            }
        }
    }
}
