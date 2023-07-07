using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data
{
    public class WriteData
    {
        public static void Save(string key, int value) => PlayerPrefs.SetInt(key, value);
        public static void Save(string key, float value) => PlayerPrefs.SetFloat(key, value);
        public static void Save(string key, bool value) => PlayerPrefs.SetInt(key, value ? 1 : 0);
        public static void Save(string key, string value) => PlayerPrefs.SetString(key, value);
    }

    public class ReadData
    {
        public static int LoadData(string  key, int defaultValue) => PlayerPrefs.GetInt(key, defaultValue);
        public static float LoadData(string key, float defaultValue) => PlayerPrefs.GetFloat(key, defaultValue);
        public static bool LoadData(string key, bool defaultValue) => PlayerPrefs.GetInt(key, defaultValue ? 1 : 0) == 1;
        public static string LoadData(string  key, string defaultValue) => PlayerPrefs.GetString(key, defaultValue);
    }
}
