using System;
using UnityEngine;

namespace Localization
{
    [Serializable]
    public abstract class Translatable<T> : ITranslatable
    {
        [SerializeField] protected T _russian;
        [SerializeField] protected T _english;
        [SerializeField] protected T _turkish;

        protected Language _currentLanguage;

        public virtual void Init(Language language)
        {
            _currentLanguage = language;
        }

        public abstract void OnLanguageChanged(Language language);

        protected T GetTranslateByLanguage(Language language)
        {
            return language switch
            {
                Language.Russian => _russian,
                Language.English => _english,
                Language.Turkish => _turkish,
                _ => throw new InvalidOperationException(nameof(language))
            };
        }
    }
}