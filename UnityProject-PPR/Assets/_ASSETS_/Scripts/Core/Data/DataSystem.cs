using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public partial class DataSystem
{
    public delegate void DataIsChanged(int category, int tag, object from, object to);
    public delegate void MapIsChanged(string key, object from, object to);
    public delegate void DataDoubleChanged(double from, double to);
    public delegate void DataFloatChanged(float from, float to);
    public delegate void DataLongChanged(long from, long to);
    public delegate void DataIntChanged(int from, int to);
    public delegate void DataStringChanged(string from, string to);
    public delegate void DataBoolChanged(bool from, bool to);

    internal const int CATEGORY_MAX = 2;
    internal const int TAG_MAX = 5;

    [Serializable]
    private class Sentinel
    {
        [SerializeField] public bool[][] data;
        [SerializeField] public Dictionary<string, bool> map;

        [SerializeField] public DataIsChanged[][] dataIsChanged;
        [SerializeField] public Dictionary<string, MapIsChanged> mapIsChanged;

        public void InitSentinel()
        {
            data = new bool[CATEGORY_MAX][];
            dataIsChanged = new DataIsChanged[CATEGORY_MAX][];

            for (int i = 0; i < data.Length; i++)
            {
                data[i] = new bool[TAG_MAX];
                dataIsChanged[i] = new DataIsChanged[TAG_MAX];
                
                for (int k = 0; k < data[i].Length; k++)
                {
                    data[i][k] = false;
                    dataIsChanged[i][k] = null;
                }
            }

            map = new Dictionary<string, bool>();
            mapIsChanged = new Dictionary<string, MapIsChanged>();
        }
    }

    [Serializable]
    private class DataSet
    {
        [SerializeField] public object[][] data;
        [SerializeField] public Dictionary<string, object> map;

        public void InitDataSet()
        {
            data =  new object[CATEGORY_MAX][];
            
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = new object[TAG_MAX];
            }

            map = new Dictionary<string, object>();
        }
    }

    private DataSet data = null;
    private Sentinel sentinel = null;

    private static DataSystem instance = null;
    public static DataSystem Instance
    {
        get { return instance == null ? instance = new DataSystem() : instance; }
    }

    private DataSystem()
    {
        if (data == null) data = new DataSet();
        if (sentinel == null) sentinel = new Sentinel();

        data.InitDataSet();
        sentinel.InitSentinel();
    }

    /// <summary>
    /// Data Set Value
    /// </summary>
    public void SetValue(int category, int tag, double value)
    {
        var from = GetDouble(category, tag);
        if (from == value) return;

        data.data[category][tag] = Encrypt(value);
        sentinel.data[category][tag] = true;
        sentinel.dataIsChanged[category][tag]?.Invoke(category, tag, from, value);
    }

    public void SetValue(int category, int tag, float value)
    {
        var from = GetFloat(category, tag);
        if (from == value) return;

        data.data[category][tag] = Encrypt(value);
        sentinel.data[category][tag] = true;
        sentinel.dataIsChanged[category][tag]?.Invoke(category, tag, from, value);
    }

    public void SetValue(int category, int tag, long value)
    {
        var from = GetLong(category, tag);
        if (from == value) return;

        data.data[category][tag] = Encrypt(value);
        sentinel.data[category][tag] = true;
        sentinel.dataIsChanged[category][tag]?.Invoke(category, tag, from, value);
    }

    public void SetValue(int category, int tag, int value)
    {
        var from = GetInt(category, tag);
        if (from == value) return;

        data.data[category][tag] = Encrypt(value);
        sentinel.data[category][tag] = true;
        sentinel.dataIsChanged[category][tag]?.Invoke(category, tag, from, value);
    }

    public void SetValue(int category, int tag, string value)
    {
        var from = GetString(category, tag);
        if (from == value) return;

        data.data[category][tag] = Encrypt(value);
        sentinel.data[category][tag] = true;
        sentinel.dataIsChanged[category][tag]?.Invoke(category, tag, from, value);
    }

    public void SetValue(int category, int tag, bool value)
    {
        var from = GetBool(category, tag);
        if (from == value) return;

        data.data[category][tag] = Encrypt(value);
        sentinel.data[category][tag] = true;
        sentinel.dataIsChanged[category][tag]?.Invoke(category, tag, from, value);
    }

    /// <summary>
    /// Data Set Map
    /// </summary>
    public void SetValue(string key, double value)
    {
        var from = GetDouble(key);
        if (from == value) return;

        data.map[key] = Encrypt(value);
        sentinel.map[key] = true;
        if (sentinel.mapIsChanged.ContainsKey(key))
            sentinel.mapIsChanged[key]?.Invoke(key, from, value);
    }

    public void SetValue(string key, float value)
    {
        //Debug.Log("set float");

        var from = GetFloat(key);
        if (from == value) return;

        data.map[key] = Encrypt(value);
        sentinel.map[key] = true;
        if (sentinel.mapIsChanged.ContainsKey(key))
            sentinel.mapIsChanged[key]?.Invoke(key, from, value);
    }

    public void SetValue(string key, long value)
    {
        var from = GetLong(key);
        if (from == value) return;

        data.map[key] = Encrypt(value);
        sentinel.map[key] = true;
        if (sentinel.mapIsChanged.ContainsKey(key))
            sentinel.mapIsChanged[key]?.Invoke(key, from, value);
    }

    public void SetValue(string key, int value)
    {
        var from = GetInt(key);
        if (from == value) return;

        data.map[key] = Encrypt(value);
        sentinel.map[key] = true;
        if (sentinel.mapIsChanged.ContainsKey(key))
            sentinel.mapIsChanged[key]?.Invoke(key, from, value);
    }

    public void SetValue(string key, string value)
    {
        //Debug.Log("set string");

        var from = GetString(key);
        if (from == value) return;

        data.map[key] = Encrypt(value);
        sentinel.map[key] = true;
        if (sentinel.mapIsChanged.ContainsKey(key))
            sentinel.mapIsChanged[key]?.Invoke(key, from, value);
    }

    public void SetValue(string key, bool value)
    {
        var from = GetBool(key);
        if (from == value) return;

        data.map[key] = Encrypt(value);
        sentinel.map[key] = true;
        if (sentinel.mapIsChanged.ContainsKey(key))
            sentinel.mapIsChanged[key]?.Invoke(key, from, value);
    }

    /// <summary>
    /// Data Get Value
    /// </summary>
    public double GetDouble(int category, int tag)
    {
        return DecryptDouble(data.data[category][tag]);
    }

    public float GetFloat(int category, int tag)
    {
        return DecryptFloat(data.data[category][tag]);
    }

    public long GetLong(int category, int tag)
    {
        return DecryptLong(data.data[category][tag]);
    }

    public int GetInt(int category, int tag)
    {
        return DecryptInt(data.data[category][tag]);
    }

    public string GetString(int category, int tag)
    {
        return DecryptString(data.data[category][tag]);
    }

    public bool GetBool(int category, int tag)
    {
        return DecryptBool(data.data[category][tag]);
    }

    /// <summary>
    /// Data Get Map
    /// </summary>
    public double GetDouble(string key, double def = 0.0)
    {
        if(data.map.ContainsKey(key)) return DecryptDouble(data.map[key]);

        data.map[key] = Encrypt(def);
        return def;
    }

    public float GetFloat(string key, float def = 0.0f)
    {
        if (data.map.ContainsKey(key)) return DecryptFloat(data.map[key]);

        data.map[key] = Encrypt(def);
        return def;
    }

    public long GetLong(string key, long def = 0)
    {
        if (data.map.ContainsKey(key)) return DecryptLong(data.map[key]);

        data.map[key] = Encrypt(def);
        return def;
    }

    public int GetInt(string key, int def = 0)
    {
        if (data.map.ContainsKey(key)) return DecryptInt(data.map[key]);

        data.map[key] = Encrypt(def);
        return def;
    }

    public string GetString(string key, string def = "")
    {
        if (data.map.ContainsKey(key)) return DecryptString(data.map[key]);

        data.map[key] = Encrypt(def);
        return def;
    }

    public bool GetBool(string key, bool def = false)
    {
        if (data.map.ContainsKey(key)) return DecryptBool(data.map[key]);

        data.map[key] = Encrypt(def);
        return def;
    }

    public void RemoveKey(string key)
    {
        if (data.map.ContainsKey(key))
        {
            data.map.Remove(key);
        }
    }

    /// <summary>
    /// Data Changed Check (On Update)
    /// </summary>
    public bool ValueIsChanged(int category, int tag)
    {
        bool ret = sentinel.data[category][tag];
        sentinel.data[category][tag] = false;
        return ret;
    }

    public bool ValueIsChanged(string key)
    {
        if (!sentinel.map.ContainsKey(key)) return false;

        bool ret = sentinel.map[key];
        sentinel.map[key] = false;
        return ret;
    }

    public void SetValueIsChangedCallback(int category, int tag, DataIsChanged callback)
    {
        sentinel.dataIsChanged[category][tag] = callback;
    }

    public void SetMapIsChangedCallback(string key, MapIsChanged callback)
    {
        sentinel.mapIsChanged[key] = callback;
    }

    /// <summary>
    /// Data Save & Load
    /// </summary>
    public void SaveToFile(string fileName = "game.dat")
    {
        string fullPath = Application.persistentDataPath + "/" + fileName;
        string json = JsonConvert.SerializeObject(data, Formatting.Indented);
        File.WriteAllText(fullPath, json);
        Debug.Log("Saved to " + fullPath);
    }

    public void LoadFromFile(string fileName = "game.dat")
    {
        string fullPath = Application.persistentDataPath + "/" + fileName;
        string jsonRaw = File.ReadAllText(fullPath);

        DataSet tmpData = JsonConvert.DeserializeObject<DataSet>(jsonRaw);
        data = tmpData;

        for (int i = 0; i < tmpData.data.Length; i++)
        {
            for (int k = 0; k < tmpData.data[i].Length; k++)
            {
                if (tmpData.data[i][k] == data.data[i][k]) continue;
                if (tmpData.data[i][k] == null) continue;

                sentinel.data[i][k] = true;
                sentinel.dataIsChanged[i][k](i, k, data.data[i][k], tmpData.data[i][k]);
            }
        }

        // TODO: DATA 정리 필요
        foreach (string key in tmpData.map.Keys)
        {
            //if (tmpData.map[key] == data.map[key]) continue;
            if (tmpData.map[key] == null) continue;

            SettingsDataLoad(key, data.map[key]);

            sentinel.map[key] = true;
            if (sentinel.mapIsChanged.ContainsKey(key))
            {
                sentinel.mapIsChanged[key](key, data.map[key], tmpData.map[key]);
            }
        }

        Debug.Log("Load from " + fullPath);
    }

    public void SettingsDataLoad(string key, object value)
    {
        switch (key)
        {
            case "SETTINGS_BGM":
                DataFrame.Instance.dBGM = Convert.ToSingle(value);
                break;
            case "SETTINGS_SFX":
                DataFrame.Instance.dSFX = Convert.ToSingle(value);
                break;
            case "SETTINGS_FPS":
                DataFrame.Instance.dFPS = Convert.ToInt32(value);
                break;
            case "SETTINGS_LANGUAGE":
                DataFrame.Instance.dLanguage = (string)value;
                break;
        }

        //TODO => Dictionary로 사용
    }
}
