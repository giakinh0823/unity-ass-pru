namespace Common
{
    using System;
    using UnityEngine;

    public static class LocalDataHandler
    {
        private const string LocalDataPrefix = "LocalData-";

        public static bool LoadData<T>(ref T outData)
        {
            var rawData = PlayerPrefs.GetString($"{LocalDataPrefix}{typeof(T).Name}");

            if (string.IsNullOrEmpty(rawData))
            {
                return false;
            }

            JsonUtility.FromJsonOverwrite(rawData, outData);
            return true;
        }

        public static void SaveData<T>(T data)
        {
            var rawData = JsonUtility.ToJson(data);
            PlayerPrefs.SetString($"{LocalDataPrefix}{typeof(T).Name}", rawData);
        }
    }

    public abstract class BaseLocalData<T> where T : BaseLocalData<T>, new()
    {
        public static T Instance { get; set; } = new();

        protected BaseLocalData()
        {
            this.Load();
        }

        public void Save()
        {
            LocalDataHandler.SaveData(this);
        }

        public void Load()
        {
            var data = this;
            if (LocalDataHandler.LoadData(ref data)) return;
            this.Init();
        }

        public virtual void Init()
        {
            this.Save();
        }
    }
}