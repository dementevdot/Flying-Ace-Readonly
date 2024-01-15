using System;
using UnityEngine;

namespace Localization
{
    [Serializable]
    public class TextContainer
    {
        [SerializeField] protected string _russian;
        [SerializeField] protected string _english;
        [SerializeField] protected string _turkish;

        public string Russian => _russian;
        public string English => _english;
        public string Turkish => _turkish;
    }
}