namespace Common
{
    using UnityEngine;

    public static class LocalDataHandler
    {
        private const string LocalDataPrefix = "LocalData-";

        public static T LoadData<T>()
        {
            var rawData = PlayerPrefs.GetString($"{LocalDataPrefix}{typeof(T).Name}");

            if (string.IsNullOrEmpty(rawData))
            {
                return default;
            }

            return JsonUtility.FromJson<T>(rawData);
        }

        public static void SaveData<T>(T data)
        {
            var rawData = JsonUtility.ToJson(data);
            PlayerPrefs.SetString($"{LocalDataPrefix}{typeof(T).Name}", rawData);
        }
    }
}