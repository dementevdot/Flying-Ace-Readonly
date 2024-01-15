using System;
using System.Collections.Generic;
using UnityEngine;

namespace Localization
{
    public class Handler : MonoBehaviour
    {
        private static Language Language;

        [SerializeField] private TranslatebleText[] _texts;
        [SerializeField] private TranslatebleTmpText[] _tmpTexts;
        [SerializeField] private TranslatebleImage[] _images;
        [SerializeField] private Data.SO.Game _data;

        private static event Action<Language> LanguageChanged;

        private void Awake()
        {
            if (_data.IsInited == false)
                _data.Inited += () => SetLanguage(_data.Language.Value);
            else
                SetLanguage(_data.Language.Value);

            var translatebles = new List<ITranslatable>();

            translatebles.AddRange(_texts);
            translatebles.AddRange(_tmpTexts);
            translatebles.AddRange(_images);

            foreach (var translateble in translatebles)
            {
                translateble.Init(Language);
                LanguageChanged += translateble.OnLanguageChanged;
            }
        }

        private void OnDestroy()
        {
            foreach (var translatebleText in _texts)
                LanguageChanged -= translatebleText.OnLanguageChanged;

            foreach (var translatebleImage in _images)
                LanguageChanged -= translatebleImage.OnLanguageChanged;
        }

        private static void SetLanguage(Language language)
        {
            if (language == Language) return;

            Language = language;
            LanguageChanged?.Invoke(Language);
        }
    }
}
