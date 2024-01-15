namespace Localization
{
    public interface ITranslatable
    {
        public void Init(Language language);
        public void OnLanguageChanged(Language language);
    }
}