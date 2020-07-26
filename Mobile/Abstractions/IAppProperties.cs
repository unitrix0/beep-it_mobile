namespace Mobile.Abstractions
{
    public interface IAppProperties
    {
        string GetProperty(string name);
        void SetProperty(string name, string value);
    }
}