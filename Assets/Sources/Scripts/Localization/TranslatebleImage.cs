using System;
using UnityEngine;
using UnityEngine.UI;

namespace Localization
{
    [Serializable]
    public class TranslatebleImage : Translatable<Sprite>
    {
        [SerializeField] private Image _image;

        public override void Init(Language language)
        {
            base.Init(language);

            _image.sprite = GetTranslateByLanguage(language);
        }

        public override void OnLanguageChanged(Language language)
        {
            if (_currentLanguage == language) return;

            _currentLanguage = language;

            _image.sprite = GetTranslateByLanguage(language);
        }
    }
}