using System.Linq;
using System.Text;
using AutoMapper;
using Mobile.Abstractions;
using Mobile.DTOs;
using Prism.Navigation;

namespace Mobile.ViewModels
{
    public class ArticleBasePageViewModel : ViewModelBase
    {
        private readonly IArticleService _articles;
        private readonly IMapper _mapper;
        private string _name;
        private bool _hasLifetime;
        private int _contentAmount;
        private string _unit;
        private string _imageUrl;

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public bool HasLifetime
        {
            get => _hasLifetime;
            set => SetProperty(ref _hasLifetime, value);
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

        public string ImageUrl
        {
            get => _imageUrl;
            set => SetProperty(ref _imageUrl, value);
        }

        public ArticleBasePageViewModel() { }

        public ArticleBasePageViewModel(IArticleService articles, IMapper mapper)
        {
            _articles = articles;
            _mapper = mapper;
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
                IsBusy = true;
                Article article = await _articles.GetArticle(barcode);
                _mapper.Map(article, this);
                Unit = _articles.BaseData.Units.Single(u => u.Id == article.UnitId).Abbreviation;
                IsBusy = false;
            }
        }


    }
}
