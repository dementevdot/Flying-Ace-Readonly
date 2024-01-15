using System;
using TMPro;
using UnityEngine;

namespace Localization
{
    [Serializable]
    public class TranslatebleTmpText : Translatable<string>
    {
        [SerializeField] private TMP_Text _text;

        public override void Init(Language language)
        {
            base.Init(language);

            _text.text = GetTranslateByLanguage(_currentLanguage);
        }

        public override void OnLanguageChanged(Language language)
        {
            if (_currentLanguage == language) return;

            _currentLanguage = language;

            _text.text = GetTranslateByLanguage(language);
        }
    }
}