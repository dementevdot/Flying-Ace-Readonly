using System;
using UnityEngine;

namespace Data
{
    public static class ReactivePlayerPrefs
    {
        public static event Action PlayerPrefsChanged;

        public static bool HasKey(string key) => PlayerPrefs.HasKey(key);

        public static void DeleteKey(string key)
        {
            PlayerPrefs.DeleteKey(key);
            PlayerPrefsChanged?.Invoke();
        }

        public static void DeleteAll()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefsChanged?.Invoke();
        }

        public static void SetString(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
            PlayerPrefsChanged?.Invoke();
        }

        public static string GetString(string key, string defaultValue) => PlayerPrefs.GetString(key, defaultValue);

        public static string GetString(string key) => PlayerPrefs.GetString(key);

        public static void SetInt(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
            PlayerPrefsChanged?.Invoke();
        }

        public static int GetInt(string key, int defaultValue) => PlayerPrefs.GetInt(key, defaultValue);

        public static int GetInt(string key) => PlayerPrefs.GetInt(key);

        public static void SetFloat(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
            PlayerPrefsChanged?.Invoke();
        }

        public static float GetFloat(string key, float defaultValue) => PlayerPrefs.GetFloat(key, defaultValue);

        public static float GetFloat(string key) => PlayerPrefs.GetFloat(key);
    }
}